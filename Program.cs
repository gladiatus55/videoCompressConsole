using System;
using System.Diagnostics;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        string directoryPath;

        if (args.Length == 0)
        {
            Console.Write("Enter the directory path: ");
            directoryPath = Console.ReadLine();
        }
        else
        {
            directoryPath = args[0];
        }

        if (string.IsNullOrWhiteSpace(directoryPath) || !Directory.Exists(directoryPath))
        {
            Console.WriteLine("The specified directory does not exist.");
            return;
        }

        string ffmpegPath = Path.Combine(Directory.GetCurrentDirectory(), "Tools", "ffmpeg.exe");
        if (!File.Exists(ffmpegPath))
        {
            Console.WriteLine("ffmpeg.exe not found in the current directory.");
            return;
        }

        ConvertFilesInDirectory(directoryPath, ffmpegPath);
    }

    static void ConvertFilesInDirectory(string directoryPath, string ffmpegPath)
    {
        foreach (string file in Directory.GetFiles(directoryPath))
        {
            string extension = Path.GetExtension(file).ToLower();
            if (extension == ".mp4") // Modify this condition for different file types
            {
                string outputFile = Path.Combine(directoryPath, Path.GetFileNameWithoutExtension(file) + "-c.mp4");
                if (ConvertFile(ffmpegPath, file, outputFile))
                {
                    File.Delete(file); // Delete original file after successful conversion
                    Console.WriteLine($"Deleted original file: {file}");
                }
            }
        }

        foreach (string subdirectory in Directory.GetDirectories(directoryPath))
        {
            ConvertFilesInDirectory(subdirectory, ffmpegPath);
        }
    }

    static bool ConvertFile(string ffmpegPath, string inputFile, string outputFile)
    {
        ProcessStartInfo processInfo = new ProcessStartInfo
        {
            FileName = ffmpegPath,
            Arguments = $"-i \"{inputFile}\" -vcodec h264 -acodec mp2 \"{outputFile}\"",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using (Process process = new Process { StartInfo = processInfo })
        {
            process.OutputDataReceived += (sender, data) => Console.WriteLine(data.Data);
            process.ErrorDataReceived += (sender, data) => Console.WriteLine(data.Data);
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();
            return process.ExitCode == 0;
        }
    }
}

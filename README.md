# Video Converter using FFmpeg

## Overview
This is a C# console application that recursively traverses a specified directory and converts all `.mp4` video files using `ffmpeg`. After conversion, it compares the file sizes:
- If the compressed file is **smaller**, it deletes the original.
- If the compressed file is **larger**, it deletes the compressed file and renames the original with a `-c` suffix.

## Features
- Recursively processes `.mp4` files in subdirectories.
- Uses `ffmpeg` to re-encode videos with H.264 and MP2 codecs.
- Deletes the original file if the compression is successful.
- Retains the original file if compression results in a larger size.
- Renames retained original files with a `-c` suffix.

## Prerequisites
- .NET SDK installed ([Download .NET](https://dotnet.microsoft.com/en-us/download))

## How to Use
### **Run via Command Line**
```sh
VideoConverter.exe "C:\path\to\directory"
```
If no directory is provided, the program will prompt you to enter one.

## Example Behavior
**Before Conversion:**
```
/video-folder/
├── video1.mp4 (50MB)
├── video2.mp4 (80MB)
```
**After Conversion:**
- If compression is successful:
```
/video-folder/
├── video1-c.mp4 (30MB) ✅ (Original Deleted)
├── video2-c.mp4 (60MB) ✅ (Original Deleted)
```
- If compression results in a larger file:
```
/video-folder/
├── video1-c.mp4 (50MB) ❌ (Compressed Deleted, Original Renamed)
├── video2-c.mp4 (80MB) ❌ (Compressed Deleted, Original Renamed)
```

## Compilation
Run the following command to compile the program:
```sh
dotnet build
```

## Notes
- Ensure `ffmpeg.exe` is available in the application directory.
- Only `.mp4` files are processed.
- The program does not overwrite existing `-c.mp4` files.

## License
GNU LESSER GENERAL PUBLIC LICENSE Version 2.1
# WinLatch - Window Black Bar Manager


> **Note**: This project is currently a work in progress (WIP). Features will be added as development continues.


WinLatch is a Windows application that helps manage fullscreen and borderless fullscreen windows to reposition or remove black bars, particularly useful for games running in different aspect ratios.

## Features

- **Window Detection**: Automatically detects all visible windows on your system
- **Aspect Ratio Management**: Supports various aspect ratios (16:9, 16:10, 3:2, ultrawide 21:9, super ultrawide 32:9, etc.)
- **Black Bar Positioning**: Move black bars to top, bottom, left, or right
- **Black Bar Removal**: Remove black bars entirely for side-by-side usage on ultrawide monitors
- **Overlay Support**: Add custom overlays to black bar areas with text or images
- **Transparency Control**: Make black bars transparent for better visibility

## Use Cases

### Gaming Examples
- **16:9 on 16:10 laptop**: Move all black bars to the bottom for a cleaner look
- **16:9 games on ultrawide**: Position the game to the left and use the right side for other applications
- **Emulation**: Add console logos or branding to black bar areas
- **Streaming**: Add overlays with stream information or branding

### Supported Aspect Ratios
- 16:9 (Standard Widescreen)
- 16:10 (Standard Laptop)
- 4:3 (Traditional)
- 3:2 (GBA)
- 21:9 (Ultrawide)
- 32:9 (Super Ultrawide)
- 5:4 (Who uses this?)

## How to Use

1. **Launch WinLatch**: Run the application as administrator for best compatibility
2. **Select a Window**: Choose from the list of available windows
3. **Choose Aspect Ratio**: Select the target aspect ratio from the dropdown
4. **Position Black Bars**: Use the positioning buttons to move black bars
5. **Create Overlays**: Add custom text or images to black bar areas
6. **Adjust Transparency**: Control overlay transparency for optimal visibility

### Positioning Options

- **Bars to Bottom**: Moves all black bars to the bottom of the screen
- **Bars to Top**: Moves all black bars to the top of the screen
- **Bars to Left**: Moves all black bars to the left side
- **Bars to Right**: Moves all black bars to the right side
- **Remove Black Bars**: Resizes the window to eliminate black bars (useful for ultrawide monitors)

### Overlay Features

- **Text Overlays**: Add custom text to black bar areas
- **Image Overlays**: Display logos, branding, or decorative images
- **Transparency Control**: Adjust overlay opacity from 0-100%
- **Multiple Overlays**: Create multiple overlay elements simultaneously

## Requirements

- Windows 10/11
- .NET 9.0 Runtime
- Administrator privileges (recommended for full window manipulation)

## Building from Source

1. Install Visual Studio 2022 or Visual Studio Code with C# extension
2. Install .NET 9.0 SDK
3. Clone this repository
4. Open the project and build:
   ```
   dotnet build src
   ```
5. Run the application:
   ```
   dotnet run --project src
   ```
   
   Or use the convenience scripts:
   - Windows: `run.bat`
   - PowerShell: `run.ps1`

## Project Structure

```
WinLatch/
├── src/                    # Source code
│   ├── Program.cs          # Application entry point
│   ├── MainForm.cs         # Main GUI interface
│   ├── WindowManager.cs    # Window detection and manipulation
│   ├── WindowPositioner.cs # Black bar positioning logic
│   ├── AspectRatio.cs      # Aspect ratio calculations
│   ├── OverlayForm.cs      # Overlay creation and management
│   └── ...                 # Other source files
├── README.md               # This file
├── LICENSE                 # MIT License
├── run.bat                 # Windows batch script to build and run
├── run.ps1                 # PowerShell script to build and run
└── WinLatch.sln           # Visual Studio solution file
```

## Tips

- **For best results**, run games in borderless fullscreen mode
- **Administrator privileges** may be required for some applications
- **Game compatibility** varies - some games may reset their position
- **Multiple monitors** are supported automatically
- **Hotkeys** can be added in future versions for quick positioning

## Troubleshooting

- If windows don't move, try running as administrator
- Some games may override window positioning - try borderless mode
- For older games, compatibility mode may be required
- Restart the application if window detection becomes unreliable

## License

This project is licensed under the MIT License - see the LICENSE file for details.
A program to "latch" a forced aspect ratio window anywhere on your screen

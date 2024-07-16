# 1. CameraCaptureUI API

This is the spec for proposal: [Issue #1034](https://github.com/microsoft/WindowsAppSDK/issues/1034)

This spec details the implementation of CameraCaptureUI, enabling WinUI 3 desktop applications to seamlessly integrate native camera capture functionality across diverse Windows platforms. 

# 2. Background

The current OS CameraCaptureUI implementation, primarily designed for UWP applications, presents several challenges when used in desktop environments. Key issues include the dependency on CoreWindow, which are not compatible with desktop app frameworks and the lack of support for IInitializeWithWindow. As a result, developers are forced to resort to workarounds involving interop interfaces and additional code to integrate camera capture functionality into their WinUI 3 desktop applications.

In response to this feedback, the CameraCaptureUI API for WinAppSDK aims to provide a streamlined solution that meets the expectations of our developer community. The goal is to offer seamless integration of camera capture capabilities with full feature parity across all Windows platforms supported by WinUI 3. This new API eliminates the need for cumbersome workarounds and simplifies the process of incorporating camera functionality into desktop applications.



# 3. Description

Winappsdk version of CameraCaptureUI is essentially the same as the existing public CameraCaptureUI OS API, with a minor adjustment to the CameraCapture constructor to accept a windowID in the constructor for enhanced integration capabilities with desktop applications.

The ***Existing API*** includes the following key components:


## 3.1 CameraCaptureUI

Provides a full window UI for capturing audio, video, and photos from a camera. As well as controls for trimming video, time delayed capture, and camera settings.

This example shows how to use the CameraCaptureUI Class to take a picture. The dialog with the user occurs when you call the asynchronous method CaptureFileAsync.

```c#
CameraCaptureUI dialog = new CameraCaptureUI(windowId);
Size aspectRatio = new Size(16, 9);
dialog.PhotoSettings.CroppedAspectRatio = aspectRatio;

StorageFile file = await dialog.CaptureFileAsync(CameraCaptureUIMode.Photo);
```

### 3.1.1 Constructor (The Change Introduced)
| Name | Description |
|-|-|
| CameraCaptureUI(windowID) | Create a new CameraCaptureUI object.|
```
Note: After you create a new CameraCaptureUI object, you may want to set the PhotoSettings or VideoSettings property of the object before calling CaptureFileAsync.
```

### 3.1.2 Properties
| Name | Description | Value |
|-|-|-|
|PhotoSettings| Provides settings for capturing photos. The settings include aspect ratio, image size, format, resolution, and whether or not cropping is allowed by the user interface (UI).|CameraCaptureUIPhotoCaptureSettings (An object containing settings for capturing photos) |
|VideoSettings| Provides settings for capturing videos. The settings include format, maximum resolution, maximum duration, and whether or not to allow trimming.| CameraCaptureUIVideoCaptureSettings (An object that provides settings for capturing videos) |

### 3.1.3 Methods

| Name | Description | Parameters | Returns |
|-|-|-|-|
| CaptureFileAsync(CameraCaptureUIMode) | Launches the CameraCaptureUI user interface.| __mode__ CameraCaptureUIMode | __IAsyncOperation< StorageFile >__ |

### 3.1.4 Remarks
CameraCaptureUI provides a full window UI experience for capturing audio, video, and images. It provides controls for setting a time delay on photo captures, trimming video, and for adjusting the camera's settings such as video resolution, the audio device, brightness, and contrast.

Call CaptureFileAsync to launch the UI. The user has control over when to start the capture. When the asynchronous CaptureFileAsync operation completes, a StorageFile object is returned. 

`` Note: 
You should not specify the webcam or microphone capabilities in your app manifest file if you are using CameraCaptureUI. If you do so, your app will be displayed in the device's camera privacy settings, but even if the user denies camera access to your app, it will not prevent the CameraCaptureUI from capturing media. This is because the Windows built-in camera app is a trusted first-party app that requires the user to initiate photo, audio, and video capture with a button press. Your app may fail Windows Application Certification Kit certification when submitted to the Store if you specify the webcam or microphone capabilities when using CameraCaptureUI.
``

## 3.2 CameraCaptureUIPhotoCaptureSettings Class
Provides settings for capturing photos with CameraCaptureUI. The settings include aspect ratio, image size, format, resolution, and whether or not cropping is allowed by the user interface (UI).

### Properties

| Name | Description | Value |
|-|-|-|
| AllowCropping | Determines whether photo cropping will be enabled in the user interface for capture a photo.| Boolean |
| CroppedAspectRatio | The aspect ratio of the captured photo.| Windows.Foundation.Size |
| CroppedSizeInPixels |The exact size in pixels of the captured photo.| Windows.Foundation.Size |
| Format |Determines the format that captured photos will be stored in.| CameraCaptureUIPhotoFormat |
| MaxResolution | Determines the maximum resolution the user will be able to select. | CameraCaptureUIMaxPhotoResolution |

## 3.3 CameraCaptureUIVideoCaptureSettings Class
Provides settings for capturing videos. The settings include format, maximum resolution, maximum duration, and whether or not to allow trimming.

### Properties
| Name | Description | Value |
|-|-|-|
| AllowTrimming | Determines whether the user can trim the captured video.| Boolean |
| Format | Determines the format that captured videos will be stored in.| CameraCaptureUIVideoFormat |
| MaxDurationInSeconds | The maximum duration of the video that the user can capture.| Single |
| MaxResolution | Determines the maximum resolution the user will be able to select.| CameraCaptureUIMaxVideoResolution |


## 3.4 Enums
All existing enumerated values from the CameraCapture class are brought over as-is to the Windows App SDK version as well.

#### 3.4.1 CameraCaptureUIMode

Determines whether the user interface for capturing from the attached camera allows capture of photos, videos, or both photos and videos.

| Name | Value | Description |
|-|-|-|
| PhotoOrVideo | 0 | The user interface allows capture of photos and videos. |
| Photo | 1 | The user interface allows capture of photos only.|
| Video | 2 | The user interface allows capture of videos only. |

#### 3.4.2 CameraCaptureUIPhotoFormat
Determines the format for photos captured with CameraCaptureUI.

| Name | Value | Description |
|-|-|-|
| Jpeg | 0 |JPEG format. |
| Png | 1 | PNG format. |
| JpegXR | 2 | JPEG-XR format. |

#### 3.4.3 CameraCaptureUIVideoFormat
Determines the format for saving captured videos.

| Name | Value | Description |
|-|-|-|
| Mp4 | 0 |MP4/H.264/AAC format.|
| Wmv | 1 | WMV format. |

#### 3.4.4 CameraCaptureUIMaxPhotoResolution
Determines the highest resolution the user can select for capturing photos.

| Name | Value | Description |
|-|-|-|
| HighestAvailable | 0 | The user can select any resolution. |
| VerySmallQvga | 1 |The user can select resolutions up to 320 X 240, or a similar 16:9 resolution. |
| SmallVga | 2 | The user can select resolutions up to 320 X 240, or a similar 16:9 resolution.|
| MediumXga | 3 | The user can select resolutions up to 1024 X 768, or a similar 16:9 resolution. |
| Large3M | 4 | The user can select resolutions up to 1920 X 1080, or a similar 4:3 resolution. |
| VeryLarge5M | 5 | The user can select resolutions up to 5 MP.|


#### 3.4.5 CameraCaptureUIMaxVideoResolution
Determines the highest resolution the user can select for capturing video.

| Name | Value | Description |
|-|-|-|
| HighestAvailable | 0 | The user can select any resolution. |
| LowDefinition | 1 | The user can select resolutions up to low definition resolutions.|
| StandardDefinition | 2 | The user can select resolutions up to standard definition resolutions.|
| HighDefinition | 3 | The user can select resolutions up to high definition resolutions. |

# 4. Examples

## 4.1 Capture a photo (C++)

A C++ example that tests whether a photo was successfully taken using the CaptureFileAsync method.

```c++
    // Initialize CameraCaptureUI with a specific app window handle (12345 in this case)

    CameraCaptureUI cameraUI({12345});

    // Configure Photo Settings
    cameraUI.PhotoSettings().Format(CameraCaptureUIPhotoFormat::Jpeg);
    cameraUI.PhotoSettings().AllowCropping(false);

    // Capture a photo asynchronously
    auto photoOperation = cameraUI.CaptureFileAsync(CameraCaptureUIMode::Photo);
    auto photo = photoOperation.get(); // Get the result synchronously

    // Assert the result
    if (photo != nullptr)
    {
        // Log success if photo capture was successful
        Log::Comment(L"Photo capture was successful.");
    }
    else
    {
        // Log error if photo capture failed or was canceled
        Log::Error(L"Photo capture failed or was canceled.");
    }
 ```

 ## 4.2 Capture a video (c#)
 The code is from file [CaptureVideo.xaml.cs](https://github.com/microsoftarchive/msdn-code-gallery-microsoft/tree/master/Official%20Windows%20Platform%20Sample/Windows%208%20app%20samples/%5BC%23%5D-Windows%208%20app%20samples/C%23/Windows%208%20app%20samples/CameraCaptureUI%20Sample%20(Windows%208)/C%23) of the Camera capture UI C# sample with a small modification to include the window ID in the CameraCaptureUI constructor.
 ```c#
// Initialize CameraCaptureUI with a specific app window handle (12345 in this case)
CameraCaptureUI dialog = new CameraCaptureUI(123456);

// Configure Video Settings
dialog.VideoSettings.Format = CameraCaptureUIVideoFormat.Mp4;

// Capture a video asynchronously
StorageFile file = await dialog.CaptureFileAsync(CameraCaptureUIMode.Video);

if (file != null)
{
    // Open the captured file for reading
    using (IRandomAccessStream fileStream = await file.OpenAsync(FileAccessMode.Read))
    {
        // Log success if video capture was successful
        // Add any additional logic or UI updates here
    }
}
else
{
    // Log message if no video was captured
    // Consider adding your own logging mechanism here
    // For example: logger.Log("No video captured.");
}
```

# 5. Interface Definition

```c++ (but really MIDL3)
namespace Microsoft.Windows.Media.Capture
{
    [contractversion(1)]
    apicontract CameraCaptureUIContract {};

    [contract(CameraCaptureUIContract, 1)]
    typedef enum CameraCaptureUIMode
    {
        PhotoOrVideo = 0,
        Photo,
        Video,
    } CameraCaptureUIMode;

    [contract(CameraCaptureUIContract, 1)]
    typedef enum CameraCaptureUIPhotoFormat
    {
        Jpeg = 0,
        Png,
        JpegXR,
    } CameraCaptureUIPhotoFormat;

    [contract(CameraCaptureUIContract, 1)]
    typedef enum CameraCaptureUIVideoFormat
    {
        Mp4 = 0,
        Wmv,
    } CameraCaptureUIVideoFormat;

    [contract(CameraCaptureUIContract, 1)]
    typedef enum CameraCaptureUIMaxVideoResolution
    {
        HighestAvailable = 0,
        LowDefinition,
        StandardDefinition,
        HighDefinition,
    } CameraCaptureUIMaxVideoResolution;

    [contract(CameraCaptureUIContract, 1)]
    typedef enum CameraCaptureUIMaxPhotoResolution
    {
        HighestAvailable = 0,
        VerySmallQvga,
        SmallVga,
        MediumXga,
        Large3M,
        VeryLarge5M,
    } CameraCaptureUIMaxPhotoResolution;

    [contract(CameraCaptureUIContract, 1)]
    [default_interface]
    runtimeclass CameraCaptureUIPhotoSettings
    {
        CameraCaptureUIPhotoSettings();

        Boolean AllowCropping;
        Windows.Foundation.Size CroppedAspectRatio;
        Windows.Foundation.Size CroppedSizeInPixels;
        Microsoft.Windows.Media.Capture.CameraCaptureUIPhotoFormat Format;
        Microsoft.Windows.Media.Capture.CameraCaptureUIMaxPhotoResolution MaxResolution;
    }

    [contract(CameraCaptureUIContract, 1)]
    [default_interface]
    runtimeclass CameraCaptureUIVideoSettings
    {
        CameraCaptureUIVideoSettings();

        Boolean AllowTrimming;
        Microsoft.Windows.Media.Capture.CameraCaptureUIVideoFormat Format;
        Single MaxDurationInSeconds;
        Microsoft.Windows.Media.Capture.CameraCaptureUIMaxVideoResolution MaxResolution;
    }

    [contract(CameraCaptureUIContract, 1)]
    [default_interface]
    runtimeclass CameraCaptureUI
    {
        CameraCaptureUI(Microsoft.UI.WindowId window);
        //CameraCaptureUI(Microsoft.UI.WindowId window);

        CameraCaptureUIPhotoSettings PhotoSettings{ get; };
        CameraCaptureUIVideoSettings VideoSettings{ get; };

        Windows.Foundation.IAsyncOperation<Windows.Storage.StorageFile> CaptureFileAsync(Microsoft.Windows.Media.Capture.CameraCaptureUIMode mode);
    }
}
```
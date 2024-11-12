# .NET MAUI Shaders Carousel Demo

https://github.com/user-attachments/assets/6171f4c6-e32f-4194-9c01-10e9a0ba7cb7

__Still working on this.__

**UNDER CONSTRUCTION**

Among some stunning features, SkiaSharp version 3 brought the latest [SKSL syntax](https://skia.org/docs/user/sksl/). 
Can now use [official samples](https://shaders.skia.org/) etc, to create shaders for .NET MAUI.

An important new possibility is to port existing GLSL shaders into the modern SKSL, what was done in this case. Adapted GLSL shaders from https://github.com/gl-transitions/gl-transitions were used.

[DrawnUI](https://github.com/taublast/DrawnUi.Maui) preview nuget allowed to use SkiaSharp intuitively on a hardware-accelerated Skia canvas.

Do not run this app on iOS and Catalyst simulators as SkiaSharp is not supporting their hardware acceleration at this time.

One could use the code to serve as a stand-alone control inside a usual MAUI app, just reference/initialize DrawnUI nuget then put the canvas with a drawn carousel anywhere. You could ship shader files inside the app `Raw` folder or even as strings in code or from resources.

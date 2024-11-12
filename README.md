# .NET MAUI Shaders Carousel Demo

https://github.com/user-attachments/assets/6171f4c6-e32f-4194-9c01-10e9a0ba7cb7

__Still working on this.__

**UNDER CONSTRUCTION**

Among some stunning features, SkiaSharp version 3 brought the latest [SKSL syntax](https://skia.org/docs/user/sksl/). 
Can now use [official samples](https://shaders.skia.org/) etc, to create shaders for .NET MAUI.

An important new possibility is to port existing GLSL shaders into the modern SKSL, what was done in this case. Adapted GLSL shaders from https://github.com/gl-transitions/gl-transitions were used.

[DrawnUI](https://github.com/taublast/DrawnUi.Maui) preview nuget allowed to use SkiaSharp intuitively.

DO NOT run this app on iOS and Catalyst simulators as SkiaSharp is not currently supporting hardware-acceleration for them.

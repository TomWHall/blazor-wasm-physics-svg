# Blazor WebAssembly physics SVG

A PoC of 2D physics running in Blazor WebAssembly (WASM) rendered using SVG and CSS.

![Screenshot](https://tomwhall.github.io/blazor-wasm-physics-svg/blazor-wasm-physics-svg.png)

[View online](https://tomwhall.github.io/blazor-wasm-physics-svg)

### Objectives

* Run 2D physics in WebAssembly
* Implement the "game" logic in C#, with minimal JavaScript
* Minimal build process

### Build process

* Run npm install to install Node packages (just types for DotNet JS Interop)
* Run the project in your IDE of choice. TypeScript is compiled as part of the build.
* To publish, run this command from the project directory: dotnet publish -c Release -o ./publish

### Overview of flow

* On its first render, the Razor component hosting the SVG invokes the JS function startGameLoop, passing an interop reference to itself
* JS function startGameLoop stores the Razor component reference
* JS function starts a requestAnimationFrame loop
* requestAnimationFrame function invokes Razor component to "tick" the Game instance
* Razor component ticks the physics world forward by the time elapsed since last ticked
* Razor component registers a state change to cause a re-render
* Razor component renders the bodies within the physics world as shapes in its SVG

### Notes

* The demo version does default release mode trimming. You could probably get away with more aggressive trimming, at the risk of something not working.
* Instead of using native SVG attributes for specifying body coordinates and rotation, a CSS transform style on each node is used. This is to maximize the chance of the GPU being used, to speed up rendering.
* The SVG is scaled proportionally to fill the browser window. If you're running a very hi-res monitor, performance may vary.

### Credits

* Physics engine: https://www.nuget.org/packages/Aether.Physics2D
* SVG soccer ball: https://cutthatdesign.com
* Other SVG shapes: https://heropatterns.com

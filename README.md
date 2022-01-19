# GetFunkin.AdobeNecromancer
Re-animating the dead!

_GetFunkin.AdobeNecromancer_ is a FOSS (MIT-licensed) library for reading and processing atlas formats. Currently, Sparrow Atlas v2 and Spritesheet Packer formats are supported.

Alongside the library is a lightweight library for processing pre-implemented atlas processing.

## Library Usage
Get it from NuGet.

## CLI usage.
Included in the Releases tab is a CLI app for processing these atlases.

There are two parameters, `atlas-type` and `path`. `path` must always be specified, and should point to the `.xml`/`.txt` atlas file. `atlas-type` can either be `sparrow` (Adobe Animate, etc.) or `ssp` (SpriteSheetPacker).

When in doubt, `.xml` = `sparrow` and `.txt` = `ssp`.

The correct command line arguments for using the CLI app should look like this: `dotnet GetFunkin.AdobeNecromancer.CLI.dll --atlas-type sparrow --path senpai.xml`

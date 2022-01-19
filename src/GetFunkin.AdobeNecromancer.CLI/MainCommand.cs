using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using CliFx;
using CliFx.Attributes;
using CliFx.Infrastructure;
using GetFunkin.AdobeNecromancer.Atlases.Sparrow;
using GetFunkin.AdobeNecromancer.Atlases.SpriteSheetPacker;
using JetBrains.Annotations;

namespace GetFunkin.AdobeNecromancer.CLI
{
    [Command, UsedImplicitly]
    public class MainCommand : ICommand
    {
        [CommandOption("atlas-type", 'a', Description = "The atlas type to read. [sparrow|ssp]")]
        public string AtlasType { get; init; } = "sparrow";

        [CommandOption("path", 'p', Description = "The path to read from.", IsRequired = true)]
        public string Path { get; [UsedImplicitly] init; } = "a";

        public async ValueTask ExecuteAsync(IConsole console)
        {
            await console.Output.WriteLineAsync($"Using atlas type \"{AtlasType}\" with path: {Path}");

            string directory = System.IO.Path.GetFileNameWithoutExtension(Path);

            await console.Output.WriteLineAsync("Creating directory: " + directory);

            Directory.CreateDirectory(directory);

            await using Stream file = File.OpenRead(Path);

            switch (AtlasType)
            {
                case "sparrow":
                {
                    string image = Path;
                    List<ISparrowAtlasFrame> frames = new SparrowAtlasReader().ReadStream(file, ref image);

                    using Image png = Image.FromFile(image);
                    using Bitmap bitmap = new(png);

                    foreach (ISparrowAtlasFrame frame in frames)
                    {
                        bitmap.Clone(
                            frame.GetRect(),
                            bitmap.PixelFormat
                        ).Save(System.IO.Path.Combine(directory, $"{frame.Name}.png"));
                    }

                    break;
                }

                case "ssp":
                {
                    string image = Path;
                    List<ISpriteSheetPackerAtlasFrame> frames = new SpriteSheetPackerAtlasReader().ReadStream(
                        file,
                        ref image
                    );

                    using Image png = Image.FromFile(image);
                    using Bitmap bitmap = new(png);

                    foreach (ISpriteSheetPackerAtlasFrame frame in frames)
                    {
                        bitmap.Clone(
                            frame.GetRect(),
                            bitmap.PixelFormat
                        ).Save(System.IO.Path.Combine(directory, $"{frame.Name}.png"));
                    }

                    break;
                }

                default:
                    throw new ArgumentException("Incorrect atlas type specified.", nameof(AtlasType));
            }
        }
    }
}
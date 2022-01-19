using System.Drawing;

namespace GetFunkin.AdobeNecromancer.Atlases.SpriteSheetPacker
{
    public class SpriteSheetPackerAtlasFrame : ISpriteSheetPackerAtlasFrame
    {
        public string Name { get; init; }

        public int X => int.Parse(Region[0] ?? "0");

        public int Y => int.Parse(Region[1] ?? "0");

        public int Width => int.Parse(Region[2] ?? "0");

        public int Height => int.Parse(Region[3] ?? "0");

        public string[] Data { get; init; }

        public string[] Region { get; init; }

        public Rectangle GetRect() => new(X, Y, Width, Height);

        public Point GetSourceSize()
        {
            Rectangle rect = GetRect();

            return new Point(rect.Width, rect.Height);
        }

        // An empty constructor here *is* intentional.
        // https://github.com/HaxeFlixel/flixel/blob/27960e3b66c8ebad0052973831960761971168fd/flixel/graphics/frames/FlxAtlasFrames.hx#L379
        public Point GetOffset() => new();
    }
}
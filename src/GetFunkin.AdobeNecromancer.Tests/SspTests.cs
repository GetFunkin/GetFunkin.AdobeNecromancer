using System;
using System.Collections.Generic;
using System.IO;
using GetFunkin.AdobeNecromancer.Atlases.Sparrow;
using GetFunkin.AdobeNecromancer.Atlases.SpriteSheetPacker;
using NUnit.Framework;

namespace GetFunkin.AdobeNecromancer.Tests
{
    public static class SspTests
    {
        // Very primitive manual test I have to review.
        [Test]
        public static void SimpleManualReview()
        {
            using Stream resource = typeof(SparrowTests).Assembly.GetManifestResourceStream(
                "GetFunkin.AdobeNecromancer.Tests.spirit.txt"
            ) ?? throw new NullReferenceException("Missing resource.");

            string nonPermanent = "spirit.txt";
            List<ISpriteSheetPackerAtlasFrame> frames = new SpriteSheetPackerAtlasReader().ReadStream(
                resource,
                ref nonPermanent
            );

            Console.WriteLine("Got: " + nonPermanent);

            foreach (ISpriteSheetPackerAtlasFrame frame in frames)
                Console.WriteLine($"{frame.Name}: X-{frame.X} Y-{frame.Height} W-{frame.Width} H-{frame.Height}");
        }
    }
}
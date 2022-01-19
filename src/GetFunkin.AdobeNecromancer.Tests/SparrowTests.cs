using System;
using System.Collections.Generic;
using System.IO;
using GetFunkin.AdobeNecromancer.Atlases.Sparrow;
using NUnit.Framework;

namespace GetFunkin.AdobeNecromancer.Tests
{
    public static class SparrowTests
    {
        // Very primitive manual test I have to review.
        [Test]
        public static void SimpleManualReview()
        {
            using Stream resource = typeof(SparrowTests).Assembly.GetManifestResourceStream(
                "GetFunkin.AdobeNecromancer.Tests.senpai.xml"
            ) ?? throw new NullReferenceException("Missing resource.");

            string nonPermanent = "";
            List<ISparrowAtlasFrame> frames = new SparrowAtlasReader().ReadStream(resource, ref nonPermanent);

            Console.WriteLine("Got: " + nonPermanent);

            foreach (ISparrowAtlasFrame frame in frames)
                Console.WriteLine($"{frame.Name}: X-{frame.X} Y-{frame.Height} W-{frame.Width} H-{frame.Height}");
        }
    }
}
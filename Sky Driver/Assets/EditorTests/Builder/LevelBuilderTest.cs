using System.Collections;
using System.Collections.Generic;
using static SkyDriver.Builder.PlatformType;
using NUnit.Framework;
using System.Linq;
using UnityEngine;
using UnityEngine.TestTools;


namespace SkyDriver.Builder
{

    public class LevelBuilderTest
    {

        [Test, Timeout(5000), Description("Test LevelBuilder.ParseQueues")]
        public void TestParseQueues()
        {
            string[] simpleLevel = new string[]{
                "XFT SB*",
                "XFT SB*",
                "* BSTFX",
                "* BSTFX",
                "* BSTFX",
            };
            List<Queue<char>> queues = LevelBuilder.ParseQueues(simpleLevel);
            Assert.AreEqual("***XX", string.Join("", queues[0]));
            Assert.AreEqual("   FF", string.Join("", queues[1]));
            Assert.AreEqual("BBBTT", string.Join("", queues[2]));
            Assert.AreEqual("SSS  ", string.Join("", queues[3]));
            Assert.AreEqual("TTTSS", string.Join("", queues[4]));
            Assert.AreEqual("FFFBB", string.Join("", queues[5]));
            Assert.AreEqual("XXX**", string.Join("", queues[6]));
            

        }

        [Test, Timeout(5000), Description("Tests parsing from a column")]
        public void TestParsePlatform()
        {
            Queue<char> columnQueue = "***XX".ToQueue();
            List<Platform> platforms = LevelBuilder.ParseColumn(columnQueue, 0);
            Assert.AreEqual(2, platforms.Count);

            {
                Platform platform = new(Generic, 3, 0, 0);
                Assert.Contains(platform, platforms);
            }

            {
                Platform platform = new(Exit, 2, 0, 3);
                Assert.Contains(platform, platforms);
            }
        }

        [Test, Timeout(5000), Description("Tests LevelBuilder.ParsePlatforms")]    
        public void TestParsePlatforms()
        {
            List<Queue<char>> queues = new ()
            {
                "***XX".ToQueue(),
                "BBBFF".ToQueue(),
                "SSS  ".ToQueue(),
            };
            List<Platform> platforms = LevelBuilder.ParsePlatforms(queues);
            Assert.AreEqual(6, platforms.Count);

            // "***XX".ToQueue(),
            {
                Platform platform = new(Generic, 3, 0, 0);
                Assert.Contains(platform, platforms);
            }

            {
                Platform platform = new(Exit, 2, 0, 3);
                Assert.Contains(platform, platforms);
            }

            // "BBBFF".ToQueue(),
            {
                Platform platform = new(Boost, 3, 1, 0);
                Assert.Contains(platform, platforms);
            }

            {
                Platform platform = new(Fire, 2, 1, 3);
                Assert.Contains(platform, platforms);
            }

            // "SSS  ".ToQueue(),
            {
                Platform platform = new(Sticky, 3, 2, 0);
                Assert.Contains(platform, platforms);
            }

            {
                Platform platform = new(None, 2, 2, 3);
                Assert.Contains(platform, platforms);
            }

        }

        [Test, Timeout(5000), Description("Tests loading a simple string")]
        public void TestSimpleLoadFromString()
        {
            string simpleLevel = string.Join("\n", new string[]{
                "XFT SB*",
                "XFT SB*",
                "* BSTFX",
                "* BSTFX",
                "* BSTFX",
            });
            LevelBuilder result = LevelBuilder.LoadFromString(simpleLevel);
            Assert.AreEqual(14, result.Platforms.Count);

            {
                Platform platform = new(Generic, 3, 0, 0);
                Assert.Contains(platform, result.Platforms);
            }

            {
                Platform platform = new(None, 3, 1, 0);
                Assert.Contains(platform, result.Platforms);
            }

            {
                Platform platform = new(Boost, 3, 2, 0);
                Assert.Contains(platform, result.Platforms);
            }

            {
                Platform platform = new(Sticky, 3, 3, 0);
                Assert.Contains(platform, result.Platforms);
            }

            {
                Platform platform = new(Tunnel, 3, 4, 0);
                Assert.Contains(platform, result.Platforms);
            }

            {
                Platform platform = new(Fire, 3, 5, 0);
                Assert.Contains(platform, result.Platforms);
            }

            {
                Platform platform = new(Exit, 3, 6, 0);
                Assert.Contains(platform, result.Platforms);
            }

            // "XFT SB*",
            // "XFT SB*",

            {
                Platform platform = new(Exit, 2, 0, 3);
                Assert.Contains(platform, result.Platforms);
            }

            {
                Platform platform = new(Fire, 2, 1, 3);
                Assert.Contains(platform, result.Platforms);
            }

            {
                Platform platform = new(Tunnel, 2, 2, 3);
                Assert.Contains(platform, result.Platforms);
            }

            {
                Platform platform = new(None, 2, 3, 3);
                Assert.Contains(platform, result.Platforms);
            }

            {
                Platform platform = new(Sticky, 2, 4, 3);
                Assert.Contains(platform, result.Platforms);
            }

            {
                Platform platform = new(Boost, 2, 5, 3);
                Assert.Contains(platform, result.Platforms);
            }

            {
                Platform platform = new(Generic, 2, 6, 3);
                Assert.Contains(platform, result.Platforms);
            }

        }
    }
}

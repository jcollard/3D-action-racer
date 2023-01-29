using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

namespace SkyDriver.Builder
{

    [Serializable]
    public class LevelBuilder
    {
        [SerializeField]
        private List<Platform> _platforms;
        

        public LevelBuilder(List<Platform> platforms, int track)
        {
            _platforms = platforms.ToList();
            Track = track;
        }

        public List<Platform> Platforms => _platforms.ToList();
        public int Track { get; private set; }

        public void AddPlatforms(IEnumerable<Platform> platforms)
        {
            foreach (Platform platform in platforms)
            {
                _platforms.Add(platform);
            }
        }

        public static LevelBuilder LoadFromFile(string filename) => LoadFromTextAsset(Resources.Load<TextAsset>(filename));
        public static LevelBuilder LoadFromTextAsset(TextAsset levelData) => LoadFromString(levelData.text);
        public static LevelBuilder LoadFromString(string text)
        {
            string[] data = text.Split("\n").Select(t => t.TrimEnd()).ToArray();
            List<Queue<char>> columnQueues = ParseQueues(data[1..]);
            List<Platform> platforms = ParsePlatforms(columnQueues);
            return new LevelBuilder(platforms, int.Parse(data[0].Trim()));
        }

        public static List<Queue<char>> ParseQueues(string[] levelData)
        {
            int maxWidth = levelData.Select(s => s.Length).Max();
            List<Queue<char>> columnQueues = Enumerable.Range(0, maxWidth).Select(_ => new Queue<char>()).ToList();
            levelData = levelData.Reverse().ToArray();
            for (int row = 0; row < levelData.Length; row++)
            {
                for (int col = 0; col < maxWidth; col++)
                {
                    columnQueues[col].Enqueue(levelData[row].Length <= col ? ' ' : levelData[row][col]);
                }
            }
            return columnQueues;
        }

        public static List<Platform> ParsePlatforms(List<Queue<char>> columnQueues)
        {
            List<Platform> platforms = new ();
            for (int col = 0; col < columnQueues.Count; col++)
            {
                Queue<char> columnQueue = columnQueues[col];
                platforms.AddRange(ParseColumn(columnQueue, col));
            }
            return platforms;
        }

        public static List<Platform> ParseColumn(Queue<char> columnQueue, int column)
        {
            List<Platform> platforms = new();
            int startPosition = 0;
            while (columnQueue.Count > 0)
            {
                Platform toAdd = ParsePlatform(columnQueue, column, startPosition);
                startPosition += toAdd.Length;
                if (toAdd.Type == PlatformType.None) { continue; }
                platforms.Add(toAdd);
            }
            column++;
            return platforms;
        }

        private static Platform ParsePlatform(Queue<char> columnQueue, int column, int startPosition)
        {
            char ch = columnQueue.Peek();
            int count = 0;
            while (columnQueue.Count > 0 && columnQueue.Peek() == ch) 
            { 
                columnQueue.Dequeue();
                count++; 
            }
            PlatformType type = ch.AsPlatformType();
            return new Platform(type, count, column, startPosition);
        }
    }
}
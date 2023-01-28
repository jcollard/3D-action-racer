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

        public LevelBuilder(List<Platform> platforms)
        {
            _platforms = platforms.ToList();
        }

        public List<Platform> Platforms => _platforms.ToList();

        public void AddPlatforms(IEnumerable<Platform> platforms)
        {
            foreach (Platform platform in platforms)
            {
                _platforms.Add(platform);
            }
        }

        public static LevelBuilder LoadFromFile(string filename)
        {
            TextAsset textAsset = Resources.Load<TextAsset>(filename);
            return LoadFromString(textAsset.text);
        }

        public static LevelBuilder LoadFromString(string text)
        {
            List<Queue<char>> columnQueues = ParseQueues(text.Split("\n").Select(t => t.TrimEnd()).ToArray());
            List<Platform> platforms = ParsePlatforms(columnQueues);
            return new LevelBuilder(platforms);
        }

        public static List<Queue<char>> ParseQueues(string[] levelData)
        {
            List<Queue<char>> columnQueues = Enumerable.Range(0, 7).Select(_ => new Queue<char>()).ToList();
            levelData = levelData.Reverse().ToArray();
            for (int row = 0; row < levelData.Length; row++)
            {
                for (int col = 0; col < 7; col++)
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
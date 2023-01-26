using System.Collections.Generic;

namespace SkyDriver
{
    public static class QueueExtensions
    {
        public static Queue<char> ToQueue(this string toConvert)
        {
            Queue<char> columnQueue = new ();
            foreach (char ch in toConvert)
            {
                columnQueue.Enqueue(ch);
            }
            return columnQueue;
        }
    }
}
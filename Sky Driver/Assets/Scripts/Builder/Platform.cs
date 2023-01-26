
using System;
using UnityEngine;

namespace SkyDriver.Builder
{
    [Serializable]
    public class Platform
    {

        public Platform(PlatformType type, int length, int column, int startPosition)
        {
            Type = type;
            Length = length;
            Column = column;
            StartPosition = startPosition;
        }

        [field: SerializeField]
        public int Column { get; private set; }
        [field: SerializeField]
        public int StartPosition { get; private set; }
        [field: SerializeField]
        public int Length { get; private set;}
        [field: SerializeField]
        public PlatformType Type { get; private set;}

        public override bool Equals(object obj)
        {
            return obj is Platform platform &&
                   Column == platform.Column &&
                   StartPosition == platform.StartPosition &&
                   Length == platform.Length &&
                   Type == platform.Type;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Column, StartPosition, Length, Type);
        }
    }
}
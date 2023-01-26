
using System;

namespace SkyDriver.Builder
{
    public class Platform
    {

        public Platform(PlatformType type, int length, int column, int startPosition)
        {
            Type = type;
            Length = length;
            Column = column;
            StartPosition = startPosition;
        }

        public int Column { get; }
        public int StartPosition { get; }
        public int Length { get; }
        public PlatformType Type { get; }

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
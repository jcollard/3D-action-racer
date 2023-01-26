using static SkyDriver.Builder.PlatformType;

namespace SkyDriver.Builder
{
    public enum PlatformType
    {
        None,
        Generic,
        Boost,
        Sticky,
        Tunnel,
        Fire,
        Exit,
    }

    public static class PlatformTypExtensions
    {
        public static char AsSymbol(this PlatformType type)
        {
            return type switch
            {
                None => ' ',
                Generic => '*',
                Boost => 'B',
                Sticky => 'S',
                Tunnel => 'T',
                Fire => 'F',
                Exit => 'X',
                _ => throw new System.InvalidOperationException($"Invalid type {type}"),
            };
        }

        public static PlatformType AsPlatformType(this char type)
        {
            return type switch
            {
                ' ' => None,
                '*' => Generic,
                'B' => Boost,
                'S' => Sticky,
                'T' => Tunnel,
                'F' => Fire,
                'X' => Exit,
                _ => throw new System.InvalidOperationException($"Invalid character {type}"),
            };
        }
    }

}
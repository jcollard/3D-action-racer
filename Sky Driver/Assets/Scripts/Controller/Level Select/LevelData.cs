using SkyDriver.Builder;

namespace SkyDriver.Level
{
    public static class LevelData
    {
        private static LevelBuilder _currentBuilder;
        public static LevelBuilder CurrentBuilder 
        { 
            get
            {
                if (_currentBuilder == null)
                {
                    _currentBuilder = LevelBuilder.LoadFromFile("LevelData/DemoLevel");
                }
                return _currentBuilder;
            } 
            set => _currentBuilder = value; 
        }
    }
}
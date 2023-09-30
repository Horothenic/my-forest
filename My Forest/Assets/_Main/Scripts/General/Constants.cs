namespace MyForest
{
    public class Constants
    {
        public class Forest
        {
            public const string FOREST_DATA_KEY = "forest_data";
            public const string DEFAULT_FOREST_DATA_FILE = "DefaultForest";
        }

        public class Growth
        {
            public const string GROWTH_DATA_KEY = "growth_data";
        }

        public class Grid
        {
            public const string GRID_DATA_KEY = "grid_data";
            public const string DEFAULT_GRID_DATA_FILE = "DefaultGrid";
        }

        public class Camera
        {
            public const string CAMERA_DATA_KEY = "camera_data";
            public const float ROTATION_STEP_ANGLES = 45f;
            public const string FIRST_INTRO_KEY = "first_intro";
            public const string INTRO_KEY = "intro";
        }

        public class Formats
        {
            public const string HOUR_TIMER_FORMAT = "{2:00}:{1:00}:{0:00}";
            public const string MINUTE_TIMER_FORMAT = "{1:00}:{0:00}";
        }
    }
}

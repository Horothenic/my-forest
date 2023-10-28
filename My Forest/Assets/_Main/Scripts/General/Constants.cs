namespace MyForest
{
    public static class Constants
    {
        public static class Forest
        {
            public const string FOREST_DATA_KEY = "forest_data";
            public const string DEFAULT_FOREST_DATA_FILE = "DefaultForest";
        }

        public static class ForestElements
        {
            public const int MAX_ROTATION = 360;
            public const float SCALE_TRANSITION_TIME = 0.5f;
            public const float START_SCALE_FACTOR = 0.75f;
        }

        public static class Growth
        {
            public const string GROWTH_DATA_KEY = "growth_data";
        }

        public static class Camera
        {
            public const string CAMERA_DATA_KEY = "camera_data";
            public const float ROTATION_STEP_ANGLES = 45f;
        }

        public static class Formats
        {
            public const string HOUR_TIMER_FORMAT = "{2:00}:{1:00}:{0:00}";
            public const string MINUTE_TIMER_FORMAT = "{1:00}:{0:00}";
        }
    }
}

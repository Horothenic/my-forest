using System;

using Newtonsoft.Json;

namespace MyForest
{
    public enum AudioType
    {
        Music,
        Sound
    }

    [Serializable]
    public class AudioData
    {
        public float MusicVolume { get; private set; }
        public float SoundVolume { get; private set; }

        public AudioData() { }

        [JsonConstructor]
        public AudioData(float musicVolume, float soundVolume)
        {
            MusicVolume = musicVolume;
            SoundVolume = soundVolume;
        }

        public void SetVolume(AudioType type, float newValue)
        {
            switch (type)
            {
                case AudioType.Music:
                    MusicVolume = newValue;
                    break;
                case AudioType.Sound:
                    SoundVolume = newValue;
                    break;
            }
        }
    }
}

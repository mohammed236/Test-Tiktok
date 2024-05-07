using System.Collections.Generic;
using UnityEngine;
namespace Arab.Baking
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager instance { get; private set; }

        private void Awake()
        {
            instance = this;
            playedSoundDic = new Dictionary<Sound, float>();
        }

        [System.Serializable]
        public class SoundClip
        {
            public AudioClip audioClip;
            public Sound sound;
        }

        public enum Sound
        {
            Shoot
        }

        [SerializeField] private AudioSource audioSource;
        [SerializeField] private SoundClip[] clips = new SoundClip[0];
        [SerializeField] private Dictionary<Sound, float> playedSoundDic = new Dictionary<Sound, float>();

        public void PlaySound(Sound sound)
        {
            if (CanPlaySound(sound)){
                foreach (var clip in clips)
                {
                    if (clip.sound == sound)
                    {
                        audioSource.PlayOneShot(clip.audioClip);
                    }
                }
            }
            
        }
        private bool CanPlaySound(Sound sound)
        {
            switch (sound)
            {
                default: 
                    return true;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DillonsDream
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager instace;

        public AudioSource[] musicTracks;
        public AudioSource[] sfx;

        public int levelMusicToPlay;

        //private int currentTrack;
        void Awake()
        {
            instace = this;
        }

        // Update is called once per frame
        void Start()
        {
            PlayMusic(levelMusicToPlay);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.M))
            {

                
                PlaySoundEffects(Random.Range(0, 9));
            }
        }

        public void PlayMusic(int musicToPlay)
        {
            for (int i =0; i < musicTracks.Length; i++)
            {
                musicTracks[i].Stop();
            }
            musicTracks[musicToPlay].Play();
        }

        public void PlaySoundEffects(int sfxToPlay)
        {
            sfx[sfxToPlay].Play();
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using com.toni.mlin.Core;
using UnityEngine;

namespace com.toni.mlin
{
    public class SoundController : MonoController<SoundController>
    {
        [SerializeField] private AudioSource sfxSource;

        public void PlaySound(AudioClip sound)
        {
            this.sfxSource.PlayOneShot(sound);
        }
    }
}

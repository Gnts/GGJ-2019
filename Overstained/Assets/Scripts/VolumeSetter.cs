using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeSetter : MonoBehaviour
{

    public AudioMixer master;

    public void SetSoundMaster(float soundLevel)
    {
        master.SetFloat("masterVol", soundLevel);
    }

    public void SetSoundSfx(float soundLevel)
    {
        master.SetFloat("sfxVol", soundLevel);
    }

    public void SetSoundMusic(float soundLevel)
    {
        master.SetFloat("musicVol", soundLevel);
    }

    public void SetSoundHits(float soundLevel)
    {
        master.SetFloat("hitsVol", soundLevel);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioController : MonoBehaviour
{
    public GameObject sfxObj;
    public AudioClip action;

    public void PlayClipAction()
    {
        var obj = Instantiate(sfxObj, this.transform);
        var audioSource = obj.GetComponent<AudioSource>();
        audioSource.clip = action;
        audioSource.Play();
        Destroy(obj, 1f);
    }

    public void PlayClip(AudioClip clip)
    {
        var obj = Instantiate(sfxObj, this.transform);
        var audioSource = obj.GetComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.Play();
        Destroy(obj, 1f);
    }
}

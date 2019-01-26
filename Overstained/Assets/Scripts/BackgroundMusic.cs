using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public AudioClip[] clips;
    public AudioSource audioSource;
    public float timePlaying;
    public float maxTime = 3;
    public int index = 0;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        index = Random.Range(0, clips.Length-1);
        ChangeTune(index);
    }

    void Update()
    {
        timePlaying += Time.deltaTime;

        if (timePlaying > maxTime)
        {
            timePlaying = 0;
            index = GetNextIndex();
            ChangeTune(index);
        }
    }

    public void ChangeTune(int index)
    {
        audioSource.clip = clips[index];
        audioSource.Play();
    }

    public int GetNextIndex()
    {
        if (index == clips.Length - 1)
            return 0;

        return index + 1;
    }
}

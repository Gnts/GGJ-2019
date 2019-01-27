using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(AudioSource))]
public class ColisionAudio : MonoBehaviour
{
    AudioSource source;
    float resetTime = 0.1f;
    float timer = 0f;
    void Awake()
    {
        source = GetComponent<AudioSource>();
        source.playOnAwake = false;
        source.Stop();
    }

    void Update()
    {
        timer -= Time.deltaTime;
    }

    void OnEnable()
    {
        source = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (Time.timeSinceLevelLoad < 0.1f) return;
        if (timer > resetTime)
        {
            source.Stop();
            source.Play();
            timer = 0f;
        }
    }
}

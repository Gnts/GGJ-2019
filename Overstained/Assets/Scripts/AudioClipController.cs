using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioClipController : MonoBehaviour
{
    [HideInInspector]
    public static AudioClipController instance;
    public AudioClip dishesPickup;
    public AudioClip dishesDeliver;
    public AudioClip dishesDrop;

    void OnEnable()
    {
        instance = this;
    }

    void OnDisable()
    {
        instance = null;
    }

    public static AudioClip GetClip(ClipType clipType, Pickable.Type pickableType)
    {
        if(pickableType == Pickable.Type.Dishes)
        {
            if(clipType == ClipType.Deliver)
                return instance.dishesDeliver;
            if(clipType == ClipType.PickUp)
                return instance.dishesPickup;
            if(clipType == ClipType.Drop)
                return instance.dishesDrop;
        }

        throw new Exception("Audio clip not implemented: " + pickableType + '-' + clipType);
    }

}

public enum ClipType
{
    PickUp,
    Drop,
    Deliver
}
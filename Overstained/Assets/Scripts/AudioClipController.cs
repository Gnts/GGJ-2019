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

    public AudioClip clothesPickup;
    public AudioClip clothesDeliver;
    public AudioClip clothesDrop;

    public AudioClip trashPickup;
    public AudioClip trashDeliver;
    public AudioClip trashDrop;

    public AudioClip actionClip;

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
        if (pickableType == Pickable.Type.Dishes)
        {
            if (clipType == ClipType.Deliver)
                return instance.dishesDeliver;
            if (clipType == ClipType.PickUp)
                return instance.dishesPickup;
            if (clipType == ClipType.Drop)
                return instance.dishesDrop;
        }

        if (pickableType == Pickable.Type.Clothes)
        {
            if (clipType == ClipType.Deliver)
                return instance.clothesDeliver;
            if (clipType == ClipType.PickUp)
                return instance.clothesPickup;
            if (clipType == ClipType.Drop)
                return instance.clothesDrop;
        }

        if (pickableType == Pickable.Type.Trash)
        {
            if (clipType == ClipType.Deliver)
                return instance.trashDeliver;
            if (clipType == ClipType.PickUp)
                return instance.trashPickup;
            if (clipType == ClipType.Drop)
                return instance.trashDrop;
        }

        if (pickableType == Pickable.Type.None)
        {
            return instance.actionClip;
        }

        Debug.LogWarning("Audio clip not implemented: " + pickableType + '-' + clipType);
        return instance.actionClip;
    }

}

public enum ClipType
{
    PickUp,
    Drop,
    Deliver
}
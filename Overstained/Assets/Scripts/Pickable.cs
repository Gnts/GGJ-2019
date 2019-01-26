using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Pickable : MonoBehaviour
{
    public enum Type
    {
        Trash,
        Dishes,
        Clothes,
        None
    }

    [SerializeField]
    public Type type;
    [SerializeField]
    public int score;
}



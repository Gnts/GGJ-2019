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
        Dishes
    }

    [SerializeField]
    public Type type;
}



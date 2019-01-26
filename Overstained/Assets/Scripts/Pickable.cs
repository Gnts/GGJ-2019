using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Pickable : MonoBehaviour
{
    public static List<Pickable> pickables = new List<Pickable>();

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
    public string displayName;

    public void OnEnable()
    {
        if(type == Type.None) return;

        pickables.Add(this);
    }

    public void OnDisable()
    {
        if(type == Type.None) return;

        pickables.Remove(this);
    }
}



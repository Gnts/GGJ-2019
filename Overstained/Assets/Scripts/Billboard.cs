using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform camT;
    void OnEnable()
    {
        camT = Camera.main.transform;
    }

    void Update()
    {
        transform.LookAt(transform.position + camT.rotation * Vector3.forward, camT.rotation * Vector3.up);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    private void OnCollisionEnter(Collision col)
    {
        KillObject(col.gameObject);
    }

    private void OnCollisionStay(Collision col)
    {
        KillObject(col.gameObject);
    }

    private void OnCollisionExit(Collision col)
    {
        KillObject(col.gameObject);
    }

    private void KillObject(GameObject obj)
    {
        Destroy(obj);
        Debug.Log("[KillZone] Destroyed: " + obj.name);
    }
}

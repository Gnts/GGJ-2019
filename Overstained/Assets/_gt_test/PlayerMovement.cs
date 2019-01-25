using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private Transform t;
    [SerializeField]
    private float speed = 7f;

    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        t = transform;
    }

    void Update()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");

        var move = new Vector3(horizontal, 0, vertical) * speed * Time.deltaTime;
        rb.MovePosition(t.position + move);
    }
}

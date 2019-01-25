using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private Transform t;
    [SerializeField]
    private float speed = 7f;
    [SerializeField]
    private float interactionOffset = 1f;
    [SerializeField]
    private LayerMask hitLayer;
    private Vector3 currentdirection = Vector3.zero;

    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        t = transform;
    }

    void Update()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");

        var direction = new Vector3(horizontal, 0, vertical);

        if (!(horizontal == 0 && vertical == 0))
            currentdirection = direction;


        var move = direction * speed * Time.deltaTime;
        rb.MovePosition(t.position + move);

        if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Space))
        {
            Interact();
        }

    }

    void Interact()
    {
        Debug.Log("[Player] Interact!");
        var castCenter = t.position + currentdirection + Vector3.up;


        var hits = Physics.BoxCastAll(castCenter, new Vector3(1.5F, 2, 1.5F)/2f, transform.forward, transform.rotation, 0.01f, hitLayer);
        if (hits.Length > 0)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Interaction hits: ");
            foreach (var hit in hits)
            {
                sb.Append(hit.collider.name + ', ');
            }
            Debug.Log(sb);
        }
    }

    void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawCube(t.position + currentdirection + Vector3.up, new Vector3(1.5F, 2, 1.5F));
        }
    }
}


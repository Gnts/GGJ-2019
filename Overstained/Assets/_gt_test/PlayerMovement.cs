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
    [SerializeField]
    private CharacterJoint grabJoint;
    private Vector3 currentdirection = Vector3.forward;
    [HideInInspector]
    public GameObject target;

    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        t = transform;
    }

    void FixedUpdate()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");

        var direction = new Vector3(horizontal, 0, vertical);

        if (!(horizontal == 0 && vertical == 0))
            currentdirection = direction;

        grabJoint.transform.position = t.position + currentdirection + Vector3.up * 2;

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

        if (target != null)
        {
            DropTarget();
            return;
        }

        var castCenter = t.position + currentdirection + Vector3.up;
        var hits = Physics.BoxCastAll(castCenter, new Vector3(1.5F, 2, 1.5F) / 2f, transform.forward, transform.rotation, 0.01f, hitLayer);
        if (hits.Length > 0)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Interaction hits: ");
            foreach (var hit in hits)
            {
                TakeTarget(hit);
                sb.Append(hit.collider.name + ", ");
            }
            Debug.Log(sb);
        }
    }

    void DropTarget()
    {
        grabJoint.connectedBody = null;
        target = null;
    }

    void TakeTarget(RaycastHit hit)
    {
        grabJoint.connectedBody = hit.rigidbody;
        target = hit.collider.gameObject;

    }

    void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(t.position + currentdirection + Vector3.up, new Vector3(1.5F, 2, 1.5F));
        }
    }
}


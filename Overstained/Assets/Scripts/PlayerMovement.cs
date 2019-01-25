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
    public Pickable target;

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
        var hits = GetHits();

        if (target != null)
        {

            foreach (var hit in hits)
            {
                var dp = hit.collider.GetComponent<DeliveryPoint>();
                if (dp != null && dp.type == target.type)
                {
                    DeliverTarget(dp);
                    return;
                }
            }

            DropTarget();
            return;
        }

        foreach (var hit in hits)
        {
            if (hit.collider.GetComponent<Pickable>() != null)
                TakeTarget(hit);
        }

        if (hits.Length > 0)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Interaction hits: ");
            foreach (var hit in hits)
            {
                if (hit.collider.GetComponent<Pickable>() != null)
                    TakeTarget(hit);
                sb.Append(hit.collider.name + ", ");
            }
            Debug.Log(sb);
        }
    }

    RaycastHit[] GetHits()
    {
        var castCenter = t.position + currentdirection + Vector3.up;
        var hits = Physics.BoxCastAll(castCenter, new Vector3(1.5F, 2, 1.5F) / 2f, transform.forward, transform.rotation, 0.01f, hitLayer);

        StringBuilder sb = new StringBuilder();
        sb.Append("Interaction hits: ");
        foreach (var hit in hits)
            sb.Append(hit.collider.name + ", ");
        Debug.Log(sb);

        return hits;
    }

    void DropTarget()
    {
        Debug.Log("[Player] Drop item: " + target.name);
        grabJoint.connectedBody = null;
        target = null;
    }

    void TakeTarget(RaycastHit hit)
    {
        Debug.Log("[Player] Take item: " + hit.collider.name);
        grabJoint.connectedBody = hit.rigidbody;
        target = hit.collider.GetComponent<Pickable>();
    }

    void DeliverTarget(DeliveryPoint dp)
    {
        Debug.Log("[Player] Delivered current target of type: " + dp.type);
        var targetObject = target.gameObject;
        // @TODO - award points

        grabJoint.connectedBody = null;
        target = null;
        Destroy(targetObject);
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


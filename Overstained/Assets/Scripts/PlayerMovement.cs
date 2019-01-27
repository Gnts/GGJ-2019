using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private Transform t;
    private PlayerAudioController audio;
    private AudioSource stepsSource;

    [SerializeField]
    private Transform model;

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
        rb = GetComponent<Rigidbody>();
        t = transform;
        audio = GetComponent<PlayerAudioController>();
        stepsSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");

        var direction = new Vector3(horizontal, 0, vertical);

        if (!(horizontal == 0 && vertical == 0))
        {
            currentdirection = direction;

            RotateCharacter(currentdirection);
            if (!stepsSource.isPlaying)
                stepsSource.Play();
        }
        else
        {
            if (stepsSource.isPlaying)
                stepsSource.Stop();
        }

        grabJoint.transform.position = t.position + currentdirection + Vector3.up * 2;

        float diagonalModifier = 1;
        if (horizontal == 1 && vertical == 1)
            diagonalModifier = diagonalModifier * 0.707f;

        var velocity = direction * speed * diagonalModifier;
        rb.velocity = velocity;

        if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Space))
        {
            Interact();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }

    void RotateCharacter(Vector3 direction)
    {
        model.rotation = Quaternion.LookRotation(direction);
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
        var playerCenter = t.position + Vector3.up;
        var hits = Physics.BoxCastAll(castCenter, new Vector3(3F, 3, 3F) / 2f, transform.forward, transform.rotation, 0.01f, hitLayer);
        var hits2 = Physics.BoxCastAll(playerCenter, new Vector3(2.6F, 3, 2.6F) / 2f, transform.forward, transform.rotation, 0.01f, hitLayer);

        var list = hits.ToList();
        list.AddRange(hits2);
        hits = list.ToArray();

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
        var clipToPlay = AudioClipController.GetClip(ClipType.Drop, target.type);
        audio.PlayClip(clipToPlay);
        grabJoint.connectedBody = null;
        target = null;
    }

    void TakeTarget(RaycastHit hit)
    {
        Debug.Log("[Player] Take item: " + hit.collider.name);
        grabJoint.connectedBody = hit.rigidbody;
        target = hit.collider.GetComponent<Pickable>();
        target.transform.position = transform.position + Vector3.up * 2 + currentdirection;
        var clipToPlay = AudioClipController.GetClip(ClipType.PickUp, target.type);
        audio.PlayClip(clipToPlay);
    }

    void DeliverTarget(DeliveryPoint dp)
    {

        Debug.Log("[Player] Delivered current target of type: " + dp.type);
        var targetObject = target.gameObject;
        var clipToPlay = AudioClipController.GetClip(ClipType.Deliver, target.type);
        audio.PlayClip(clipToPlay);
        ScoreController.AddScore(target.score);
        grabJoint.connectedBody = null;
        target = null;
        Destroy(targetObject);
    }

    void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(t.position + currentdirection + Vector3.up, new Vector3(3F, 2, 3F));
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(t.position + Vector3.up, new Vector3(2.6F, 2, 2.6F));
        }
    }
}


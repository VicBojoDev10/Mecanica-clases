using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField]
    private string StickTag;

    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(StickTag))
        {
            rb.isKinematic = true;
        }
        else
        {
            rb.useGravity = false;
        }
    }

  public void MoveWithVelocity(Vector3 Velocity)
    {
        rb.velocity = Velocity;
    }
}

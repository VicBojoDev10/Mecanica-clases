using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBehaviour3D : MonoBehaviour
{
    public float speed;
    private Rigidbody rb;
    private Vector3 direction, bounceVelocity;
    // Start is called before the first frame update
    void Start()
    {
        StartMovement();
    }

    // Update is called once per frame
    void Update()
    {
        direction = rb.velocity.normalized;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Barrier"))
        {
            ContactPoint contact = collision.GetContact(0);
            Vector3 normal = (contact.normal).normalized;
            Vector3 velocity = speed * direction;
            Vector3 tangent = velocity - Vector3.Dot(normal,velocity)* normal;
            tangent.Normalize();

            float Vt = Vector3.Dot(tangent, velocity);
            float Vn = Vector3.Dot(normal, velocity);
            bounceVelocity = Vt * tangent - Vt * normal;
            rb.velocity = bounceVelocity;
        }
    }

    void StartMovement()
    {
        rb = GetComponent<Rigidbody>();
        Vector3 randomVector = Random.onUnitSphere;
        rb.velocity = speed * randomVector;
    }
}

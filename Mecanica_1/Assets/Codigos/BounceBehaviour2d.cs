using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBehaviour2d : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    private Vector2 direction, bounceVelocity;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Barrier2D"))
        {
            ContactPoint2D contact = collision.GetContact(0);
            Vector2 normal = (contact.normal).normalized;
            Vector2 tangent = RotateVector(normal, -90f);

            Vector2 velocity = speed * direction;
            float Vx = Vector2.Dot(tangent,velocity);
            float Vy = Vector2.Dot(normal,velocity);
            bounceVelocity = Vx * tangent - Vy * normal;
            rb.velocity = bounceVelocity;
        }
    }
    Vector2 RotateVector(Vector2 vector, float angle)
    {
        angle *= Mathf.Deg2Rad;
        float Vx = vector.x;
        float Vy = vector.y;
        float newVx = Vx * Mathf.Cos(angle) - Vy * Mathf.Sin(angle);
        float newVy = Vx * Mathf.Sin(angle) + Vy * Mathf.Cos(angle);
        return new Vector2(newVx, newVy);
    }
    void StartMovement()
    {
        rb = GetComponent<Rigidbody2D>();
        Vector2 randomVector = Random.onUnitSphere;
        rb.velocity = speed * (randomVector.normalized);
    }
}

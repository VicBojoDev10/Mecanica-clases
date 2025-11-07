using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class ShellBehaviour : MonoBehaviour
{
    public float speed;

    public float Shellspeed = 10f;
    private Rigidbody rb;
    private Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
        StartMovement();
    }
    private void OnCollisionEnter(Collision collision)
    {
        // Rebote con jugadores
        if (collision.collider.CompareTag("FoosBallPlayer"))
        {
            ReflectOnCollision(collision);
        }

        // Rebote con paredes
        if (collision.collider.CompareTag("Wall"))
        {
            ReflectOnCollision(collision);
        }
    }
    void StartMovement()
    {
        int xDir = Random.Range(0, 2) == 0 ? -1 : 1; // izquierda o derecha
        float zDir = Random.Range(-0.5f, 0.5f);      // pequeño ángulo vertical en Z
        direction = new Vector3(xDir, 0, zDir).normalized;
        rb.velocity = direction * speed;
    }
    void ReflectOnCollision(Collision collision)
    {
        Vector3 normal = collision.contacts[0].normal;
        direction = Vector3.Reflect(direction, normal).normalized;
        rb.velocity = direction * speed;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GoalLeft"))
        {
            GameManagerFoosBall.Instance.GoalLeft();
        }
        else if (other.CompareTag("GoalRight"))
        {
            GameManagerFoosBall.Instance.GoalRight();
        }
    }
}

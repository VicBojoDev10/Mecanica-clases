using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador3 : MonoBehaviour
{
    public float jumpImpulse, gravity;
    private bool grounded;
    private Rigidbody rb;
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Physics.gravity = new Vector3(0, gravity, 0);
        if (grounded && Input.GetKeyDown(KeyCode.L))
            rb.AddForce(jumpImpulse * transform.up, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            grounded = true;
        }


    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            grounded = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cuerda"))
        {
            Debug.Log("¡El Jugador 3 fue eliminado!");
            Destroy(Player, 1f);
        }
    }
}

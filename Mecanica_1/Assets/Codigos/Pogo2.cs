using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pogo2 : MonoBehaviour
{
    public GameObject player;
    public float moveForce = 8f;
    public float jumpImpulse = 12f;
    public string hInputName = "Horizontal";
    public string vInputName = "Vertical";

    private Rigidbody rb;
    private bool isAlive = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (!isAlive) return;

        float hInput = Input.GetAxis(hInputName);
        float vInput = Input.GetAxis(vInputName);

        Vector3 direction = new Vector3(hInput, 0, vInput).normalized;
        rb.AddForce(moveForce * direction, ForceMode.Force);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Base"))
        {
            rb.AddForce(jumpImpulse * transform.up, ForceMode.Impulse);
        }
        else if (collision.collider.CompareTag("ZonaMuerte"))
        {
            Muerte();
        }
    }

    void Muerte()
    {
        Destroy(player);
        isAlive = false;
        rb.velocity = Vector3.zero;
        gameObject.SetActive(false);
        GameManagerPogo.instance.CheckPlayersAlive();
    }
}

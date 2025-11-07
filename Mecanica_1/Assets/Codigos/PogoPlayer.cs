using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PogoPlayer : MonoBehaviour
{
    public float forceImpulse, jumpImpulse;
    public string hInputName = "PogoHorizontal";
    public string vInputName = "PogoVertical";
    public float gravity;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        Physics.gravity = gravity * Vector3.up;
        float hInput = Input.GetAxis(hInputName);
        float vInput = Input.GetAxis(vInputName);
        Vector3 direction = new Vector3(hInput, 0 , vInput).normalized;
        rb.AddForce(forceImpulse * direction * Time.deltaTime, ForceMode.Force);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Base"))
        {
            rb.AddForce(forceImpulse * transform.up, ForceMode.Impulse);
        }
    }
}

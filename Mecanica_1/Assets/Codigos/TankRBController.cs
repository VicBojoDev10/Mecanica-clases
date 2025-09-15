using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankRBController : MonoBehaviour
{
    public Rigidbody body;
    public float forceBody, torqueBody, torqueTurret;
    public float linearDRag, angularDrag;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalWASD = Input.GetAxis("HorizontalWASD");
        float verticalWASD = Input.GetAxis("VerticalWASD");

        float horizontalArrows = Input.GetAxis("HorizontalArrows");

        float dt = Time.deltaTime;
        Vector3 force = verticalWASD * forceBody *transform.forward * dt;
        body.AddForce(force);

        Vector3 torque = horizontalWASD * torqueBody * transform.up * dt;
        body.AddTorque(torque);

        //body.linearDrag = linearDrag;
        //body.angularDrag
    }
}

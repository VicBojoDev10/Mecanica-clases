using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikedWheel : MonoBehaviour
{
    public float speed;
    public float radius = 1f;
    // Start is called before the first frame update
    void Start()
    {
        float dt = Time.fixedDeltaTime;
        Vector3 velocity = speed * Vector3.right;
        float angularVelocity = (speed / radius) * Mathf.Rad2Deg;

        transform.Translate(velocity * dt, Space.World);
        transform.Rotate(0,0, -angularVelocity * dt, Space.Self);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

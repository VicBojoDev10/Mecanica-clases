using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationBase : MonoBehaviour
{
    public float maxSpeed, slerp;
    private float speed;
    public Transform[] engines;

    // Update is called once per frame
    void Update()
    {
        PlatformRotation();
        RotateEngines();
    }

    void PlatformRotation()
    {
        float dt = Time.deltaTime;
        float hInput = Input.GetAxis("Horizontal");
        speed = maxSpeed * hInput;
        Vector3 eulers = new Vector3(0f, speed * dt, 0f);
        transform.Rotate(eulers, Space.Self);   
    }
    void RotateEngines()
    {
        float dt = Time.deltaTime;
        float targetAngle = -90f * speed / maxSpeed;
        Quaternion targetRotation = Quaternion.Euler(targetAngle, 0f, 0f);
        foreach(Transform engine in engines)
        {
            engine.localRotation = Quaternion.Slerp(engine.localRotation, targetRotation, slerp * dt);
        }
    }    
}

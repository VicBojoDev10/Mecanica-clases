using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularMovement : MonoBehaviour
{
    public float heightParameter = 0.1f;
    public float restLength = 10;
    public float theta0;
    public float omega0;
    public float alpha;
    private float radius;
    private Vector3 center;
    private float t;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Vector3 positionFunction()
    {
        float x = center.x + radius * Mathf.Cos(Theta());
        float y = center.y + radius * Mathf.Sin(Theta());
        float z = center.z;
        return new Vector3(x, y, z);
    }

    float Theta()
    {

    }

    float Profile(float z)
    {

    }
}


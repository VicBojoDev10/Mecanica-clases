using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicOrbit : MonoBehaviour
{
    public float mayorSemiAxis, minorSemiAxis;
    public float period, phase;
    private float time;
    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = PositionFunction(time);
        GetComponent<TrailRenderer>().Clear();
    }

    private void FixedUpdate()
    {
        time += Time.fixedDeltaTime;
        transform.localPosition = PositionFunction(time);
    }
    Vector3 PositionFunction(float t)
    {
        float a = mayorSemiAxis;
        float b = minorSemiAxis;
        float T = period;
        float phi = phase;
        float pi = Mathf.PI;
        float x = a * Mathf.Cos(2 * pi * t / T + phi);
        float z = b * Mathf.Sin(2 * pi * t / T + phi);
        return new Vector3(x, 0, z);
    }

}

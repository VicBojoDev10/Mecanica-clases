using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiroParabolico : MonoBehaviour
{
    float t;

    public Vector3 P0, V0;

    Vector3 g = new Vector3(0, -9.8f, 0);
    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        transform.position = PositionFunction(t);
    }
    Vector3 PositionFunction(float time)
    {
        return 0.5f * g * time * time + V0 * time + P0;
    }
}

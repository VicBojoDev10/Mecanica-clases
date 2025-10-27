using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeMovement : MonoBehaviour
{
    public float swing;
    public Transform fireBalls;
    private BezierCruve _beziercurve;
    private float time;
    // Start is called before the first frame update
    void Start()
    {
        _beziercurve = GetComponent<BezierCruve>();
    }

    private void FixedUpdate()
    {
        ControlPointsMovement();
        FireBallMovement();
    }
    void FireBallMovement()
    {
        for(int i = 0; i < fireBalls.childCount; i++)
        {
            float si = (float)i / (fireBalls.childCount - 1f);
            fireBalls.GetChild(i).position = _beziercurve.Bezier(si);
        }
    }
    private void ControlPointsMovement()
    {
        float z1 = _beziercurve.P[1].position.z;
        float z2 = _beziercurve.P[2].position.z;
        _beziercurve.P[1].position = CirclePath(5f, z1);
        _beziercurve.P[2].position = CirclePath(5f, z2);
        time += Time.fixedDeltaTime;
    }
    Vector3 CirclePath(float radius, float zCoordinate)
    {
       float xCoordinate = radius * Mathf.Sin(swing * time);
        float yCoordinate = radius * Mathf.Cos(swing * time);
        return new Vector3(xCoordinate, yCoordinate, zCoordinate);
    }
   }

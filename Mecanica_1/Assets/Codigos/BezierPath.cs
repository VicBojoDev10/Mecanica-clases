using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierPath : MonoBehaviour
{
    public BezierCurve bezierCurve;
    [Range(0f, 1f)] public float time;

    // Update is called once per frame
    void Update()
    {
        transform.position = bezierCurve.Bezier(time);
        Quaternion rotation = Quaternion.LookRotation(TangentVector(time), BinormalVector(time));
        transform.rotation = rotation;
    }
    Vector3 TangentVector(float t)
    {
        return bezierCurve.BezierDerivative(t).normalized;
    }
    Vector3 BinormalVector(float t)
    {
       Vector3 bezierDerivative = bezierCurve.BezierDerivative(t);
        Vector3 bezierSecondDerivative = bezierCurve.BezierDerivative(t);
        Vector3 crossProduct = Vector3.Cross(bezierDerivative, bezierSecondDerivative);
        return crossProduct.normalized;
    }
}

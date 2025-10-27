using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class BezierCruve : MonoBehaviour
{
    [System.NonSerialized]
    public List<Transform> P = new List<Transform>();
    private int curvepoints = 50;
    private int n;
    // Start is called before the first frame update
    void Start()
    {
        InitCurve();
    }

    // Update is called once per frame
    void Update()
    {
        SampleCurve();
    }

    private int Factorial(int n)
    {
        int result = 1;
        for(int k = 1; k <= n; k++)
        {
            result *= k;
        }
        return result;
    }
    private int Binomial(int n, int i)
    {
        int result = Factorial(n) / (Factorial(i) * Factorial(n-i));
        return result;
    }
    private float PolBern(int n, int i, float s)
    {
        float result = Binomial(n, i) * Mathf.Pow(1f - s, n - i) * Mathf.Pow(s, i);
        return result;
    }
    public Vector3 Bezier(float s)
    {
        Vector3 result = Vector3.zero;
        for (int i = 0; i <= n; i++)
        {
            result += PolBern(n, i, s) * P[i].position;
        }
        return result;
    }
    public Vector3 BezierDerivative(float s)
    {
        Vector3 result= Vector3.zero;
        for (int i = 0; i <= n - 1; i++)
        {
            result += PolBern(n - 1, i, s) * (P[i + 1].position - P[i].position);
        }
        return n * result;
    }
    public void InitCurve()
    {
        foreach (Transform child in transform)
        {
            P.Add(child);
        }
        n = P.Count - 1;
        GetComponent<LineRenderer>().positionCount = curvepoints;
        GetComponent<LineRenderer>().widthMultiplier = 0.25f;
    }
    public void SampleCurve()
    {
        for(int i = 0; i < curvepoints; i++)
        {
            float s = (float)i / (float)(curvepoints - 1);
            GetComponent<LineRenderer>().SetPosition(i, Bezier(s));
        }
    }
}

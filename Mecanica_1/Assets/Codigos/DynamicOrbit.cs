using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicOrbit : MonoBehaviour
{
    [Header("Constante de Gravitacion:")]

    public float G;
    [Header("Establece las condiciones indiciones:")]
    public Vector3 P0;
    public Vector3 V0;
    [Header("Constantes de Movimiento: NO MODIFICAR")]
    public float energy;
    public float eccentricity;
    public float majorSemiAxis;
    public float minorSemiAxis;
    public float period;
    [Header("Marca/Desmarca para Iniciar/Pausar")]
    public bool isActive = false;

    private Vector3 Pf, Vf, angularMomentun, laplaceRungeLenz;

    private void FixedUpdate()
    {
       if(!isActive)
       {
            transform.localPosition = P0;
            Debug.DrawRay(transform.position, V0, Color.green);
            transform.GetComponent<TrailRenderer>().Clear();
            calculateOrbitParameters();
       }
       if(isActive && energy < 0)
       {
            float dt = Time.fixedDeltaTime;

            Vector3 k1 = V0;
            Vector3 l1 = a(P0);
            Vector3 k2 = V0 + 0.5f * l1 * dt;
            Vector3 l2 = a(P0 + 0.5f * k1 * dt);
            Vector3 k3 = V0 + 0.5f * l2 * dt;
            Vector3 l3 = a(P0 + 0.5f * k2 * dt);
            Vector3 k4 = V0 + l3 * dt;
            Vector3 l4 = a(P0 + k3 * dt);

            Pf = P0 + dt * (k1 + 2 * k2 + 2 * k3 + k4) / 6f;
            Vf = V0 + dt * (l1 + 2 * l2 + 2 * l3 + l4) / 6f;

            transform.localPosition = Pf;
            P0 = Pf;
            V0 = Vf;
       }
    }

    Vector3 a(Vector3 p)
    {
        return -G * p / Mathf.Pow(p.magnitude, 3);
    }
    void calculateOrbitParameters()
    {
        energy = 0.5f * Vector3.Dot(V0, V0) - G / P0.magnitude;
        angularMomentun = Vector3.Cross(P0, V0);
        laplaceRungeLenz = Vector3.Cross(angularMomentun, V0) + G * P0 / P0.magnitude;
        eccentricity = laplaceRungeLenz.magnitude / G;
        majorSemiAxis = -G / (2 * energy);
        minorSemiAxis = majorSemiAxis * Mathf.Sqrt(1 - eccentricity * eccentricity);
        period = 2 * Mathf.PI * Mathf.Sqrt(Mathf.Pow(majorSemiAxis, 3) / 6);
    }
}

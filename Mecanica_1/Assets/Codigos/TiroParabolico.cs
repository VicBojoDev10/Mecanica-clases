using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class Disparos3 : MonoBehaviour
{
    float t;

    public Vector3 P0, V0;

    Vector3 g = new Vector3(0, -9.8f, 0);

    private Rigidbody rb;

    public Enemy enemyL;

    // Update is called once per frame

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        t += Time.deltaTime;
        transform.position = PositionFunction(t);
    }
    Vector3 PositionFunction(float time)
    {
        return 0.5f * g * time * time + V0 * time + P0;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent<Enemy>(out Enemy enemycomponent))
        {
            enemycomponent.daño(1);
        }

        if (collision.gameObject.tag == "Enemy")
        {
            enemyL = collision.gameObject.GetComponent<Enemy>();
            enemyL.Vidas = enemyL.Vidas - 1;
            Destroy(gameObject);
        }
    }
}

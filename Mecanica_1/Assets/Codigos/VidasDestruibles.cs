using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidasDestruibles : MonoBehaviour
{
    public float Vidas, vidasmaximas = 2f;
    public GameObject explosion;
    public GameObject enemy;
    private Transform target;
    // Start is called before the first frame update
    void Start()
    {
        Vidas = vidasmaximas;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void daño(float damageamount)
    {
        Vidas -= damageamount;

        if (Vidas <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Vector3 direction = target.position - transform.position;
            direction.y = 0;

            GameObject Explosion = Instantiate(explosion);

            Destroy(explosion, 1.5f);
        }
    }
}

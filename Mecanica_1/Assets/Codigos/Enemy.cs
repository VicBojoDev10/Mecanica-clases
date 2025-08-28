using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Vidas, vidasmaximas = 2f;
    private Transform target;
    public float speed;
    public GameObject explosion;
    public GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Turret").GetComponent<Transform>();
        Vidas = vidasmaximas;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
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
    GameObject Explosion = Instantiate(explosion);
            
    Destroy(explosion);
    }
}
}

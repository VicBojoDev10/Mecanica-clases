using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanBehaviour : MonoBehaviour
{
    public Transform target1;
    public Transform target2;
    public KeyCode Ladoizq;
    public KeyCode Ladoder;
    public bool bombaenelsarten = false;
    public float flyingTime;
    
    // Update is called once per frame
    void Update()
    {
        Jugadores();

    }
    public void EnableCollider()
    {
        bool isEnabled = GetComponent<BoxCollider>().enabled;
        GetComponent<BoxCollider>().enabled = !isEnabled;
        bombaenelsarten |= isEnabled;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bomb"))
        {
            if (bombaenelsarten == true)
            {
                Vector3 P0 = other.transform.position;
                Vector3 Pf = target1.position;
                Vector3 g = Physics.gravity;
                float T = flyingTime;
                Vector3 hitVelocity = (Pf - P0) / T - 0.5f * g * T;

                Vector3 randomTorque = 100f * Random.onUnitSphere;
                other.GetComponent<Rigidbody>().velocity = hitVelocity;
                other.GetComponent<Rigidbody>().AddTorque(randomTorque, ForceMode.Impulse);
            }

          
        }
    }
    private void Jugadores()
    {
        if (Input.GetKeyDown(KeyCode.S) && CompareTag("Jug1"))
        {
            GetComponent<Animator>().SetTrigger("Move");

        }
        if (Input.GetKeyDown(KeyCode.K) && CompareTag("Jug2"))
        {
            GetComponent<Animator>().SetTrigger("Move");

        }
        if (Input.GetKeyDown(KeyCode.B) && CompareTag("Jug3"))
        {
            GetComponent<Animator>().SetTrigger("Move");


        }
    }
}

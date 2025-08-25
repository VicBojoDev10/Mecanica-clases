using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanBehaviour : MonoBehaviour
{
    public Transform target;
    public KeyCode movePan;
    public float flyingTime;
    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(movePan))
        {
            GetComponent<Animator>().SetTrigger("Move");
        }
    }
    public void EnableCollider()
    {
        bool isEnabled = GetComponent<BoxCollider>().enabled;
        GetComponent<BoxCollider>().enabled = isEnabled;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Bomb"))
        {
            Vector3 P0 = other.transform.position;
            Vector3 Pf = target.position;
            Vector3 g = Physics.gravity;
            float T = flyingTime;
            Vector3 hitVelocity = (Pf - P0) / T - 0.5f * g * T;

            Vector3 randomTorque = 100f * Random.onUnitSphere;
            other.GetComponent<Rigidbody>().velocity = hitVelocity;
            other.GetComponent<Rigidbody>().AddTorque(randomTorque, ForceMode.Impulse);
        }
    }
}

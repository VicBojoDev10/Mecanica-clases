using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PracticeObjective : MonoBehaviour
{
    public GameObject Objetivo;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Flecha"))
        {
            Debug.Log("Objetivo Destruido");
            Destroy(Objetivo);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private Spawn spawnPrefab;

    [SerializeField]
    private float SpawnDurationInseconds = 2f;

    private SpawnShooter spawnShooter;
    // Start is called before the first frame update
    void Start()
    {
        spawnShooter = GetComponent<SpawnShooter>();
        NewSpawn();
    }
    public void NewSpawn()
    {
        spawnShooter.ChangeCurrentSpawn(Instantiate(spawnPrefab.gameObject, transform.position, transform.rotation).GetComponent<Spawn>());

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Spawn>())
        {
            Invoke(nameof(NewSpawn), SpawnDurationInseconds);
        }
    }
}

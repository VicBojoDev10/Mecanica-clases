using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnShooter : MonoBehaviour
{
    [SerializeField]
    private float FlightDurationInSeconds = 2f;

    private Spawn currentSpawn;
    private Camera mainCamera;
    private bool isShot;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }
    public void ChangeCurrentSpawn(Spawn NewSpawn)
    {
        currentSpawn = NewSpawn;
        isShot = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!isShot)
            return;

            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit))
            {
                shootWithVelocity(hit.point);
            }

        }
    }
    private void shootWithVelocity(Vector3 targetPosition)
    {
        currentSpawn.MoveWithVelocity((targetPosition - currentSpawn.transform.position) / FlightDurationInSeconds);
        isShot = true;
    }
}

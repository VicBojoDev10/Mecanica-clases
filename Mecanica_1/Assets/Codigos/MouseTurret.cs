using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTurret : MonoBehaviour
{
    public float rotationSpeed, bulletSpeed;
    public Transform yAxis, xAxis, shootpoint;
    public RectTransform crossHair;
    public GameObject bulletPrefab;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;
        float yRotation = dt * rotationSpeed * Input.GetAxis("Mouse X");
        float xRotation = dt * rotationSpeed * Input.GetAxis("Mouse Y");
        yAxis.Rotate(Vector3.up, yRotation, Space.Self);
        xAxis.Rotate(Vector3.right, -xRotation, Space.Self);

        float zScreen = 500f;
        float z0= shootpoint.position.z;
        float vz0 = (bulletSpeed * shootpoint.forward).z;
        float T = (zScreen - z0) / vz0;

        crossHair.position = PositionFunction(T);

        if(Input.GetMouseButtonDown(0))
        {
            GameObject bullet = Instantiate(bulletPrefab, shootpoint.position, shootpoint.rotation);
            bullet.GetComponent<Rigidbody>().velocity = bulletSpeed * shootpoint.forward;
            Destroy(bullet,4.5f);

            PositionFunction(T);

        }
    }

    Vector3 PositionFunction(float t)
    {
        Vector3 P0 = shootpoint.position;
        Vector3 V0 = bulletSpeed * shootpoint.forward;
        Vector3 g = new Vector3(0f, -9.81f, 0f);
        return 0.5f * g * t * t + V0 * t + P0;
    }
}

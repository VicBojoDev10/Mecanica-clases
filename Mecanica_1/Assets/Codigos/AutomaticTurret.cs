using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticTurret : MonoBehaviour
{
    public Transform target;
    public Transform turretAxisY;
    public Transform turretAxisX;
    public Transform shootPoint;
    public GameObject bulletPrefab;
    public RectTransform crossHair;
    public float rotSpeed, bulletSpeed;
    private float V0;
    private float g = 9.8f;
    public GameObject Enemy;

    private void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        float dt = Time.deltaTime;
        float yRotation = dt * rotSpeed * Input.GetAxis("Mouse Y");
        float xRotation = dt * rotSpeed * Input.GetAxis("Mouse X");
        turretAxisY.Rotate(Vector3.up, yRotation, Space.Self);
        turretAxisX.Rotate(Vector3.up, -xRotation, Space.Self);

        float zScreen = 500f;
        float z0 = shootPoint.position.z;
        float vz0 = (bulletSpeed * shootPoint.forward).z;
        float T = (zScreen - z0) / vz0;

        crossHair.position = PositionFunction(T);

        TurretRotation();
        AimRotation();

        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }

    void Fire()
    {
        Vector3 p0 = shootPoint.position;
        Quaternion r0 = shootPoint.rotation;
        Vector3 v0 = V0 * shootPoint.forward;
        GameObject bullet = Instantiate(bulletPrefab, p0, r0);
        bullet.GetComponent<Disparos3>().P0 = p0;
        bullet.GetComponent<Disparos3>().V0 = v0;
        if (CompareTag("Enemy"))
        {
            Destroy(Enemy);
        Destroy(bullet, 30f);
        }
    }

    void TurretRotation()
    {
        float dt = Time.deltaTime;
        Quaternion newRotation = Quaternion.LookRotation(TargetDirection(), Vector3.up);
        turretAxisY.localRotation = Quaternion.Slerp(turretAxisY.localRotation, newRotation, rotSpeed * dt);
    }

    void AimRotation()
    {
        Vector2 angles = Angles();
        turretAxisX.localRotation = Quaternion.Euler(-angles.x, 0, 0);
    }

    Vector3 TargetDirection()
    {
        Vector3 direction = target.position - transform.position;
        direction.y = 0;
        return direction;
    }

    Vector2 Angles()
    {
        Vector2 delta = Delta();
        float dx = delta.x;
        float dy = delta.y;
        float tanA = dy / dx;
        float secA = Mathf.Sqrt(1 + tanA * tanA);
        V0 = 1 + Mathf.Sqrt(g * dx * (tanA + secA));

        float U = V0 * V0 / (dx * g);
        float w1 = U + Mathf.Sqrt(U * U - 2 * tanA * U - 1);
        float w2 = U - Mathf.Sqrt(U * U - 2 * tanA * U - 1);

        float angle1 = Mathf.Rad2Deg * Mathf.Atan(w1);
        float angle2 = Mathf.Rad2Deg * Mathf.Atan(w2);
        return new Vector2(angle1, angle2);
    }

    Vector2 Delta()
    {
        Vector3 relativePosition = target.position - shootPoint.position;
        Vector2 relativePosition2D = new Vector2(relativePosition.x, relativePosition.z);

        float dx = relativePosition2D.magnitude;
        float dy = relativePosition.y;
        return new Vector2(dx, dy);
    }

    Vector3 PositionFunction(float t)
    {
        Vector3 P0 = shootPoint.position;
        Vector3 V0 = bulletSpeed * shootPoint.forward;
        Vector3 g = new Vector3(0f, -9.81f, 0f);
        return 0.5f * g * t * t + V0 * t + P0;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GetComponent<VidasDestruibles>();

        }
    }
}

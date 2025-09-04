using UnityEngine;

public class PlayerTurret : MonoBehaviour
{
    public Transform turret, barrel;
    public float rotSpeed, bulletSpeed, bulletLifeSpan;
    public Transform shootPoint;
    public GameObject bulletPrefab;

    public Transform crossHair;

    private float angleX;

    void Update()
    {
        TurretRotation();
        BarrelRotation();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }

        Vector3 g = new Vector3(0, -9.8f, 0);
        Vector3 P0 = shootPoint.position;
        Vector3 V0 = bulletSpeed * shootPoint.forward;
        float T = FlyingTime();
        Vector3 crossHairPos = 0.5f * g * T * T + V0 * T + P0;
        crossHair.position = crossHairPos;
    }

    void TurretRotation()
    {
        float dt = Time.deltaTime;
        float hInput = Input.GetAxis("Horizontal");
        float angle = rotSpeed * hInput * dt;
        Vector3 eulerAngles = new Vector3(0, angle, 0);
        turret.Rotate(eulerAngles, Space.Self);
    }

    void BarrelRotation()
    {
        float dt = Time.deltaTime;
        float vInput = Input.GetAxis("Vertical");
        angleX -= rotSpeed * vInput * dt;
        angleX = Mathf.Clamp(angleX, -90f, 0f);
        barrel.localRotation = Quaternion.Euler(angleX, 0, 0);
    }

    void Fire()
    {
        Vector3 position = shootPoint.position;
        Quaternion rotation = shootPoint.rotation;
        GameObject bullet = Instantiate(bulletPrefab, position, rotation);
        bullet.GetComponent<Disparos3>().P0 = shootPoint.position;
        bullet.GetComponent<Disparos3>().V0 = bulletSpeed * shootPoint.forward;
        Destroy(bullet, bulletLifeSpan);
    }

    float FlyingTime()
    {
        float y0 = shootPoint.position.y;
        float Vy0 = bulletSpeed * (shootPoint.forward.y);
        float g = 9.8f;
        return (Vy0 + Mathf.Sqrt(Vy0 * Vy0 + 2 * g * y0)) / g;
    }



}


using UnityEngine;

public class CircularMovement : MonoBehaviour
{
    public float heightParameter = 0.1f;
    public float restLength = 10;
    public float theta0;
    public float omega0;
    public float alpha;
    private float radius;
    private Vector3 center;
    private float t;
    // Start is called before the first frame update
    void Start()
    {
        center = transform.localPosition;
        transform.position = positionFunction();
        radius = Profile(transform.localPosition.z);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = positionFunction();
        t += Time.deltaTime;
    }

    Vector3 positionFunction()
    {
        float x = center.x + radius * Mathf.Cos(Theta());
        float y = center.y + radius * Mathf.Sin(Theta());
        float z = center.z;
        return new Vector3(x, y, z);
    }

    float Theta()
    {
        float result = 0.5f * alpha * t * t + omega0 * t + theta0;
        return (Mathf.PI / 180f) * result;
    }

    float Profile(float z)
    {
        return heightParameter * z * (restLength - z);
    }
}


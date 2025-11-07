using UnityEngine;

public class FoosBallPlayer : MonoBehaviour
{
    public string verticalInputName;
    public float forceMagnitude, damping;
    public Rigidbody cubeRb;
    private float verticalInput;

    // Update is called once per frame
    void Update()
    {
        cubeRb.maxLinearVelocity = damping;

        verticalInput = Input.GetAxisRaw(verticalInputName);
        Vector3 force = -forceMagnitude * Vector3.right * verticalInput;
        cubeRb.AddForce(force, ForceMode.Impulse);
    }
}

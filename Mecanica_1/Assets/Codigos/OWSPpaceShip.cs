using System.ComponentModel;
using UnityEngine;

public class OWSPpaceShip : MonoBehaviour
{
    public float torqueMagnitude;

    [Range(0f, 5f)] public float angularDamping;

    public float forceMagnitude;

    [Range(0f, 5f)] public float linearDamping;

    private Rigidbody rb;
    private Vector2 inputRS, inputLS;
    private float valueRS, valueLS;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        inputRS = Get_RightStick_Input();
        inputLS = Get_LeftStick_input();
        valueRS = Get_RightTrigger_Input();
        valueLS = Get_LeftTrigger_Input();
    }
    void FixedUpdate()
    {
        ModifyRBParameters();
        ApplyTorque();
        ApplyForce();
    }
    void ApplyTorque()
    {
        if(!Input.GetButton("LB"))
        {
            Vector3 normalizedTorque = (-inputRS.y * transform.right + inputRS.x * transform.up).normalized;
            Vector3 torque = torqueMagnitude * normalizedTorque;
            rb.AddTorque(torque, ForceMode.Force);
        }
        else
        {
            Vector3 normalizedTorque = -inputRS.x * transform.forward;
            Vector3 torque = -torqueMagnitude * normalizedTorque;
            rb.AddTorque(torque, ForceMode.Force);
        }
    }
    void ApplyForce()
    {
        Vector3 leftRightDirection = inputLS.x * transform.right;
        Vector3 frontRearDirection = inputLS.y * transform.forward;
        Vector3 upDownDirection = (valueRS - valueLS) * transform.up;

        Vector3 force = forceMagnitude * (leftRightDirection + frontRearDirection + upDownDirection).normalized;

        rb.AddForce(force, ForceMode.Force);
    }
    void ModifyRBParameters()
    {
        rb.angularDrag = angularDamping;
        rb.maxLinearVelocity = linearDamping;
    }
    Vector2 Get_RightStick_Input()
    {
        float x = Input.GetAxis("Horizontal-RS");
        float y = Input.GetAxis("Vertical-RS");
        return new Vector2(x, y);
    }
    Vector2 Get_LeftStick_input()
    {
        float x = Input.GetAxis("Horizontal-LS");
        float y = Input.GetAxis("Vertical-LS");
        return new Vector2 (x, y);
    }
    float Get_LeftTrigger_Input()
    {
        float x = Input.GetAxis("LT");
        return x;
    }
    float Get_RightTrigger_Input()
    {
        float x = Input.GetAxis("RT");
        return x;
    }
}

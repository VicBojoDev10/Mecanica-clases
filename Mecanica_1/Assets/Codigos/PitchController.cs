using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitchController : MonoBehaviour
{
    public float flyingTime;
    public Transform shootPoint, target;
    public GameObject ballPrefab;

    public void ThrowBall()
    {
        GameObject ball = Instantiate(ballPrefab,shootPoint.position, Quaternion.identity);
        Vector3 g = Physics.gravity;
        Vector3 hitVelocity = (target.position - shootPoint.position) / flyingTime - 0.5f * g * flyingTime;
        ball.GetComponent<Rigidbody>().velocity = hitVelocity;
        Destroy(ball, 10f);
    }
 
}

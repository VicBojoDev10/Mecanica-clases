using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovBarrita : MonoBehaviour
{
    public float speed = 10f;
    public float boundary = 8f; // Límite izquierdo/derecho de la pantalla

    void Update()
    {
        float move = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        transform.Translate(move, 0, 0);

        // Limitar el movimiento dentro de los límites
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -boundary, boundary);
        transform.position = pos;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class radar_movement : MonoBehaviour
{
    Rigidbody rb;
    public float movement_speed = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 moveDirection = Vector3.zero;

        // WASD ve Q/E için hareket yönlerini topla
        if (Input.GetKey(KeyCode.W))
            moveDirection += transform.forward;
        if (Input.GetKey(KeyCode.S))
            moveDirection -= transform.forward;
        if (Input.GetKey(KeyCode.A))
            moveDirection -= transform.right;
        if (Input.GetKey(KeyCode.D))
            moveDirection += transform.right;
        if (Input.GetKey(KeyCode.Q))
            moveDirection += transform.up;
        if (Input.GetKey(KeyCode.E))
            moveDirection -= transform.up;

        // Eðer tuþla yön verilmediyse, Axis inputlarýna bak
        if (moveDirection == Vector3.zero)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            moveDirection = new Vector3(h, 0, v);
        }

        // Normalize ederek sabit hýz uygula
        rb.velocity = moveDirection.normalized * movement_speed;
    }

}

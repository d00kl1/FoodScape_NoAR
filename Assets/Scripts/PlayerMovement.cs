using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//#define USE_RIGID_BODY

public class PlayerMovement : MonoBehaviour
{
    public float speed  = 5;
    public float rotSpeed = 720;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        #if USE_RIGID_BODY
        rb = GetComponent<Rigidbody>();
        #endif
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movementDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        movementDirection.Normalize();

        #if USE_RIGID_BODY
            rb.velocity = movementDirection * speed;
        #else
            transform.Translate(movementDirection * speed * Time.deltaTime, Space.World);
        #endif

        if (movementDirection != Vector3.zero) {
            Quaternion toRot = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRot, rotSpeed * Time.deltaTime);
        }
    }
}

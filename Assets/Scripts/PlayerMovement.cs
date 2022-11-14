using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float launchSpeed = 150.0f;

    public float turnSpeed = 2.0f;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Manipulate Transform component
        // if (Input.GetKey(KeyCode.W)) 
        // {
        //     transform.position += transform.up * Time.deltaTime;
        // }

        // float directionInput = Input.GetAxis("Horizontal");
        // if (directionInput < 0)
        // {
        //     transform.Rotate(Vector3.forward * Time.deltaTime * 100);
        // }
        // else if (directionInput > 0)
        // {
        //     transform.Rotate(Vector3.back * Time.deltaTime * 100);
        // }    
    }

    void FixedUpdate()
    {
        float forwardInput = Input.GetAxis("Vertical");
        if (forwardInput > 0)
        {
            rb.AddForce(transform.up * launchSpeed * Time.fixedDeltaTime);
        }

        float directionInput = Input.GetAxis("Horizontal");
        if (directionInput != 0)
        {
            rb.angularVelocity = -directionInput * turnSpeed;
        }
    }
}

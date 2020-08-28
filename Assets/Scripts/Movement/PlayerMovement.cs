using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    public Camera camera;

    private Rigidbody rb;

    public float speed;
    public float maxAcceleration = 10f;

    private Vector3 velocity;
    public float maxSpeed = 10f;

    private Vector3 newPositon;
    // v = v0 + aT 
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        velocity = rb.velocity;
    }

    private void Update()
    {
        HandleInput();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit))
            {
                newPositon = hit.point;
                transform.LookAt(hit.point);
                transform.rotation = new Quaternion( 0f, transform.rotation.y, 0f, transform.rotation.w );
            }
    
        }
    }

    private void HandleMovement()
    {
        if (Vector3.Distance(transform.position, newPositon) > 1f)
        {
            Vector3 desiredVelocity = (newPositon - transform.position).normalized * speed;
            velocity = rb.velocity;
            float maxSpeedChange = maxAcceleration * Time.deltaTime;
            
            velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
            velocity.z = Mathf.MoveTowards(velocity.z, desiredVelocity.z, maxSpeedChange);
            rb.velocity = velocity;
        }
        else
        {
            rb.velocity = new Vector3(0f, rb.velocity.y, 0f);
        }
    }
}

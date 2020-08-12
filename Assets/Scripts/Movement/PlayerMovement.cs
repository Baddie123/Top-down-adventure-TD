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

    // Update is called once per frame
    void FixedUpdate()
    {
        HandleInput();
        HandleMovement();
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit))
            {
                Debug.DrawRay(hit.point, Vector3.up * 10f, Color.green);
                newPositon = hit.point;
                Debug.Log(hit.point);
            }
            else
            {
                Debug.Log("Raycast hit nothing");
            }
        }
    }

    private void HandleMovement()
    {
        if (Vector3.Distance(transform.position, newPositon) > 1f)
        {
            Debug.DrawLine(newPositon, transform.position, Color.blue, 1f);
            Vector3 desiredVelocity = (newPositon - transform.position).normalized * speed;
            velocity = rb.velocity;
            float maxSpeedChange = maxAcceleration * Time.deltaTime;
            
            velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
            velocity.z = Mathf.MoveTowards(velocity.z, desiredVelocity.z, maxSpeedChange);
            Debug.Log(maxSpeedChange);
            rb.velocity = velocity;
        }
        else
        {
            rb.velocity = new Vector3(0f, rb.velocity.y, 0f);
        }
    }
}

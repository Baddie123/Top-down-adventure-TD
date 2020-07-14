using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb = null;
    public Camera camera;
    public float speed;
    
    public float acceleration;
    private float maxAcceleration = 1f;
    private Vector3 velocity;
    public float stoppingDistance = 0.5f;
    private Vector3 newPosition;

    public VisualEffect clickEffect;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        newPosition = rb.position;

    }

    // Update is called once per frame
    void Update()
    {
        velocity = rb.velocity;

        if (Input.anyKey)
        {
            HandleInput();
        }


        
    }

    private void FixedUpdate()
    {
        
        if (Vector3.Distance(rb.position, newPosition) > stoppingDistance)
        {
            Debug.Log("updating velocity:");
            Vector3 direction = (newPosition - transform.position).normalized * speed * Time.deltaTime;

            Vector3 pos = Vector3.Lerp(transform.position, newPosition, speed * Time.deltaTime);
            rb.MovePosition(transform.position + direction);
            transform.LookAt(new Vector3(newPosition.x, transform.position.y, newPosition.z));
        }
        
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                newPosition = hit.point;
                Debug.Log(newPosition);
            }
            
        }
    }
}

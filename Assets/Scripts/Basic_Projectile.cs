using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic_Projectile : MonoBehaviour
{
    public float velocity;

    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        if (rb == null)
        {
            rb = gameObject.GetComponent<Rigidbody>();
        }
        rb.velocity = transform.forward * velocity;
    }

}

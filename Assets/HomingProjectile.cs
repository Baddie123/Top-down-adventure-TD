using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingProjectile : MonoBehaviour
{
    public float speed = 5f;

    public Rigidbody rb;

    private Transform target = null;

    public float rotationSpeed;

    public ParticleSystem explosionEffect;

    public float radius = 5f;

    public float explosionForce = 100f;

    private Vector3 mPos;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.forward * speed * Time.deltaTime;
    }

    private void Update()
    {
        if (!target)
        {
            Collider[] targets = Physics.OverlapSphere(transform.position, radius * 2, LayerMask.GetMask("Enemies"));
            float distance = Single.MaxValue;

            foreach (var t in targets)
            {
                float temp = Vector3.Distance(t.gameObject.transform.position, transform.position);
                if (temp < distance)
                {
                    distance = temp;
                    target = t.gameObject.transform;
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (target)
        {
            Vector3 direction = target.position - transform.position;
            //rb.velocity = direction.normalized * speed * Time.deltaTime;
            rb.velocity = transform.forward * speed * Time.deltaTime;
            Quaternion lookRotation = Quaternion.LookRotation(direction.normalized);

            //rotate us over time according to speed until we are in the required rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Explode();
            Destroy(gameObject);
        }
    }

    private void Explode()
    {
        Debug.Log("Explode");
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Collider[] nearby = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider col in nearby)
        {
            Rigidbody rb = col.GetComponent<Rigidbody>();
            if (rb)
            {
                rb.AddExplosionForce(explosionForce, transform.position, radius);
            }
        }
        
    }
}

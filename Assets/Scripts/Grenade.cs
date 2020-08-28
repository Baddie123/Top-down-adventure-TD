using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float startingVelocity;
    
    [SerializeField] 
    private Rigidbody rb;
    
    public float radius;
    public float explosionForce;
    
    public float dischargeTimer = 2f;
    private float startDischargeTimer;

    [SerializeField] 
    private ParticleSystem explosionEffect;
    // Start is called before the first frame update
    void Start()
    {
        startDischargeTimer = dischargeTimer;
        if (rb == null)
        {
            rb = gameObject.GetComponent<Rigidbody>();
        }
        rb.AddForce(transform.forward * startingVelocity);
    }

    // Update is called once per frame
    private void OnTriggerStay(Collider other)
    {
        if (dischargeTimer < 0f)
        {
            Explode();
            dischargeTimer = startDischargeTimer;
            Destroy(gameObject);

        }
        else
        {
            dischargeTimer -= Time.deltaTime;
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

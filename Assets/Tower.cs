using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class Tower : MonoBehaviour
{
    
    public GameObject projectile;
    private int projectileIndex = 0;
    
    public Transform bulletStart;

    public float rof = 2f;
    private float startRof;
    
    // Start is called before the first frame update
    void Start()
    {
        startRof = rof;
    }

    // Update is called once per frame
    void Update()
    {
        if (rof <= 0f)
        {
            Fire();
            rof = startRof;
        }
        else
        {
            rof -= Time.deltaTime;
        }
    }

    void Fire()
    {
        Instantiate(projectile, bulletStart.position, Quaternion.Euler(Vector3.up));
    }
}

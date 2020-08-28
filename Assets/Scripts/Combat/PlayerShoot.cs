using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{

    public GameObject[] projectiles;
    private int projectileIndex = 0;
    
    
    public Camera camera;

    public GameObject player;

    public Transform bulletStart;


    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit))
            {
                Instantiate(projectiles[projectileIndex], bulletStart.transform.position, player.transform.rotation);
            }
            else
            {
                Debug.Log("Raycast hit nothing");
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            projectileIndex = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            projectileIndex = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            projectileIndex = 2;
        }
    }

}

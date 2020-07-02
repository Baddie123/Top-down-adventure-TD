using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField]
    private HexGrid grid;

    public GameObject player;
    


    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            HandleInput();
        }
    }
    
    void HandleInput()
    {
        
        Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(inputRay, out hit)) {
            TouchCell(hit.point);
        }
   
    }
    
        
    void TouchCell (Vector3 position) {
        position = transform.InverseTransformPoint(position);
        HexCoordinates coordinates = HexCoordinates.FromPosition(position);
        player.transform.position = new Vector3();
    }
}

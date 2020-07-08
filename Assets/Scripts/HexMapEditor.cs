using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexMapEditor : MonoBehaviour
{
    // Variables //
	public Color[] colors;
	public HexGrid hexGrid;
	private Color activeColor;


    // Unity Functions //
	void Awake () {
		SelectColor(0);
	}

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleInput(); 
        }
    }

    void HandleInput()
    {
        Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(inputRay, out hit)) {
            hexGrid.TouchCell(hit.point, activeColor);
        }
    }

    // Public Functions //
    public void SelectColor (int index) {
		activeColor = colors[index];
	}
}

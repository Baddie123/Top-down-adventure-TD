using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexMapEditor : MonoBehaviour
{
    // Variables //
	public Color[] colors;
	public HexGrid hexGrid;

	private Color activeColor;
    private int activeElevation;


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


    // Public Functions //
    public void SelectColor (int index) {
		activeColor = colors[index];
	}

    public void SetElevation (float elevation) {
		activeElevation = (int)elevation;
	}


    // Private Functions //
    void HandleInput()
    {
        Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(inputRay, out hit)) {
            EditCell(hexGrid.GetCell(hit.point));
        }
    }

    void EditCell (HexCell cell) {
		cell.color = activeColor;
        cell.elevation = activeElevation;
		hexGrid.Refresh();
	}
}

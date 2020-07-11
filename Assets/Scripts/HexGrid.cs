using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGrid : MonoBehaviour
{
    // Variables //
    public int width;
    public int height;

    public HexCell cellPrefab;
    public HexCell[] cells;
    private HexMesh hexMesh;

    public Color defaultColor = Color.white;
    public Color touchedColor = Color.magenta;


    // Unity Functions
    private void Awake()
    {
        hexMesh = GetComponentInChildren<HexMesh>();
        cells = new HexCell[width * height];

        for (int z = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                CreateCell(x, z);
            }
        }
        
    }

    void Start () {
        hexMesh.Triangulate(cells);
    }


    // Public Functions //
    public void TouchCell (Vector3 position, Color color) {
        position = transform.InverseTransformPoint(position);
        HexCoordinates coordinates = HexCoordinates.FromPosition(position);
        Debug.Log("touched at " + coordinates.ToString());
        
        //Find index of the cell in the array, change its color, and re-triangulate it
        int index = coordinates.X + coordinates.Z * width + coordinates.Z / 2;
		HexCell cell = cells[index];
		cell.color = color;
		hexMesh.Triangulate(cells);
    }

    // Private Functions //
    void CreateCell(int x, int z)
    {
        Vector3 position;
        position.x = (x + z * 0.5f - z / 2) * (HexMetrics.innerRadius * 2f);
        position.y = 0f;
        position.z = z *  (HexMetrics.outerRadius * 1.5f);

        HexCell cell = cells[x + z * width] = Instantiate<HexCell>(cellPrefab);
        cell.transform.SetParent(transform, false);
        cell.transform.localPosition = position;
        cell.coordinates = HexCoordinates.FromOffsetCoordinates(x,z);
        cell.color = defaultColor;

        //Connect cells to their Left/Right neighbors
        if (x > 0) {
			cell.SetNeighbor(HexDirection.W, cells[(x + z * width) - 1]);
		}
        //Connect cells to their lower right/left neighbors
		if (z > 0) {
			if ((z & 1) == 0) {
				cell.SetNeighbor(HexDirection.SE, cells[(x + z * width) - width]);
				if (x > 0) {
					cell.SetNeighbor(HexDirection.SW, cells[(x + z * width) - width - 1]);
				}
			}
			else {
				cell.SetNeighbor(HexDirection.SW, cells[(x + z * width) - width]);
				if (x < width - 1) {
					cell.SetNeighbor(HexDirection.SE, cells[(x + z * width) - width + 1]);
				}
			}
		}
    }
}

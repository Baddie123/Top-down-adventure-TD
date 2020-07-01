using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGrid : MonoBehaviour
{
    public int width;

    public int height;

    public HexCell cellPrefab;

    public HexCell[] cells;

    private HexMesh hexMesh;
    
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
    
    
    void CreateCell(int x, int z)
    {
        Vector3 position;
        
        position.x = (x + z * 0.5f - z / 2) * (HexMetrics.innerRadius * 2f);
        position.y = 0f;
        position.z = z *  (HexMetrics.outerRadius * 1.5f);

        HexCell cell = cells[x + z * width] = Instantiate<HexCell>(cellPrefab);
        cell.transform.SetParent(transform, false);
        cell.transform.localPosition = position;
    }
}

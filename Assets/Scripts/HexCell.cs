using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class HexCell : MonoBehaviour
{
    public HexCoordinates coordinates;
    public Color color;
    private Mesh hexCell;
    private List<Vector3> vertices;
    private List<int> triangles;
    private MeshCollider meshCollider;
    public MeshRenderer meshRenderer;
    
    void Awake () {
        GetComponent<MeshFilter>().mesh = hexCell = new Mesh();
        hexCell.name = "Hex Cell Mesh";
        vertices = new List<Vector3>();
        triangles = new List<int>();

        if (GetComponent<MeshCollider>() == null)
        {
            meshCollider = gameObject.AddComponent<MeshCollider>();   
        }
        else
        {
            meshCollider = GetComponent<MeshCollider>();
        }

        meshRenderer = GetComponent<MeshRenderer>();
//        Triangulate(this);
    }

//    void Triangulate (HexCell cell) {
//        hexCell.Clear();
//        vertices.Clear();
//        triangles.Clear();
//
//        Vector3 center = cell.transform.localPosition;
//        for (int i = 0; i < 6; i++)
//        {
//            AddTriangle(
//                center,
//                center + HexMetrics.corners[i],
//                center + HexMetrics.corners[i+1]
//            );
//        }
//        
//        hexCell.vertices = vertices.ToArray();
//        hexCell.triangles = triangles.ToArray();
//        hexCell.RecalculateNormals();
//        meshCollider.sharedMesh = hexCell;
//    }
//
//
//    // Private/Helper Functions //
//    void AddTriangle (Vector3 v1, Vector3 v2, Vector3 v3) {
//        int vertexIndex = vertices.Count;
//        vertices.Add(v1);
//        vertices.Add(v2);
//        vertices.Add(v3);
//        triangles.Add(vertexIndex);
//        triangles.Add(vertexIndex + 1);
//        triangles.Add(vertexIndex + 2);
//    }

}

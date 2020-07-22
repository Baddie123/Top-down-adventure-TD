using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class HexCell : MonoBehaviour
{
    // Variables //
    public HexCoordinates coordinates;
    public Color color;
    public int elevation;
    public const float elevationStep = 5f;

    Mesh hexCell;
    List<Vector3> vertices;
    List<int> triangles;
    List<Color> colors;
    MeshCollider meshCollider;
    [SerializeField] HexCell[] neighbors;

    
    // Unity Functions //
    void Awake () {
        GetComponent<MeshFilter>().mesh = hexCell = new Mesh();
        hexCell.name = "Hex Cell Mesh";
        vertices = new List<Vector3>();
        triangles = new List<int>();
        colors = new List<Color>();
        meshCollider = gameObject.AddComponent<MeshCollider>();
    }


    // Public Functions //
    public HexCell GetNeighbor (HexDirection direction) {
		return neighbors[(int)direction];
	}

    public void SetNeighbor (HexDirection direction, HexCell cell) {
		neighbors[(int)direction] = cell;
        cell.neighbors[(int)direction.Opposite()] = this;
	}


    // Private/Helper Functions //
    void AddTriangle (Vector3 v1, Vector3 v2, Vector3 v3) {
        int vertexIndex = vertices.Count;
        vertices.Add(v1);
        vertices.Add(v2);
        vertices.Add(v3);
        triangles.Add(vertexIndex);
        triangles.Add(vertexIndex + 1);
        triangles.Add(vertexIndex + 2);
    }

    private void AddTriangleColor (Color color) {
        colors.Add(color);
        colors.Add(color);
        colors.Add(color);
    }
}

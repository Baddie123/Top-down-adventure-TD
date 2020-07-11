using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class HexMesh : MonoBehaviour
{
    // Variables //
    Mesh hexMesh;
    List<Vector3> vertices;
    List<int> triangles;
    List<Color> colors;
    MeshCollider meshCollider;


    // Unity Functions //
    void Awake () {
        GetComponent<MeshFilter>().mesh = hexMesh = new Mesh();
        hexMesh.name = "Hex Mesh";
        vertices = new List<Vector3>();
        triangles = new List<int>();
        colors = new List<Color>();
        meshCollider = gameObject.AddComponent<MeshCollider>();
    }


    // Public Functions //
    public void Triangulate (HexCell[] cells)
    {
        hexMesh.Clear();
        vertices.Clear();
        triangles.Clear();
        colors.Clear();

        for (int i = 0; i < cells.Length; i++)
        {
            GiveDirections(cells[i]);
        }

        hexMesh.vertices = vertices.ToArray();
        hexMesh.triangles = triangles.ToArray();
        hexMesh.colors = colors.ToArray();
        hexMesh.RecalculateNormals();
        meshCollider.sharedMesh = hexMesh;
    }

    void GiveDirections (HexCell cell) {
		for (HexDirection d = HexDirection.NE; d <= HexDirection.NW; d++) {
			TriangulateCell(d, cell);
		}
	}
    
    void TriangulateCell (HexDirection direction, HexCell cell) {
        Vector3 center = cell.transform.localPosition;
        for (int i = 0; i < 6; i++)
        {
            AddTriangle(
                center,
                center + HexMetrics.GetFirstSolidCorner(direction),
                center + HexMetrics.GetSecondSolidCorner(direction)
            );
            HexCell neighbor = cell.GetNeighbor(direction) ?? cell;
            HexCell prevNeighbor = cell.GetNeighbor(direction.Previous()) ?? cell;
            HexCell nextNeighbor = cell.GetNeighbor(direction.Next()) ?? cell;
            AddTriangleColor(
                cell.color,
                (cell.color + prevNeighbor.color + neighbor.color) / 3f,
                (cell.color + neighbor.color + nextNeighbor.color) / 3f
            );
        }
    }
    
    void AddTriangle (Vector3 v1, Vector3 v2, Vector3 v3) {
        int vertexIndex = vertices.Count;
        vertices.Add(v1);
        vertices.Add(v2);
        vertices.Add(v3);
        triangles.Add(vertexIndex);
        triangles.Add(vertexIndex + 1);
        triangles.Add(vertexIndex + 2);
    }


    // Private/Helper Functions //
    private void AddTriangleColor (Color color) {
        colors.Add(color);
        colors.Add(color);
        colors.Add(color);
    }

    private void AddTriangleColor (Color c1, Color c2, Color c3) {
		colors.Add(c1);
		colors.Add(c2);
		colors.Add(c3);
	}
}

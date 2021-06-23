using UnityEngine;
using System;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{

    Mesh mesh;

    Vector3[] vertices;
    int[] triangles;
    System.Random random = new System.Random();


    [Header("Size")]
    public int xSize = 200;
    public int zSize = 200;
    
    [Header("PerlinNoise 1")]
    public float xMultiplier_p1 = 0.01f;
    public float zMultiplier_p1 = 0.01f;
    public float perlinMultipler_p1 = 1;

    [Header("PerlinNoise 2")]
    public float xMultiplier_p2 = 0.02f;
    public float zMultiplier_p2 = 0.02f;
    public float perlinMultipler_p2 = 2;

    [Header("PerlinNoise 3")]
    public float xMultiplier_p3 = 0.03f;
    public float zMultiplier_p3 = 0.03f;
    public float perlinMultipler_p3 = 3;

    [Header("PerlinNoise 4")]
    public float xMultiplier_p4 = 0.04f;
    public float zMultiplier_p4 = 0.04f;
    public float perlinMultipler_p4 = 4;

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    void Update()
    {
        CreateShape();
        UpdateMesh();
    }

    void CreateShape()
    {
        // sets the size of the array of vertices
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];

        // generates the vertices
        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                float perlinNoise1 = Mathf.PerlinNoise(x * xMultiplier_p1, z * zMultiplier_p1) * perlinMultipler_p1;
                float perlinNoise2 = Mathf.PerlinNoise(x * xMultiplier_p2, z * zMultiplier_p2) * perlinMultipler_p2;
                float perlinNoise3 = Mathf.PerlinNoise(x * xMultiplier_p3, z * zMultiplier_p3) * perlinMultipler_p3;
                float perlinNoise4 = Mathf.PerlinNoise(x * xMultiplier_p4, z * zMultiplier_p4) * perlinMultipler_p4;
                float y = perlinNoise1 + perlinNoise2 + perlinNoise3 + perlinNoise4;
                vertices[i] = new Vector3(x, y , z);
                i++;
            }
        }

        triangles = new int[xSize * zSize * 6];

        for (int vertex = 0, tries = 0, z = 0; z < zSize; z++)
        {
            for (int i = 0; i < xSize; i++)
            {
                triangles[tries + 0] = vertex + 0;
                triangles[tries + 1] = vertex + xSize + 1 ;
                triangles[tries + 2] = vertex + 1;
                triangles[tries + 3] = vertex + 1;
                triangles[tries + 4] = vertex + xSize + 1;
                triangles[tries + 5] = vertex + xSize + 2;

                vertex++;
                tries += 6;
            }
            vertex++;
        }
    }

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk
{
    public Vector3[] vertices;
    public int[] triangles;
    public Vector2[] uvs;
    public Color[] colors;

    public Vector2Int coords;

    public MeshFilter meshFilter;
    public MeshRenderer renderer;

    public Mesh mesh;
}

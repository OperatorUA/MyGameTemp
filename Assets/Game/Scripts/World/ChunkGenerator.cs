using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChunkGenerator : MonoBehaviour
{
    public float resolutionFactor;
    public int ChunkSize = 100;

    public Material material;
    public bool realTimeUpdate = false;

    public Gradient gradient;

    public PerlinComponent mountainsPerlin;
    public PerlinComponent plainsPerlin;
    public PerlinComponent unevennessPerlin;
    public PerlinComponent waterPitsPerlin;

    private int xSize;
    private int zSize;

    public float minTerrainHeight = 0;
    public float maxTerrainHeight = 10;

    private int seed;
    private int seedIndex;

    private List<Chunk> chunks = new List<Chunk>();
    private Chunk currentChunk;

    public void CreateChunk(Vector2Int coords, int seed)
    {
        currentChunk = new Chunk();

        currentChunk.coords = coords;
        this.seed = seed;
        seedIndex = 0;

        InitCurrentChunk();
        UpdateCurrentChunk();

        chunks.Add(currentChunk);
    }

    private void OnValidate()
    {
        if (realTimeUpdate && chunks.Count > 0)
        {
            UpdateAllChunks();
        }
    }

    private void UpdateAllChunks()
    {
        foreach (var chunk in chunks)
        {
            currentChunk = chunk;
            UpdateCurrentChunk();
        }
    }

    public void UpdateCurrentChunk()
    {
        SetVecticles();
        SetTriangles();

        CreateUv();
        SetColors();

        UpdateMesh();
    }

    private void InitCurrentChunk()
    {
        GameObject chunkObject = new GameObject($"Chunk: {currentChunk.coords.x}, {currentChunk.coords.y}");
        chunkObject.transform.SetParent(transform, false);

        xSize = ChunkSize;
        zSize = ChunkSize;

        currentChunk.meshFilter = chunkObject.transform.AddComponent<MeshFilter>();
        currentChunk.renderer = chunkObject.transform.AddComponent<MeshRenderer>(); ;
        currentChunk.renderer.material = material;

        currentChunk.mesh = new Mesh();
        currentChunk.meshFilter.mesh = currentChunk.mesh;

        currentChunk.vertices = new Vector3[(xSize + 1) * (zSize + 1)];
        currentChunk.triangles = new int[xSize * zSize * 6];

        mountainsPerlin.seed = GetSeed();
        plainsPerlin.seed = GetSeed();
        unevennessPerlin.seed = GetSeed();
        waterPitsPerlin.seed = GetSeed();
    }

    private void SetVecticles()
    {
        //minTerrainHeight = float.MaxValue;
        //maxTerrainHeight = float.MinValue;

        for (int index = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                int currentX = x + ChunkSize * currentChunk.coords.x;
                int currentZ = z + ChunkSize * currentChunk.coords.y;

                float height = GetCombinePerlinValue(currentX, currentZ);
                Vector3 pointPosition = new Vector3(currentX, height, currentZ);
                currentChunk.vertices[index] = pointPosition;

                //if (minTerrainHeight > height) minTerrainHeight = height;
                //if (maxTerrainHeight < height) maxTerrainHeight = height;
                index++;
            }
        }
    }

    private void SetTriangles()
    {
        int vert = 0;
        int tris = 0;

        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                currentChunk.triangles[tris + 0] = vert + 0;
                currentChunk.triangles[tris + 1] = vert + xSize + 1;
                currentChunk.triangles[tris + 2] = vert + 1;
                currentChunk.triangles[tris + 3] = vert + 1;
                currentChunk.triangles[tris + 4] = vert + xSize + 1;
                currentChunk.triangles[tris + 5] = vert + xSize + 2;

                vert++;
                tris += 6;
            }
            vert++;
        }
    }

    private void CreateUv()
    {
        currentChunk.uvs = new Vector2[currentChunk.vertices.Length];

        for (int index = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                currentChunk.uvs[index] = new Vector2((float)x / xSize, (float)z / zSize);
                index++;
            }
        }
    }

    private void SetColors()
    {
        currentChunk.colors = new Color[currentChunk.vertices.Length];

        for (int index = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                float y = Mathf.InverseLerp(minTerrainHeight, maxTerrainHeight * resolutionFactor, currentChunk.vertices[index].y);
                currentChunk.colors[index] = gradient.Evaluate(y);
                index++;
            }
        }
    }

    private void UpdateMesh()
    {
        currentChunk.mesh.Clear();

        currentChunk.mesh.vertices = currentChunk.vertices;
        currentChunk.mesh.triangles = currentChunk.triangles;
        currentChunk.mesh.uv = currentChunk.uvs;
        currentChunk.mesh.colors = currentChunk.colors;

        currentChunk.mesh.RecalculateNormals();
    }

    private float GetCombinePerlinValue(int x, int z)
    {
        float mountains = mountainsPerlin.GetValue(x, z, resolutionFactor);
        float plains = plainsPerlin.GetValue(x, z, resolutionFactor);
        float unevenness = unevennessPerlin.GetValue(x, z, resolutionFactor);
        float waterPits = waterPitsPerlin.GetValue(x, z, resolutionFactor);

        float result = mountains - plains;

        if (result < 0f) result = 0;
        result += unevenness;

        result -= waterPits;

        return result;
    }

    
    private float GetSeed()
    {
        int result = seed + seedIndex;
        seedIndex++;
        return result;
    }
}

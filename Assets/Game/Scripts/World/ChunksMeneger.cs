using UnityEngine;

public class ChunksMeneger : MonoBehaviour
{
    public Vector2Int chunksCount;
    private ChunkGenerator chunkGenerator;

    private void Start()
    {
        chunkGenerator = GetComponent<ChunkGenerator>();

        int seed = Random.Range(0, 99999);
        for (int x = 0; x < chunksCount.x; x++)
        {
            for (int z = 0; z < chunksCount.y; z++)
            {
                chunkGenerator.CreateChunk(new Vector2Int(x, z), seed);
            }
        }
    }
}

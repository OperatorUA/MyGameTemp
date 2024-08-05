using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PerlinComponent
{
    public float seed;
    public float zoom = 1;
    public float scale = 1;
    public float GetValue(int x, int z, float resolutionScale = 1f)
    {

        float perlin = Mathf.PerlinNoise((x + seed) / (zoom * resolutionScale), (z + seed) / (zoom * resolutionScale));

        return perlin * scale * resolutionScale;
    }
}

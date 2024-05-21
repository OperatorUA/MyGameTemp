using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GrowConfig : BaseConfig
{
    // Setup by id
    public List<GrowStage> growStages = new List<GrowStage>();

    public AttributeParameter growSpeed;
    public float growDifficulty;
    public int currentStage;
}

[System.Serializable]
public struct GrowStage
{
    public GameObject modelPrefab;
}

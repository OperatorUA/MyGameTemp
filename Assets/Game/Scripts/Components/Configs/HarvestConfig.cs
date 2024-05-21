using AYellowpaper.SerializedCollections;
using System.Collections.Generic;

[System.Serializable]
public class HarvestConfig : BaseConfig
{
    public SkillType skillType;
    public float harvestDifficulty;
    public int requiredSkillLevel;

    public List<DropItem> dropList = new List<DropItem>();
}

[System.Serializable]
public class DropItem
{
    public ItemData ItemData;
    public int minCount;
    public int maxCount;
    public int chanse = 100;
}
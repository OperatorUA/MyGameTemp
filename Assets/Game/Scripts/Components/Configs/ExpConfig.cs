using AYellowpaper.SerializedCollections;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ExpConfig : BaseConfig
{
    public SerializedDictionary<SkillType, SkillProgress> skills = new SerializedDictionary<SkillType, SkillProgress>();

    public ExpConfig()
    {
        foreach (SkillType type in Enum.GetValues(typeof(SkillType)))
        {
            skills[type] = new SkillProgress();
        }
    }
}

[System.Serializable]
public class SkillProgress
{
    public int level;
    public float currentExp;
    public float expToNextLvl;
    public AttributeParameter learningSpeed;
}

public enum SkillType
{
    Farmer,
    Miner,
    MeleAtteck
}

using System.Collections.Generic;
using UnityEngine;

public class ExpComponent : BaseComponent
{
    public ExpConfig expConfig;
    protected override void InitComponent(BaseConfig config)
    {
        expConfig = config as ExpConfig;

        foreach (KeyValuePair<SkillType, SkillProgress> pair in expConfig.skills)
        {
            UpdateExpToNextLvl(pair.Value);
        }
    }

    public void AddExp(SkillType skillType, float expAmounth)
    {
        SkillProgress skill = expConfig.skills[skillType];

        skill.currentExp += expAmounth * skill.learningSpeed.currentValue;
        if (skill.currentExp >= skill.expToNextLvl) LvlUp(skill);
    }

    private void UpdateExpToNextLvl(SkillProgress skill)
    {
        skill.expToNextLvl = GamePrefs.baseExpToNextLevel * Mathf.Pow(GamePrefs.expToNextLevelIncrease, skill.level);
    }

    private void LvlUp(SkillProgress skill)
    {
        if (skill.level < GamePrefs.maxLevel)
        {
            skill.level += 1;
            UpdateExpToNextLvl(skill);
            if (skill.currentExp >= skill.expToNextLvl) LvlUp(skill);
        }
    }
}

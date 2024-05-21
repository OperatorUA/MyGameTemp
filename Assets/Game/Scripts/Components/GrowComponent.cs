using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowComponent : BaseComponent
{
    public GrowConfig growConfig;

    public float growProgress = 0;
    public float nextStageProgressAmounth = 0;

    public GameObject currentModel;

    protected override void InitComponent(BaseConfig config)
    {
        growConfig = config as GrowConfig;
        UpdateNextStageRequireProgress();
    }

    private void Update()
    {
        growProgress += growConfig.growSpeed.currentValue * Time.deltaTime;

        if (growProgress > nextStageProgressAmounth)
        {
            NextStage();
        }
    }

    private void UpdateNextStageRequireProgress()
    {
        if (growConfig.currentStage == growConfig.growStages.Count - 1)
        {
            nextStageProgressAmounth = growConfig.growDifficulty * 2f;
            return;
        }

        float growThresholdPerStage = growConfig.growDifficulty / (growConfig.growStages.Count - 1);
        nextStageProgressAmounth = (growConfig.currentStage + 1) * growThresholdPerStage;
    }

    private void NextStage()
    {
        growConfig.currentStage++;
        if (growConfig.currentStage > growConfig.growStages.Count - 1)
        {
            growConfig.currentStage = 0;
            growProgress = 0;
        }

        UpdateNextStageRequireProgress();
        ChangeModel(growConfig.growStages[growConfig.currentStage].modelPrefab);
    }

    private void ChangeModel(GameObject prefab)
    {
        GameObject newModel = Instantiate(prefab, transform.position, transform.rotation, transform);
        
        if (currentModel == null) currentModel = transform.GetChild(0).gameObject;
        currentModel.SetActive(false);

        currentModel = newModel;
    }
}

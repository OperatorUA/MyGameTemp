using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BasePlant : BaseResource
{
    [SerializeField] private PlantData plantData;

    private HealthComponent healthComponent;
    private GrowComponent growComponent;

    public override void Awake()
    {
        base.Awake();
        plantData = ResourceLoader.DeepCopy(entityData) as PlantData;

        healthComponent = transform.AddComponent<HealthComponent>();
        healthComponent.Init(plantData.healthConfig);

        growComponent = transform.AddComponent<GrowComponent>();
        growComponent.Init(plantData.growConfig);
    }
}

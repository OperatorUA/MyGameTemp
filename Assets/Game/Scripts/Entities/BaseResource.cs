using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaseResource : BaseEntity
{
    [SerializeField] private ResourceData resourceData;
    private HarvestComponent harvestComponent;

    public virtual void Awake()
    {
        resourceData = ResourceLoader.DeepCopy(entityData) as ResourceData;

        harvestComponent = transform.AddComponent<HarvestComponent>();
        harvestComponent.Init(resourceData.harvestConfig);
    }
}

using Unity.VisualScripting;
using UnityEngine;

public class BaseUnit : BaseEntity
{
    public UnitData unitData;
    public Outline outline;

    public OrdersBehaviour ordersBehaviour;
    public HealthComponent healthComponent;
    public ExpComponent expComponent;
    public InventoryComponent inventoryComponent;

    private void Awake()
    {
        unitData = ResourceLoader.DeepCopy(entityData as UnitData);

        outline = transform.AddComponent<Outline>();
        outline.OutlineWidth = 4f;
        outline.OutlineColor = new Color(0.3f, 1, 1);
        outline.enabled = false;

        ordersBehaviour = transform.AddComponent<OrdersBehaviour>();

        healthComponent = transform.AddComponent<HealthComponent>();
        healthComponent.Init(unitData.healthConfig);

        expComponent = transform.AddComponent<ExpComponent>();
        expComponent.Init(unitData.expConfig);

        inventoryComponent = transform.AddComponent<InventoryComponent>();
        inventoryComponent.Init(unitData.inventoryConfig);
    }
}

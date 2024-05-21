using UnityEngine;

public class HealthComponent : BaseComponent
{
    public HealthConfig healthConfig;
    public float currentHealth;

    protected override void InitComponent(BaseConfig config)
    {
        healthConfig = config as HealthConfig;
        currentHealth = healthConfig.maxHealth.currentValue;
    }

    private void Update()
    {
        if (healthConfig == null) return;

        if (currentHealth < healthConfig.maxHealth.currentValue)
        {
            currentHealth += healthConfig.healthRegen.currentValue * Time.deltaTime;
        }

        if (currentHealth > healthConfig.maxHealth.currentValue) currentHealth = healthConfig.maxHealth.currentValue;
    }
}

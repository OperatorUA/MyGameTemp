using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseComponent : MonoBehaviour
{
    protected abstract void InitComponent(BaseConfig config);

    public void Init(BaseConfig config)
    {
        if (config != null)
        {
            InitComponent(config);
        }
        else
        {
            Debug.LogError("config is null!");
        }
    }
}

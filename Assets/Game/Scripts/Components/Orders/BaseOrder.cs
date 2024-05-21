using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class BaseOrder
{
    public UnityEvent OnOrderCompleted = new UnityEvent();
    public abstract void Execute();
}

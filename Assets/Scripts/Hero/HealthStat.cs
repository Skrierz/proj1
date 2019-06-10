using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class HealthStat
{
    [SerializeField]
    private HealthBAr bar;
    [SerializeField]
    private float maxVal;
    [SerializeField]
    private float currentVal;

    public float CurrentVal
    {
        get
        {
            return currentVal;
        }
        set
        {
            currentVal = Mathf.Clamp(value, 0, MaxVal);
            bar.Value = currentVal;
        }
    }

    public float MaxVal
    {
        get
        {
            return maxVal;
        }
        set
        {
            maxVal = value;
            bar.MaxValue = maxVal;
        }
    }
    
    public void Initrialize()
    {
        this.MaxVal = maxVal;
        this.CurrentVal = currentVal;
    }
}

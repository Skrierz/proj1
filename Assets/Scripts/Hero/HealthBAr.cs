using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBAr : MonoBehaviour
{
    [SerializeField]
    private float lerpSpeed;


    private float fillAmount;
    [SerializeField]
    private Image content;
    [SerializeField]
    private Text valueText;

    public float MaxValue { get; set; }

    public float Value
    {
        set
        {
            valueText.text = value + "/" + MaxValue;
            fillAmount = Map(value, MaxValue);
        }
    }

    

    // Update is called once per frame
    void Update()
    {
        Bar();
    }
    private void Bar()
    {
        if (fillAmount!=content.fillAmount)
        {
            content.fillAmount = Mathf.Lerp(content.fillAmount,fillAmount,Time.deltaTime*lerpSpeed);
        }
    
    }
    private float Map(float value, float inMax)
    {
        return value/ inMax;
    }
}

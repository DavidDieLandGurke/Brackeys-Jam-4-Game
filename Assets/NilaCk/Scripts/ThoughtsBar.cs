using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThoughtsBar : MonoBehaviour
{
    Slider slider;
    public Gradient gradient;
    public Image fillImage;

    void Start()
    {
        slider = GetComponent<Slider>();
    }

    public void setThoughts(int convincement)
    {
        slider.value = convincement;
        fillImage.color = gradient.Evaluate(slider.normalizedValue);
    } 
    public void setMaxThoughts(int convincement)
    {
        slider.maxValue = convincement;
        slider.value = convincement;
        fillImage.color = gradient.Evaluate(1f);
    }
}

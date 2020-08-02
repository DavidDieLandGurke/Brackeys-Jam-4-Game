using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Zombies : MonoBehaviour
{
    public float maxHealth = 100;
    float currentHealth;

    public Slider healthSlider;
    public GameObject sliderCanvas;

    public Gradient gradient;
    public Image fill;

    void Start()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        fill.color = gradient.Evaluate(1);
    }

    void Update()
    {
        healthSlider.value = currentHealth;
        sliderCanvas.transform.position = transform.position;
        
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        fill.color = gradient.Evaluate(healthSlider.normalizedValue);
        if (currentHealth <= 0)
        {
            Destroy(sliderCanvas);
            Destroy(gameObject);
        }
    }
}

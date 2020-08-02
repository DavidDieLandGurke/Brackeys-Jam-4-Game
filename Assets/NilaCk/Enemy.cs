using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
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
        if (healthSlider != null && fill != null)
        {
            healthSlider.maxValue = maxHealth;
            fill.color = gradient.Evaluate(1);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Convinced());
        }
    }

    void Update()
    {
        if (healthSlider != null && sliderCanvas != null)
        {
            healthSlider.value = currentHealth;
            sliderCanvas.transform.position = transform.position;
        }
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (fill != null)
        {
            fill.color = gradient.Evaluate(healthSlider.normalizedValue);
        }
        if (currentHealth <= 0)
        {
            if (sliderCanvas != null)
            {
                Destroy(sliderCanvas);
            }
            Destroy(gameObject);
        }
    }

    IEnumerator Convinced()
    {
        yield return new WaitForSeconds(0.2f);
        gameObject.GetComponent<AIPath>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        gameObject.tag = "EnemyConvinced";
        yield return new WaitForSeconds(5f);
        TakeDamage(100);
    }
}

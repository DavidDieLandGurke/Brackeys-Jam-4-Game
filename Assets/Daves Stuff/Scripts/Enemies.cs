using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemies : MonoBehaviour
{
    public float maxHealth = 100;
    float currentHealth;

    public Slider healthSlider;
    public GameObject sliderCanvas;

    public Gradient gradient;
    public Image fill;

    public GameObject bubble;
    public Animator bubbleAnim;

    public GameObject parent;

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
            StartCoroutine(Convince());
        }
    }

    void Update()
    {
        healthSlider.value = currentHealth;
        sliderCanvas.transform.position = transform.position;
        bubble.transform.position = transform.position;
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
            Destroy(parent);
        }
    }

    IEnumerator Convince()
    {
        gameObject.GetComponent<AIPath>().enabled = false;
        gameObject.tag = "EnemyConvinced";
        bubbleAnim.SetTrigger("Bubble");
        yield return new WaitForSeconds(2.5f);
        TakeDamage(100);
    }
}

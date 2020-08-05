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

    public GameObject bubble;
    public Animator bubbleAnim;

    public Animator anim;

    public int id;

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
        if (healthSlider != null && sliderCanvas != null)
        {
            healthSlider.value = currentHealth;
            sliderCanvas.transform.position = transform.position;
        }
        bubble.transform.position = transform.position;

        anim.SetFloat("Vel", GetComponent<Rigidbody2D>().velocity.magnitude);
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
            GameEvents.NotNull(id);
            id = 1000;
            Destroy(transform.parent.gameObject);
        }
    }

    IEnumerator Convince()
    {
        gameObject.GetComponent<AIPath>().canMove = false;
        GetComponent<Rigidbody2D>().isKinematic = true;
        yield return new WaitForSeconds(0.01f);
        gameObject.tag = "EnemyConvinced";
        bubbleAnim.SetTrigger("Bubble");
        yield return new WaitForSeconds(2.5f);
        TakeDamage(100);
    }
}

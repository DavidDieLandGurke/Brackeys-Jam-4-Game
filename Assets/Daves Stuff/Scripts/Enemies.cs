using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemies : MonoBehaviour
{
    public float maxHealth = 100;
    float currentHealth;

    public string jankTag;

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

        anim.SetFloat("Vel", GetComponent<AIPath>().velocity.magnitude);
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
        if (jankTag == "Cop")
        {
            FindObjectOfType<AudioManager>().Play($"CopAttack{Random.Range(1, 3)}");
            yield return new WaitForSeconds(0.01f);
            jankTag = "CopConvinced";
            yield return new WaitForSeconds(2f);
            jankTag = "Cop";
        }
        if (jankTag == "Teacher")
        {
            FindObjectOfType<AudioManager>().Play($"TeacherAttack{Random.Range(1, 3)}");
            yield return new WaitForSeconds(0.01f);
            jankTag = "CopConvinced";
            yield return new WaitForSeconds(2f);
            jankTag = "Teacher";
        }
        if (jankTag == "Worker")
        {
            FindObjectOfType<AudioManager>().Play($"WorkerAttack{Random.Range(1, 3)}");
            yield return new WaitForSeconds(0.01f);
            jankTag = "CopConvinced";
            yield return new WaitForSeconds(2f);
            jankTag = "Worker";
        }

        gameObject.GetComponent<AIPath>().canMove = false;
        GetComponent<Rigidbody2D>().isKinematic = true;
        yield return new WaitForSeconds(0.01f);
        FindObjectOfType<AudioManager>().Play($"PlayerHurt{Random.Range(1, 4)}");
        Destroy(sliderCanvas);
        gameObject.tag = "EnemyConvinced";
        bubbleAnim.SetTrigger("Bubble");
        yield return new WaitForSeconds(2.5f);

        if (jankTag == "Cop")
        {
            FindObjectOfType<AudioManager>().Play($"CopDeath{Random.Range(1, 3)}");
            yield return new WaitForSeconds(0.01f);
            jankTag = "CopConvinced";
        }
        else if (jankTag == "Teacher")
        {
            FindObjectOfType<AudioManager>().Play($"TeacherDeath{Random.Range(1, 3)}");
            yield return new WaitForSeconds(0.01f);
            jankTag = "CopConvinced";
        }
        else if (jankTag == "Worker")
        {
            FindObjectOfType<AudioManager>().Play($"WorkerDeath{Random.Range(1, 3)}");
            yield return new WaitForSeconds(0.01f);
            jankTag = "CopConvinced";
        }

        TakeDamage(100);
    }
}

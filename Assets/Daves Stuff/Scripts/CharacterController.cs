using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private Rigidbody2D _rb;

    private float _xAxis;
    private float _yAxis;

    public float speed;

    private Vector2 _mousePos;
    private Vector2 _mouseDirection;
    private float _mouseAngle;

    public int strength;

    public Camera mainCam;

    public LayerMask mask;

    private LineRenderer _lineRenderer;

    public GameObject deathCanvas;

    public int maxHealth;
    int currentHealth;

    public ThoughtsBar thoughtsBar;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _lineRenderer = GetComponent<LineRenderer>();
        currentHealth = maxHealth;
        thoughtsBar.setMaxThoughts(maxHealth);
    }

    void Update()
    {
        _xAxis = Input.GetAxis("Horizontal");
        _yAxis = Input.GetAxis("Vertical");

        _mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        transform.eulerAngles = new Vector3(0, 0, _mouseAngle);

        Debug.DrawRay(transform.position, _mouseDirection);

        GetComponent<LineRenderer>().SetPosition(0, transform.position);

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void FixedUpdate()
    {
        _rb.velocity = new Vector2(_xAxis, _yAxis)* speed;

        _mouseDirection = _mousePos - _rb.position;
        _mouseAngle = Mathf.Atan2(_mouseDirection.y, _mouseDirection.x) * Mathf.Rad2Deg;
    }

    void Shoot()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, _mouseDirection, 50, mask);
        StartCoroutine(ShotVisualisation(ray));

        if (ray.collider != null && ray.collider.gameObject.CompareTag("Enemy"))
        {
            if(ray.collider.gameObject.GetComponent<Enemies>() != null)
            {
                ray.collider.gameObject.GetComponent<Enemies>().TakeDamage(strength);
            }
            else
            {
                ray.collider.gameObject.GetComponent<Enemy>().TakeDamage(strength);
            }
        }
    }

    IEnumerator ShotVisualisation(RaycastHit2D ray)
    {
        _lineRenderer.enabled = true;
        _lineRenderer.SetPosition(0, transform.position);
        if(ray.collider != null)
        {
            _lineRenderer.SetPosition(1, ray.point);
        }
        else
        {
            _lineRenderer.SetPosition(1, _mousePos + _mouseDirection * 15);
        }

        yield return new WaitForSeconds(0.025f);
        _lineRenderer.enabled = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            StartCoroutine(onHit());
        }
    }

    public void LoseThoughts(int loss)
    {
        currentHealth -= loss;
        thoughtsBar.setThoughts(currentHealth);
        if (currentHealth <= 0)
        {
            deathCanvas.SetActive(true);
            Destroy(gameObject);
        }
    }

    IEnumerator onHit()
    {
        LoseThoughts(25);
        yield return new WaitForSeconds(1.2f);
    }
}

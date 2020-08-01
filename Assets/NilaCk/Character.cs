using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Rigidbody2D _rb;

    private float _xAxis;
    private float _yAxis;

    public float speed;

    public GameObject muzzleFlash;
    public GameObject zombieHitEffect;
    public GameObject hitEffect;

    private Vector2 _mousePos;
    private Vector2 _mouseDirection;
    private float _mouseAngle;

    public Camera mainCam;

    public int strength;

    public LayerMask mask;
    private LineRenderer _lineRenderer;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>(); _lineRenderer = GetComponent<LineRenderer>();
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
        _rb.velocity = new Vector2(_xAxis, _yAxis) * speed;

        _mouseDirection = _mousePos - _rb.position;
        _mouseAngle = Mathf.Atan2(_mouseDirection.y, _mouseDirection.x) * Mathf.Rad2Deg;
    }

    void Shoot()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, _mouseDirection, 50, mask);
        StartCoroutine(ShotVisualisation(ray));

        Instantiate(muzzleFlash, ray.collider.gameObject.transform.position, ray.collider.gameObject.transform.rotation);

        if (ray.collider != null && ray.collider.gameObject.CompareTag("Enemy"))
        {
            ray.collider.gameObject.GetComponent<Zombies>().TakeDamage(strength);
            Instantiate(zombieHitEffect, ray.collider.gameObject.transform.position, ray.collider.gameObject.transform.rotation);
        }
        if (ray.collider != null && ray.collider.gameObject.tag != "Enemy")
        {
            Instantiate(hitEffect, ray.collider.gameObject.transform.position, ray.collider.gameObject.transform.rotation);
        }
    }

    IEnumerator ShotVisualisation(RaycastHit2D ray)
    {
        _lineRenderer.enabled = true;
        _lineRenderer.SetPosition(0, transform.position);
        if (ray.collider != null)
        {
            _lineRenderer.SetPosition(1, ray.point);
        }
        else
        {
            _lineRenderer.SetPosition(1, _mousePos + _mouseDirection * 15);
        }

        yield return new WaitForSeconds(0.05f);
        _lineRenderer.enabled = false;
    }

}

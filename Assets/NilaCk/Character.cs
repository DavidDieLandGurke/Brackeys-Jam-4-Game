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

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _xAxis = Input.GetAxis("Horizontal");
        _yAxis = Input.GetAxis("Vertical");

        _mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        transform.eulerAngles = new Vector3(0, 0, _mouseAngle);

        Debug.DrawRay(transform.position, _mouseDirection);

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

        Instantiate(muzzleFlash, ray.collider.gameObject.transform.position, ray.collider.gameObject.transform.rotation);

        if (ray.collider != null && ray.collider.gameObject.CompareTag("Enemy"))
        {
            Debug.LogError("It's Spawning At The Enemy");
            ray.collider.gameObject.GetComponent<Zombies>().TakeDamage(strength);
            Instantiate(zombieHitEffect, ray.collider.gameObject.transform.position, ray.collider.gameObject.transform.rotation);
        }
        if (ray.collider != null && ray.collider.gameObject.tag != "Enemy")
        {
            Debug.LogError("It Isn't Spawning At The Enemy");
            Instantiate(hitEffect, ray.collider.gameObject.transform.position, ray.collider.gameObject.transform.rotation);
        }
    }
}

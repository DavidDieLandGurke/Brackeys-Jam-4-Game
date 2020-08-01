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

    public Camera mainCam;

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
    }

    void FixedUpdate()
    {
        _rb.velocity = new Vector2(_xAxis, _yAxis)* speed;

        _mouseDirection = _mousePos - _rb.position;
        _mouseAngle = Mathf.Atan2(_mouseDirection.y, _mouseDirection.x) * Mathf.Rad2Deg;
    }
}

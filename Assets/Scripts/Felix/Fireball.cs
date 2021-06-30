using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    private Rigidbody2D rb;

    private float direction;
    private float speed;

    private bool isLaunched = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (isLaunched)
        {
            rb.MovePosition(rb.position + (Vector2)transform.right * direction * speed * Time.fixedDeltaTime);
        }
    }

    public void Initialize(float _direction, float _speed)
    {
        direction = _direction;
        speed = _speed;
    }

    public void LaunchFireball()
    {
        isLaunched = true;

        Destroy(gameObject, 5f);
    }
}

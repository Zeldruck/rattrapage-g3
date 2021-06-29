using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 direction;
    private bool isDown = false;
    private float timerH = 0f;
    private float timerV = 0f;

    public float forceMovement;
    public float gravity;
    public Vector2 windForce;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");

        if (timerH > 0f)
            timerH -= Time.deltaTime;

        if (timerV > 0f)
            timerV -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (timerV <= 0f || isDown)
            rb.velocity += (Vector2)transform.up * gravity * Time.fixedDeltaTime;

        rb.velocity += windForce * Time.fixedDeltaTime;

        if ((timerH <= 0f || timerV <= 0) && (direction.x != 0f || direction.y != 0f))
        {
            if (timerV <= 0f && direction.y > 0f)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0f);
                isDown = false;
            }
            else if (timerV <= 0f && direction.y < 0f)
            {
                isDown = true;
            }

            rb.AddForce(new Vector2(timerH <= 0f ? direction.x : 0f, timerV <= 0f ? direction.y : 0f) * forceMovement * Time.fixedDeltaTime);

            if (timerH <= 0f && direction.x != 0f)
                timerH = 1f;

            if (timerV <= 0f && direction.y != 0f)
                timerV = 1f;
        }
    }
}

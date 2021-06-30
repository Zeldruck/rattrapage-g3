using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 direction;
    private bool isDown = false;
    private float timerH = 0f;
    private float timerV = 0f;
    private float timerProjectile = 0f;
    private bool shoot = false;

    [HideInInspector] public bool invincible = false;

    public int life;
    public float forceMovement;
    public Vector2 windForce;
    [Space]
    public GameObject projectilePrefab;
    public float projectileSpeed;
    public float projectileCooldown;

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

        if (timerProjectile > 0f)
            timerProjectile -= Time.deltaTime;
        else if (Input.GetKey(KeyCode.Space))
        {
            shoot = true;
            timerProjectile = projectileCooldown;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity += new Vector2(windForce.x, timerV <= 0f || isDown ? windForce.y : 0f) * Time.fixedDeltaTime;

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

        if (shoot)
        {
            Shoot();
            shoot = false;
        }
    }

    private void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position + new Vector3(transform.localScale.x / 2f, 0f, 0f), Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().velocity = transform.right * projectileSpeed * Time.fixedDeltaTime;
        Destroy(projectile, 5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            life--;

            if (life <= 0)
            {
                Destroy(this);
                SceneManager.LoadSceneAsync(0);
            }
        }
    }
}

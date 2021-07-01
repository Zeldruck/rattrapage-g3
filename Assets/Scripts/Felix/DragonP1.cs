using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragonP1 : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;

    public Transform shootTransform;
    public GameObject fireballPrefab;
    [Space]
    public Transform player;

    private float timerGeneral = .5f;
    private int attackChoosed = 0;

    private float lastPosition;
    private float nextPosition;
    private float timerMoving;
    private float timerMovingLerp;

    private float startHealth;
    public float health;
    public Image healthbar;

    [Header("Move")]
    public Vector2 movingRangeY;
    public Vector2 cooldownMovingRange;
    public float speed;

    [Header("Attack1")]
    public float distanceAttack1;
    public float cooldownAttack1;
    public float fireballSpeedAttack1;

    [Header("Attack2")]
    public float distanceAttack2;
    public float cooldownAttack2;
    public float fireballSpeedAttack2;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        startHealth = health;

        timerMoving = Random.Range(cooldownMovingRange.x, cooldownMovingRange.y);
        timerMovingLerp = 0f;
    }

    void Update()
    {
        if (player != null)
        {
            float distance = Vector3.Distance(player.position, transform.position);

            if (distance <= distanceAttack1 && attackChoosed == 1)
            {
                Attack1();
                timerGeneral = cooldownAttack1;
                attackChoosed = 0;
            }

            if (distance <= distanceAttack2 && attackChoosed == 2)
            {
                Attack2();
                timerGeneral = cooldownAttack2;
                attackChoosed = 0;
            }

            if (timerGeneral > 0f)
                timerGeneral -= Time.deltaTime;
            else
            {
                attackChoosed = Random.Range(1, 3);
            }

            if (timerMoving > 0f)
                timerMoving -= Time.deltaTime;

            healthbar.fillAmount = health / startHealth;
        }
    }

    private void FixedUpdate()
    {
        if (timerMoving <= 0f)
        {
            if (timerMovingLerp <= 0f)
            {
                lastPosition = rb.position.y;
                nextPosition = Random.Range(-movingRangeY.y / 2f, movingRangeY.y / 2f);
            }

            timerMovingLerp += Time.fixedDeltaTime * speed;

            Vector2 nPos = new Vector2(rb.position.x, Mathf.Lerp(lastPosition, nextPosition, timerMovingLerp));
            rb.MovePosition(nPos);

            if (timerMovingLerp >= 1f)
            {
                timerMoving = Random.Range(cooldownMovingRange.x, cooldownMovingRange.y); ;
                timerMovingLerp = 0f;
            }
        }
    }

    // Tir en direction du joueur
    private void Attack1()
    {
        GameObject fireball = Instantiate(fireballPrefab, shootTransform.position, Quaternion.identity);

        Vector3 dirAngle = (player.position - transform.position).normalized;
        float angle = Mathf.Atan2(dirAngle.y, dirAngle.x) * Mathf.Rad2Deg;
        fireball.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        fireball.GetComponent<Fireball>().Initialize(1f, fireballSpeedAttack1);
    }

    // Tir 3 balles 
    private void Attack2()
    {
        GameObject fireball = Instantiate(fireballPrefab, shootTransform.position, Quaternion.Euler(0f, 0f, -15f));
        fireball.GetComponent<Fireball>().Initialize(-1f, fireballSpeedAttack2);

        fireball = Instantiate(fireballPrefab, shootTransform.position, Quaternion.Euler(0f, 0f, 0f));
        fireball.GetComponent<Fireball>().Initialize(-1f, fireballSpeedAttack2);

        fireball = Instantiate(fireballPrefab, shootTransform.position, Quaternion.Euler(0f, 0f, 15f));
        fireball.GetComponent<Fireball>().Initialize(-1f, fireballSpeedAttack2);
    }

    private void Dead()
    {
        Destroy(gameObject);
        Destroy(player.GetComponent<Player>());
        Destroy(player.GetComponent<Collider2D>());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerProjectile"))
        {
            Destroy(collision.gameObject);

            health--;

            if (health <= 0)
                Dead();

            healthbar.fillAmount = health / startHealth;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, distanceAttack1);

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector3(movingRangeY.x, 0f, 0f), new Vector3(1f, movingRangeY.y, 0f));
    }
}

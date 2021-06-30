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

    private float startHealth;
    public float health;
    public Image healthbar;

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
    }

    void Update()
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

        healthbar.fillAmount = health / startHealth;
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
        player.GetComponent<Player>().invincible = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            health--;

            if (health <= 0)
                Dead();

            healthbar.fillAmount = health / startHealth;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, distanceAttack1);
    }
}

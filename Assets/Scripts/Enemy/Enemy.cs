
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Movimento")]
    [SerializeField] private float speed = 2f;
    [SerializeField] private bool chasePlayer = true;

    [Header("Vida")]
    [SerializeField] private int health = 3;
    [SerializeField] public int points = 100;

    private Rigidbody2D rb;
    private Transform player;
    private bool isDead;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        GameObject p = GameObject.FindWithTag("Player");

        if (p != null)
        {
            player = p.transform;
        }
    }

    private void FixedUpdate()
    {
        if (isDead)
            return;

        if (chasePlayer && player != null)
        {
            rb.MovePosition(Vector2.MoveTowards(
                rb.position,
                player.position,
                speed * Time.fixedDeltaTime
            ));
        }
        else
        {
            rb.MovePosition(rb.position + Vector2.down * speed * Time.fixedDeltaTime);
        }
    }

    public void TakeDamage(int amount)
    {
        if (isDead || amount <= 0)
            return;

        health -= amount;

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (isDead)
            return;

        isDead = true;

        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddScore(points);
        }

        if (EnemySpawner.Instance != null)
        {
            EnemySpawner.Instance.EnemyDefeated();
        }

        Destroy(gameObject);
    }
}

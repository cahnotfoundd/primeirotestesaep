
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Movimento")]
    [SerializeField] private float speed = 2f;
    [SerializeField] private bool chasePlayer = true;

    [Header("Vida")]
    [SerializeField] private int health = 3;
    [SerializeField] public int points = 100;

    private Transform player;
    private bool isDead;

    private void Start()
    {
        GameObject p = GameObject.FindWithTag("Player");

        if (p != null)
        {
            player = p.transform;
        }
    }

    private void Update()
    {
        if (isDead)
            return;

        if (chasePlayer && player != null)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                player.position,
                speed * Time.deltaTime
            );
        }
        else
        {
            transform.Translate(
                Vector2.down * speed * Time.deltaTime
            );
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

        Destroy(gameObject);
    }
}

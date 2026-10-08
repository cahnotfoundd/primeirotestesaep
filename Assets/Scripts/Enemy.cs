using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private float speed = 2f; // velocidade do inimigo
    [SerializeField] private int health = 1; // pontos de vida do inimigo
    [SerializeField] private bool chasePlayer = true; // se o inimigo persegue o jogador

    private Transform player; // referência ao transform do jogador

    private void Start()
    {
        if (chasePlayer)
        {
            GameObject p = GameObject.FindWithTag("Player"); // procura o jogador na cena
            if (p != null) player = p.transform; // se encontrou, pega o transform do jogador
        }
    }

    // Update is called once per frame
    private void Update()
    {
        // se encontrar o player e o inimigo persegue o jogador, move o inimigo na direção do player
        if (chasePlayer && player != null)
        {
            transform.position = Vector2.MoveTowards(
            transform.position, player.position, speed * Time.deltaTime); // move o inimigo na direção do jogador com base na velocidade e no tempo
        }
        else
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime); // move o inimigo para baixo com base na velocidade e no tempo
        }
    }

    public void TakeDamage(int amount) // função para o inimigo receber dano
    {
        health -= amount;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet")) // verifica se o inimigo colidiu com uma bala
        {
            TakeDamage(1); // chama a função de receber dano
            Destroy(other.gameObject); // destrói a bala
        }
    }

    private void OnBecameInvisible() // função chamada quando o inimigo sai da tela
    {
        Destroy(gameObject); // destrói o inimigo
    }
}

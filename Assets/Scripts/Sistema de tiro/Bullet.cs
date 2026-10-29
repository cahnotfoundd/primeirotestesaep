using System.Data;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f; // velocidade da bala
    [SerializeField] private float lifetime = 3f; // tempo de vida da bala
    [SerializeField] private int damage = 1;

    void Start()
    {
        Destroy(gameObject, lifetime); // destrói a bala após o tempo de vida
    }
    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime); // move a bala para cima com base na velocidade e no tempo
    }


    // verifica se a bala colidiu com um inimigo
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy")) // verifica se a bala colidiu com um inimigo
        return;
        
           if (other.TryGetComponent<Enemy>(out Enemy enemy)) // verifica se o inimigo tem o componente Enemy
            {
                enemy.TakeDamage(damage); // chama a função de receber dano do inimigo
                Destroy(gameObject); // destrói a bala

            }
           
    }

}


using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] private GameObject enemyPrefab; // prefab do inimigo
    [SerializeField] private float spawnInterval = 1.5f; // segundos entre cada spawn
    [SerializeField] private float margin = 1f; // distância do spawn do inimigo em relação ao centro da tela

    private Camera cam; // referência à câmera principal do jogo
    private float timer; // temporizador para controlar o spawn dos inimigos

    private void Awake()
    {
        cam = Camera.main; // referencia camera principal
    }
    private void Update()
    {
        timer += Time.deltaTime; // incrementa o temporizador com o tempo decorrido desde o último frame
        if (timer >= spawnInterval) // verifica se o tempo do spawn já passou
        {
            timer = 0f; // reseta o temporizador
            SpawnEnemy(); // chama a função de spawn do inimigo
        }
    }

    private void SpawnEnemy()
    {
        //cantos da tela convertidos para coordenadas do mundo
        Vector2 min = cam.ViewportToWorldPoint(new Vector3(0f, 0f, 0f)); //canto inferior esquerdo da tela
        Vector2 max = cam.ViewportToWorldPoint(new Vector3(1f, 1f, 0f));//canto superior direito da tela

        float x = Random.Range(min.x + margin, max.x - margin); // posição x aleatória dentro da tela com margem
        float y = max.y + margin; // posição y acima da tela com margem

        Instantiate(enemyPrefab, new Vector2(x, y), Quaternion.identity); // instancia o inimigo na posição aleatória com rotação padrão
    }
}

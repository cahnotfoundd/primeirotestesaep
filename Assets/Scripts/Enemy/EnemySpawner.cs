using System.Collections;
using UnityEngine;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance;

    [Header("Enemy")]
    [SerializeField] private GameObject enemyPrefab; // prefab do inimigo
    [SerializeField] private float spawnInterval = 1.5f; // segundos entre cada spawn
    [SerializeField] private float margin = 1f; // distância do spawn do inimigo em relação ao centro da tela

    [Header("Ondas")]
    [SerializeField] private int [] enemiesPerWave = {5, 8, 12};
    [SerializeField] private float timeBetweenWaves = 2f;

    [Header("Interface")]
    [SerializeField] private TextMeshProUGUI waveText;

    private Camera cam; // referência à câmera principal do jogo
    private int currentWave = 0;
    private int spawnedThisWave = 0;
    private int aliveEnemies = 0;

    private void Awake()
    {
        Instance = this;
        cam = Camera.main; // referencia camera principal
    }

    private void Start()
    {
        StartCoroutine(StartNextWave());    
    }

    private IEnumerator StartNextWave()
    {
        if (currentWave >= enemiesPerWave.Length)
        {
            if (waveText != null)
                waveText.text = "Ondas concluídas!";

            Debug.Log("FIM DAS ONDAS DETECTADO!");

            if (GameManager.Instance != null)
            {
                Debug.Log("GameManager encontrado!");
                GameManager.Instance.Victory();
            }
            else
            {
                Debug.LogError("GameManager.Instance está NULL!");
            }

            yield break;
        }

        currentWave++;
        spawnedThisWave = 0;

        UpdateWaveUI();

        int total = enemiesPerWave[currentWave - 1];

        while (spawnedThisWave < total)
        {
            SpawnEnemy();
            spawnedThisWave++;

            yield return new WaitForSeconds(spawnInterval);
        }

        while (aliveEnemies > 0)
        {
            yield return null;
        }

        yield return new WaitForSeconds(timeBetweenWaves);

        StartCoroutine(StartNextWave());
    }

    private void SpawnEnemy()
    {
        if (enemyPrefab == null || cam == null)
            return;
       
        Vector3 min = cam.ViewportToWorldPoint(
            new Vector3(0f, 0f, -cam.transform.position.z));

        Vector3 max = cam.ViewportToWorldPoint(
            new Vector3(1f, 1f, -cam.transform.position.z));

        float x = Random.Range(min.x + margin, max.x - margin);
        float y = Random.Range(min.y + margin, max.y - margin);

        Vector2 spawnPosition = new Vector2(x, y);

        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        aliveEnemies++;
    }

    public void EnemyDefeated()
    {
        aliveEnemies = Mathf.Max(0, aliveEnemies - 1);
    }

    private void UpdateWaveUI()
    {
        if (waveText != null)
        {
            waveText.text = "Onda: " + currentWave + "/" + enemiesPerWave.Length;
        }
    }
}

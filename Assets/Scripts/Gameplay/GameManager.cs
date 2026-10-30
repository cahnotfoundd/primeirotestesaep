
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Pontuação")]
    [SerializeField] private int score = 0;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI finalScoreText;

    [Header("Telas")]
    [SerializeField] private GameObject panelGameOver;
    [SerializeField] private GameObject panelVictory;

    private bool gameEnded = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateScoreUI();

        panelGameOver.SetActive(false);
        panelVictory.SetActive(false);
    }

    public void AddScore(int points)
    {
        if (gameEnded) return;

        score += points;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = score.ToString();
    }

    public void GameOver()
    {
        if (gameEnded) return;

        gameEnded = true;
        panelGameOver.SetActive(true);
        ShowFinalScore();
        Time.timeScale = 0f;
    }

    public void Victory()
    {
        Debug.Log("MÉTODO VICTORY FOI CHAMADO!");

        if (gameEnded) return;

        gameEnded = true;

        if (panelVictory == null)
        {
            Debug.LogError("Panel Victory não está atribuído!");
            return;
        }

        panelVictory.SetActive(true);
        ShowFinalScore();
        Time.timeScale = 0f;
    }

    private void ShowFinalScore()
    {
        if (finalScoreText != null)
            finalScoreText.text = score.ToString();
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

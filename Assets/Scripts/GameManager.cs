using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance
    {
        get; private set;
    }

    [SerializeField] Text currentLifeText;
    [SerializeField] GameObject gameOverCanvas, gameClearCanvas;
    [SerializeField] int maxLife = 3;

    int currentLife;

    public enum GameState
    {
        GameStart,
        GameOver,
        GameClear
    }

    GameState currentGameState;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Initialization();
    }

    private void Initialization()
    {
        currentLife = maxLife;
        currentGameState = GameState.GameStart;
        gameOverCanvas.SetActive(false);
        gameClearCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        switch (CheckCurrentGameState())
        {
            case GameState.GameStart:
                ShowCurrentLife();
                CheckCurrentLife();
                break;
            case GameState.GameOver:
                GameOver();
                break;
            case GameState.GameClear:
                GameClear();
                break;
        }
    }

    private GameState CheckCurrentGameState()
    {
        return currentGameState;
    }

    private void ShowCurrentLife()
    {
        currentLifeText.text = currentLife.ToString();
    }

    private void CheckCurrentLife()
    {
        if(currentLife <= 0)
        {
            Instance.ChangeGameState(GameState.GameOver);
        }
    }

    public void ReduceLife()
    {
        currentLife--;
    }

    public void ReachedGoal()
    {
        Instance.ChangeGameState(GameState.GameClear);
    }

    private void ChangeGameState(GameState targetGameState)
    {
        currentGameState = targetGameState;
    }


    private void GameOver()
    {
        gameOverCanvas.SetActive(true);
    }

    private void GameClear()
    {
        gameClearCanvas.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public GameState GetCurrentGameState()
    {
        return currentGameState;
    }
}

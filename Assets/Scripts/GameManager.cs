using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public Text titleText;        // タイトル
    public Text instructionText;  //「左」「右」
    public Text resultText;       //リザルト
    public Text progressText;     //現在の回数（例: 3 / 10）
    public Button startButton;    //スタートボタン

    [SerializeField] int totalRounds = 10;
    public int currentRound = 0;
    public int mistakeCount = 0;

    private bool isPlaying = false;
    private string currentDirection = "";
    private float startTime;

    private void Awake()
    {
        mistakeCount = 0;
    }

    void Start()
    {
        titleText.text = "反射神経ゲーム";
        resultText.text = "";
        instructionText.text = "";
        progressText.text = "";
        startButton.onClick.AddListener(StartGame);
    }

    void Update()
    {
        if (!isPlaying)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0)) // 左クリック
        {
            CheckInput("←");
        }
        else if (Input.GetMouseButtonDown(1)) // 右クリック
        {
            CheckInput("→");
        }
    }

    void StartGame()
    {
        titleText.text = "";
        resultText.text = "";

        startButton.gameObject.SetActive(false);
        currentRound = 0;
        isPlaying = true;
        startTime = Time.time;
        NextRound();
    }

    void NextRound()
    {
        if (currentRound >= totalRounds)
        {
            EndGame();
            return;
        }

        currentDirection = Random.value > 0.5f ? "←" : "→";
        instructionText.text = currentDirection;
        progressText.text = $"{currentRound + 1} / {totalRounds}";
    }

    void CheckInput(string input)
    {
        if (input == currentDirection)
        {
            currentRound++;
            NextRound();
        }
        else
        {
            mistakeCount++;
            ResetGame();
        }
    }

    void ResetGame()
    {
        instructionText.text = "ミス！";
        progressText.text = "";
        isPlaying = false;
        Invoke(nameof(RestartAfterMistake), 1.5f);
    }

    void RestartAfterMistake()
    {
        StartGame();
    }

    void EndGame()
    {
        isPlaying = false;

        float clearTime = Time.time - startTime;

        instructionText.text = "";
        progressText.text = "";

        resultText.text = $"クリア！\nクリア時間: {clearTime:F2} 秒\nミスの回数: {mistakeCount} 回";
        titleText.text = "反射神経ゲーム";
        startButton.gameObject.SetActive(true);
    }
}
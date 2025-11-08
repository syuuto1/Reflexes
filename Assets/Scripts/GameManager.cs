using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public Text titleText;        // タイトル
    public Text instructionText;  //「左」「右」
    public Text resultText;       //リザルト
    public Text progressText;     //現在の回数
    public Button startButton;    //スタートボタン

    [SerializeField] int totalRounds = 10; //ラウンド数
    public int currentRound = 0;
    public int mistakeCount = 0;

    private bool isPlaying = false;
    private string currentDirection = "";
    private float startTime;

    private void Awake()
    {
        mistakeCount = 0; //ミス回数の初期化
    }

    void Start()
    {
        //各テキストの初期化
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

    /// <summary>
    /// スタートボタンを押した時
    /// </summary>
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

    /// <summary>
    /// 次のラウンド
    /// </summary>
    void NextRound()
    {
        //ラウンドの終了
        if (currentRound >= totalRounds)
        {
            EndGame();
            return;
        }

        currentDirection = Random.value > 0.5f ? "←" : "→";     //ランダムに表示
        
        //テキスト表示
        instructionText.text = currentDirection;
        progressText.text = $"{currentRound + 1} / {totalRounds}";
    }

    /// <summary>
    /// 入力のチェック
    /// </summary>
    /// <param name="input">クリックの方向</param>
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

    /// <summary>
    /// ミス時の処理
    /// </summary>
    void ResetGame()
    {
        instructionText.text = "ミス！";
        progressText.text = "";
        isPlaying = false;
        Invoke(nameof(RestartAfterMistake), 1.5f);
    }

    /// <summary>
    /// リスタート処理
    /// </summary>
    void RestartAfterMistake()
    {
        StartGame();
    }

    /// <summary>
    /// クリア時処理
    /// </summary>
    void EndGame()
    {
        isPlaying = false;

        float clearTime = Time.time - startTime; //経過時間の算出

        instructionText.text = "";
        progressText.text = "";

        resultText.text = $"クリア！\nクリア時間: {clearTime:F2} 秒\nミスの回数: {mistakeCount} 回";
        titleText.text = "反射神経ゲーム";
        startButton.gameObject.SetActive(true);
    }
}
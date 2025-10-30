using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public Text titleText;        // �^�C�g��
    public Text instructionText;  //�u���v�u�E�v
    public Text resultText;       //���U���g
    public Text progressText;     //���݂̉񐔁i��: 3 / 10�j
    public Button startButton;    //�X�^�[�g�{�^��

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
        titleText.text = "���ː_�o�Q�[��";
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

        if (Input.GetMouseButtonDown(0)) // ���N���b�N
        {
            CheckInput("��");
        }
        else if (Input.GetMouseButtonDown(1)) // �E�N���b�N
        {
            CheckInput("��");
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

        currentDirection = Random.value > 0.5f ? "��" : "��";
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
        instructionText.text = "�~�X�I";
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

        resultText.text = $"�N���A�I\n�N���A����: {clearTime:F2} �b\n�~�X�̉�: {mistakeCount} ��";
        titleText.text = "���ː_�o�Q�[��";
        startButton.gameObject.SetActive(true);
    }
}
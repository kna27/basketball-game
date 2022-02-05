using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static Vector3 player1Pos;
    private static Vector3 player2Pos;
    private static Vector3 bal;

    public static int ballHolder;
    public static int scoreOne = 0;
    public static int scoreTwo = 0;
    public static float timeLeft;
    [SerializeField] private Text scoreOneText;
    [SerializeField] private Text scoreTwoText;
    [SerializeField] private Text timeText;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private static float scoreTimeout = 0.5f;
    public static float waitTime;
    private void Start()
    {
        scoreOne = 0;
        scoreTwo = 0;
        player1Pos = GameObject.Find("Player1").transform.position;
        player2Pos = GameObject.Find("Player2").transform.position;
        bal = GameObject.Find("Ball").transform.position;
        timeLeft = 5;
        gameOverPanel.SetActive(false);

    }

    void Update()
    {
        scoreOneText.text = scoreOne.ToString();
        scoreTwoText.text = scoreTwo.ToString();
        timeText.text = timeLeft <= 0 ? "0.0" : timeLeft.ToString("F1");
        timeLeft -= timeLeft <= 0 ? 0 : Time.deltaTime;
        if (timeLeft <= 0)
        {
            EndGame(gameOverPanel);
        }
    }

    public static void ChangeScore(int team, int score)
    {
        waitTime = scoreTimeout;
        if (team == 1)
        {
            scoreTwo += score;
        }
        else
        {
            scoreOne += score;
        }
        DisplayText("Team " + team + " scored");
    }



    private static void ResetGame()
    {
        GameObject.Find("Player1").transform.position = player1Pos;
        GameObject.Find("Player2").transform.position = player2Pos;
        GameObject.Find("Ball").GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GameObject.Find("Ball").transform.position = bal;
    }
    private void FixedUpdate()
    {
        waitTime -= waitTime <= 0f ? 0 : Time.deltaTime;
    }

    public static void DisplayText(string text)
    {
        Debug.Log(text);
        ResetGame();
    }

    private static void EndGame(GameObject endPanel)
    {
        endPanel.SetActive(true);
        endPanel.transform.GetChild(0).GetComponent<Text>().text = "Player " + (scoreOne >= scoreTwo ? "1" : "2") + " won!";
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }
}

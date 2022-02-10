using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static Vector3 player1Pos = new Vector3(-6f, -2.49f, 0);
    private static Vector3 player2Pos = new Vector3(6f, -2.49f, 0);
    private static int newParentPlayer;
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
        timeLeft = 60;
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
        newParentPlayer = team;
        waitTime = scoreTimeout;
        if (team == 1)
        {
            scoreTwo += score;
        }
        else
        {
            scoreOne += score;
        }
        DisplayText("Team " + (team == 1 ? 2 : 1) + " scored");
    }
    
    private static void ResetGame()
    {
        GameObject.Find("Player1").transform.position = player1Pos;
        GameObject.Find("Player2").transform.position = player2Pos;
        GameObject.Find("Ball").GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GameObject.Find("Player1").GetComponent<Movement>().jumpedWithBall = false;
        GameObject.Find("Player2").GetComponent<Movement>().jumpedWithBall = false;
        GameObject.Find("Ball").transform.GetChild(0).GetComponent<Ball>().ParentBall(GameObject.Find("Player" + newParentPlayer).transform);
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

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    private static Vector3 player1Pos;
    private static Vector3 player2Pos;
    private static Vector3 bal;

    public static int ballHolder;
    public static int scoreOne = 0;
    public static int scoreTwo = 0;
    [SerializeField] private Text scoreOneText;
    [SerializeField] private Text scoreTwoText;
    [SerializeField] private Text debugInfo;
    [SerializeField] private static float scoreTimeout = 0.5f;
    public static float waitTime;
    private void Start()
    {
        player1Pos = GameObject.Find("Player1").transform.position;
        player2Pos = GameObject.Find("Player2").transform.position;
        bal = GameObject.Find("Ball").transform.position;

    }

    void Update()
    {
        scoreOneText.text = scoreOne.ToString();
        scoreTwoText.text = scoreTwo.ToString();
        debugInfo.text = string.Format("Ball Holder: {0}", ballHolder);
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

}

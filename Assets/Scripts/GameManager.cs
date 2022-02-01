using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private Vector3 player1Pos;
    private Vector3 player2Pos;
    private Vector3 bal;

    public static int ballHolder;
    public static int scoreOne = 0;
    public static int scoreTwo = 0;
    [SerializeField] private Text scoreOneText;
    [SerializeField] private Text scoreTwoText;
    [SerializeField] private Text debugInfo;

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
        if (team == 1)
        {
            scoreTwo += score;
        }
        else
        {
            scoreOne += score;
        }
    }

}

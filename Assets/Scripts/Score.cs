using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour
{
    public int team;
    private static DisableColliders disabled;

    void Start()
    {
        disabled = GameObject.Find("Game Manager").GetComponent<DisableColliders>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.transform.name == "Ball")
        {
            if (team != GameManager.ballHolder && GameManager.waitTime <= 0f)
            {
                GameManager.ChangeScore(team, col.transform.GetChild(0).GetComponent<Ball>().distToNetOnThrow >= 1.02 ? 3 : 2);
            }
        }
    }
    public void DisplayText(string text, Vector3 player1Pos, Vector3 player2Pos, int newParentPlayer)
    {
        Debug.Log(text);
        StartCoroutine(ResetGame(player1Pos, player2Pos, newParentPlayer));
    }
    public IEnumerator ResetGame(Vector3 player1Pos, Vector3 player2Pos, int newParentPlayer)
    {
        disabled.DisableCollider();
        yield return new WaitForSeconds(2);
        GameObject.Find("Player1").transform.position = player1Pos;
        GameObject.Find("Player2").transform.position = player2Pos;
        GameObject.Find("Ball").GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GameObject.Find("Player1").GetComponent<Movement>().jumpedWithBall = false;
        GameObject.Find("Player2").GetComponent<Movement>().jumpedWithBall = false;
        GameObject.Find("Ball").transform.GetChild(0).GetComponent<Ball>().ParentBall(GameObject.Find("Player" + newParentPlayer).transform);
    }
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEditor;

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
        GameObject textObj = (GameObject) Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/textTemplate.prefab", typeof(GameObject)), GameObject.Find("Canvas").transform);
        textObj.GetComponent<Text>().text = text;
        StartCoroutine(ResetGame(player1Pos, player2Pos, newParentPlayer));
    }

    public IEnumerator ResetGame(Vector3 player1Pos, Vector3 player2Pos, int newParentPlayer)
    {
        disabled.DisableCollider();
        GameObject.Find("Player1").GetComponent<Movement>().enabled = false;
        GameObject.Find("Player2").GetComponent<Movement>().enabled = false;
        yield return new WaitForSeconds(2);
        GameObject.Find("Player1").transform.position = player1Pos;
        GameObject.Find("Player2").transform.position = player2Pos;
        GameObject.Find("Ball").GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GameObject.Find("Player1").GetComponent<Movement>().jumpedWithBall = false;
        GameObject.Find("Player2").GetComponent<Movement>().jumpedWithBall = false;
        GameObject.Find("Player1").GetComponent<Movement>().enabled = true;
        GameObject.Find("Player2").GetComponent<Movement>().enabled = true;
        GameObject.Find("Ball").transform.GetChild(0).GetComponent<Ball>().ParentBall(GameObject.Find("Player" + newParentPlayer).transform);
    }

    public void GameEnd(GameObject endPanel, int scoreOne, int scoreTwo)
    {
        StartCoroutine(GameEndWait(endPanel, scoreOne, scoreTwo));
    }

    public IEnumerator GameEndWait(GameObject endPanel, int scoreOne, int scoreTwo)
    {
        GameObject.Find("Player1").GetComponent<Movement>().enabled = false;
        GameObject.Find("Player2").GetComponent<Movement>().enabled = false;
        yield return new WaitForSeconds(2);
        GameObject.Find("Ball").GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        endPanel.SetActive(true);
        endPanel.transform.GetChild(0).GetComponent<Text>().text = "Player " + (scoreOne >= scoreTwo ? "1" : "2") + " won!";
    }
}

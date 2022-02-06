using UnityEngine;

public class Score : MonoBehaviour
{
    public int team;

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.transform.name == "Ball")
        {
            if (team != GameManager.ballHolder && GameManager.waitTime <= 0f)
            {
                GameManager.ChangeScore(team, col.transform.GetChild(0).GetComponent<Ball>().distToNetOnThrow >= 1.25 ? 3 : 2);
            }
        }
    }
}

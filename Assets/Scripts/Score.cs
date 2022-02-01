using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public int team;

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.transform.name == "Ball")
        {
            if (team != GameManager.ballHolder)
            {
                GameManager.ChangeScore(team, 1);
            }
        }

    }
}

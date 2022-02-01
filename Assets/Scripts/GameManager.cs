using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static int ballHolder;
    public static int scoreOne = 0;
    public static int scoreTwo = 0;
    [SerializeField] private Text scoreOneText;
    [SerializeField] private Text scoreTwoText;
    [SerializeField] private Text debugInfo;

    void Update()
    {
        scoreOneText.text = scoreOne.ToString();
        scoreTwoText.text = scoreTwo.ToString();
        debugInfo.text = string.Format("Ball Holder: {0}", ballHolder);
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsScroll : MonoBehaviour
{
    [Range(0f, 1f)]
    [SerializeField] float speed;
    [SerializeField] int endY;
    private int startY;
    private float t = 0.0f;

    void Start()
    {
        startY = (int)GetComponent<RectTransform>().anchoredPosition.y;
    }

    void Update()
    {
        GetComponent<RectTransform>().anchoredPosition = new Vector2(GetComponent<RectTransform>().anchoredPosition.x, Mathf.Lerp(startY, endY, t));
        t += speed * Time.deltaTime;
        if (GetComponent<RectTransform>().anchoredPosition.y >= endY - 0.1f)
        {
            SceneManager.LoadScene(0);
        }
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PopupText : MonoBehaviour
{
    private float startSize;
    private float endSize;
    private float t = 0.0f;
    private void Start()
    {
        startSize = GetComponent<Text>().fontSize / 3;
        endSize = GetComponent<Text>().fontSize * 2.5f;
    }

    void Update()
    {
        StartCoroutine(DestroyObj());
        GetComponent<Text>().fontSize = (int)Mathf.Lerp(startSize, endSize, t);
        t += 1f * Time.deltaTime;
    }

    IEnumerator DestroyObj()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}

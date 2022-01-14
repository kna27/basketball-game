using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float speed = 6.5f;
    public GameObject Player1;
    public GameObject Player2;

    void Update()
    {
        Player1.transform.Translate(Input.GetAxis("Horizontal") * Time.deltaTime * speed, 0, 0);
        Player2.transform.Translate(Input.GetAxis("Horizontal2") * Time.deltaTime * speed, 0, 0);
    }
}

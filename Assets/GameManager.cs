using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float speed = 3f;
    public GameObject Player1;
    public GameObject Player2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Player1.transform.Translate(Input.GetAxis("Horizontal") * Time.deltaTime * speed, 0, 0);
    }
}

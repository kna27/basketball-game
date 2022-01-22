using UnityEngine;

public class Ball : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (LayerMask.LayerToName(col.gameObject.layer) == "Player")
        {
            GameManager.ballHolder = col.gameObject.GetComponent<Movement>().team;
            transform.parent.transform.parent = col.transform;
            transform.parent.GetComponent<Rigidbody2D>().simulated = false;
            transform.parent.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Collider2D>().enabled = false;
            transform.parent.transform.localPosition = new Vector3(1f, 0.5f, 0f);
        }
    }
}

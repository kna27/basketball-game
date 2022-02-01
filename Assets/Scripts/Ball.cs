using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float pickupDelay = 1f;
    [SerializeField] private Vector2 throwForce;
    private float pickupTime = 0f;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = transform.parent.GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (LayerMask.LayerToName(col.gameObject.layer) == "Player" && pickupTime <= 0f)
        {
            GameManager.ballHolder = col.gameObject.GetComponent<Movement>().team;
            transform.parent.transform.parent = col.transform;
            rb.simulated = false;
            rb.velocity = Vector2.zero;
            GetComponent<Collider2D>().enabled = false;
            transform.parent.transform.localPosition = new Vector3(1f, 0.5f, 0f);
        }
    }

    public void ThrowBall()
    {
        float throwDir = transform.parent.transform.parent.GetComponent<Movement>().team == 1 ? 1 : -1;
        transform.parent.transform.parent = null;
        rb.simulated = true;
        rb.velocity = new Vector2(throwForce.x * throwDir, throwForce.y);
        pickupTime = pickupDelay;
        GetComponent<Collider2D>().enabled = true;
    }

    private void Update()
    {
        pickupTime -= pickupTime <= 0 ? 0f : Time.deltaTime;
    }
}
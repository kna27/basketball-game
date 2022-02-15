using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float pickupDelay = 0.1f;
    [SerializeField] private Vector2 throwForce;
    public float distToNetOnThrow;
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
            if (!col.GetComponent<Movement>().stunned)
            {
                ParentBall(col.transform);
            }
        }
        else if (LayerMask.LayerToName(col.gameObject.layer) != "Player" && col.name != "hand_right")
        {
            GetComponent<AudioSource>().Play();
        }
    }

    public void ParentBall(Transform parent)
    {
        GameManager.ballHolder = parent.gameObject.GetComponent<Movement>().team;
        transform.parent.transform.parent = parent;
        rb.simulated = false;
        rb.velocity = Vector2.zero;
        GetComponent<Collider2D>().enabled = false;
        transform.parent.transform.localPosition = new Vector3(1f, 0.5f, 0f);
    }

    public void ThrowBall()
    {
        distToNetOnThrow = Vector3.Distance(transform.position, GameObject.Find(transform.parent.transform.parent.GetComponent<Movement>().team == 1 ? "Goal2" : "Goal1").transform.position) / 10;
        float throwDir = transform.parent.transform.parent.GetComponent<Movement>().team == 1 ? 1 : -1;
        transform.parent.transform.parent = null;
        rb.simulated = true;
        rb.velocity = new Vector2(throwForce.x * throwDir * distToNetOnThrow, throwForce.y);
        pickupTime = pickupDelay;
        GetComponent<Collider2D>().enabled = true;
    }

    public void DropBall()
    {
        float throwDir = transform.parent.transform.parent.GetComponent<Movement>().team == 1 ? 1 : -1;
        transform.parent.transform.parent = null;
        rb.simulated = true;
        rb.velocity = new Vector2(throwForce.x * throwDir / 5, 1);
        pickupTime = pickupDelay;
        GetComponent<Collider2D>().enabled = true;
    }

    private void Update()
    {
        pickupTime -= pickupTime <= 0 ? 0f : Time.deltaTime;
    }
}

using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float m_JumpForce = 450f;
    [SerializeField] private float m_MoveSpeed = 6.5f;
    [SerializeField] private float m_DashSpeed = 10f;
    [SerializeField] private float m_DashCooldown = 1f;
    public int team;
    [SerializeField] private Collider2D m_GroundCheck;
    private Collider2D m_Ground;

    const float k_GroundedRadius = 0.2f;
    const float k_MinX = -10f;
    const float k_MaxX = 10f;
    private bool m_Grounded;
    private bool m_FacingRight = true;


    private void Awake()
    {
        m_Ground = GameObject.Find("Ground").GetComponent<Collider2D>();
    }

    private void Update()
    {
        bool wasGrounded = m_Grounded;
        if (m_GroundCheck.Distance(m_Ground).distance < k_GroundedRadius)
        {
            m_Grounded = true;
            if (!wasGrounded)
            {
                if (GameManager.ballHolder == team)
                {
                    transform.GetChild(0).transform.GetChild(0).GetComponent<Ball>().ThrowBall();
                }
            }
        }
        else
        {
            m_Grounded = false;
        }
        Move(Input.GetAxisRaw("Horizontal" + team), Input.GetButtonDown("Jump" + team));
    }

    public void Move(float move, bool jump)
    {
        if (m_Grounded && jump)
        {
            m_Grounded = false;
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, m_JumpForce));
        }
        transform.Translate(move * Time.deltaTime * m_MoveSpeed, 0, 0);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, k_MinX, k_MaxX), transform.position.y, 0);

        if (move > 0 && !m_FacingRight)
        {
            Flip();
        }
        else if (move < 0 && m_FacingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        m_FacingRight = !m_FacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
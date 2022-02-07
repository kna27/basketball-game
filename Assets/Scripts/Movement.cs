using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float m_JumpForce = 450f;
    [SerializeField] private float m_MoveSpeed = 6.5f;
    [SerializeField] private float m_DashSpeed = 10f;
    [SerializeField] private float m_DashCooldown = 1f;
    public int team;
    private Animator animator;
    [SerializeField] private Collider2D m_GroundCheck;
    private Collider2D m_Ground;

    const float k_GroundedRadius = 0.2f;
    const float k_MinX = -10f;
    const float k_MaxX = 10f;
    private bool m_Grounded;
    private int landThrowCooldown;
    public bool jumpedWithBall;

    private void Awake()
    {
        m_Ground = GameObject.Find("Ground").GetComponent<Collider2D>();
        animator = transform.GetChild(0).GetComponent<Animator>();
    }

    private void Update()
    {
        bool wasGrounded = m_Grounded;
        if (m_GroundCheck.Distance(m_Ground).distance < k_GroundedRadius)
        {
            m_Grounded = true;
            if (!wasGrounded && jumpedWithBall)
            {
              Throw();
            }
        }
        else
        {
            m_Grounded = false;
        }
        Move(Input.GetAxisRaw("Horizontal" + team), Input.GetButtonDown("Jump" + team));
        if(Input.GetButton("Throw" + team))
        {
            Throw();
        }
        landThrowCooldown -= landThrowCooldown > 0 ? 1 : 0;
    }

    public void Move(float move, bool jump)
    {
        if (m_Grounded && jump)
        {
            animator.Play("Jump");
        }
        else if (move != 0)
        {
            animator.Play("Walk");
        }
        else
        {
            animator.Play("Idle");
        }
        if (m_Grounded && jump)
        {
            m_Grounded = false;
            landThrowCooldown = 10;
            jumpedWithBall = transform.childCount > 0;
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, m_JumpForce));
        }
        transform.Translate(move * Time.deltaTime * m_MoveSpeed, 0, 0);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, k_MinX, k_MaxX), transform.position.y, 0);
    }
    public void Throw()
    {
        if (GameManager.ballHolder == team && transform.childCount > 1 && landThrowCooldown <= 0)
        {
            transform.GetChild(1).transform.GetChild(0).GetComponent<Ball>().ThrowBall();
            jumpedWithBall = false;
        }
    }
}
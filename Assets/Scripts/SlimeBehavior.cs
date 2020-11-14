using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBehavior : MonoBehaviour
{
    [SerializeField]
    private float m_speed;
    private float m_horizontalInput;
    [SerializeField]
    private float m_fallAccelSpeed = 35.0f;
    private bool m_isFacingLeft = true;
    private Rigidbody2D m_rigid2D;

    [SerializeField]
    private float m_jumpSpeed = 10.0f;
    [SerializeField]
    private Transform m_groundCheck;
    [SerializeField]
    private Transform m_wallCheck;
    [SerializeField]
    private LayerMask m_whatIsPlatform;
    [SerializeField]
    private float m_gravitySpeed;
    [SerializeField]
    private float m_mass;
    private float m_checkRadius = 0.1f;
    private bool m_isRunning = false;
    private bool m_isJumping = false;
    private bool m_isHitGround;
    private RaycastHit2D m_hitWall;

    private bool m_isDestroyed = false;

    [SerializeField]
    private Animator animator;

    private void Awake()
    {
        m_rigid2D = this.transform.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        HorizontalMovement();
        Jump();
        WallDetection();
        AnimateSlime();
        Debug.Log(m_groundCheck);
    }

    private void FixedUpdate()
    {
        m_isHitGround = Physics2D.OverlapCircle(
            m_groundCheck.position, m_checkRadius, m_whatIsPlatform);
        if(m_isFacingLeft)
        {
            m_hitWall = Physics2D.Raycast(
                m_wallCheck.position, Vector2.left, m_checkRadius, m_whatIsPlatform);
        }
        else
        {
            m_hitWall = Physics2D.Raycast(
                m_wallCheck.position, Vector2.right, m_checkRadius, m_whatIsPlatform);
        }
        
    }

    private void HorizontalMovement()
    {
        m_horizontalInput = Input.GetAxis("Horizontal");
        Vector2 direction = new Vector2(m_horizontalInput, 0);
        transform.Translate(direction * m_speed * Time.deltaTime);

        if(Mathf.Abs(m_horizontalInput) != 0)
        {
            m_isRunning = true;
        }
        else
        {
            m_isRunning = false;
        }

        if (m_horizontalInput > 0 && m_isFacingLeft == true)
        {
            AnimateFlip();
            m_isFacingLeft = !m_isFacingLeft;
        }
        else if (m_horizontalInput < 0 && m_isFacingLeft == false)
        {
            AnimateFlip();
            m_isFacingLeft = !m_isFacingLeft;
        }
    }

    private void Jump()
    {
        if (m_isHitGround)
        {
            m_isJumping = false;
            if (Input.GetKeyDown(KeyCode.W))
            {
                m_rigid2D.velocity = Vector2.up * m_jumpSpeed;
            }
        }
        else
        {
            m_isJumping = true;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (m_isFacingLeft == true)
                {
                    m_rigid2D.velocity = new Vector2(-20, -m_fallAccelSpeed);
                }
                else
                {
                    m_rigid2D.velocity = new Vector2(20, -m_fallAccelSpeed);
                }
            }
        }
        // Custom Gravity value
        m_rigid2D.AddForce(Vector2.down * m_gravitySpeed * m_mass);
    }

    private void WallDetection()
    {
        if (m_hitWall)
        {
            m_speed = 0;
        }
        else
        {
            m_speed = 25;
        }
    }

    private void AnimateSlime()
    {
        animator.SetBool("IsRunning", m_isRunning);
        animator.SetBool("IsJumping", m_isJumping);
    }

    private void AnimateFlip()
    {
        Vector2 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public void DestroyPlayer()
    {
        m_isDestroyed = true;
        if(m_isDestroyed)
        {
            Destroy(this.gameObject);
        }
    }
}

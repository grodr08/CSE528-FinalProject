using UnityEngine;

public class PlatformerCharacter2D : MonoBehaviour
{
    [SerializeField] private float m_MaxSpeed = 10f;
    [SerializeField] private float m_JumpForce = 400f;
    [SerializeField] private bool m_AirControl = false;
    [SerializeField] private LayerMask m_WhatIsGround;
    [SerializeField] private GameObject m_TargetObject;

    private Transform m_GroundCheck;
    const float k_GroundedRadius = .2f;
    private bool m_Grounded;
    private Animator m_Anim;
    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true;
    private AudioSource m_AudioSource;

    private void Awake()
    {
        // 1. Ground Check
        m_GroundCheck = transform.Find("GroundCheck");
        if (m_GroundCheck == null)
        {
            GameObject groundCheck = new GameObject("GroundCheck");
            groundCheck.transform.parent = transform;
            groundCheck.transform.localPosition = Vector3.zero;
            m_GroundCheck = groundCheck.transform;
        }

        // 2. Component References
        m_Anim = GetComponent<Animator>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_AudioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        m_Grounded = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
                m_Grounded = true;
        }

        if (m_Anim != null)
        {
            SafeSetBool("Ground", m_Grounded);
            SafeSetFloat("vSpeed", m_Rigidbody2D.linearVelocity.y);
        }
    }

    public void Move(float move, bool jump)
    {
        if (m_Grounded || m_AirControl)
        {
            SafeSetFloat("Speed", Mathf.Abs(move) * m_MaxSpeed);
            m_Rigidbody2D.linearVelocity = new Vector2(move * m_MaxSpeed, m_Rigidbody2D.linearVelocity.y);

            if ((move > 0 && !m_FacingRight) || (move < 0 && m_FacingRight))
            {
                Flip();
            }
        }

        if (m_Grounded && jump)
        {
            m_Grounded = false;
            SafeSetBool("Ground", false);
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
        }
    }

    private void Flip()
    {
        m_FacingRight = !m_FacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Coins"))
        {
            CollectCoin(collider);
        }
    }

    private void CollectCoin(Collider2D coinCollider)
    {
        m_TargetObject.SendMessage("DestroyObject", coinCollider.gameObject);
        m_AudioSource.Play();
    }

    #region Animation Safety
    private bool HasParameter(string paramName, Animator animator)
    {
        foreach (AnimatorControllerParameter param in animator.parameters)
        {
            if (param.name == paramName) return true;
        }
        return false;
    }

    private void SafeSetBool(string param, bool value)
    {
        if (m_Anim != null && HasParameter(param, m_Anim))
            m_Anim.SetBool(param, value);
    }

    private void SafeSetFloat(string param, float value)
    {
        if (m_Anim != null && HasParameter(param, m_Anim))
            m_Anim.SetFloat(param, value);
    }
    #endregion
}

using UnityEngine;

[RequireComponent(typeof(PlatformerCharacter2D))]
public class Platformer2DUserControl : MonoBehaviour
{
    private PlatformerCharacter2D m_Character;
    private bool m_Jump;

    private void Awake()
    {
        m_Character = GetComponent<PlatformerCharacter2D>();
    }

    private void Update()
    {
        // Always check jump input in Update for responsiveness
        if (Input.GetButtonDown("Jump"))
        {
            m_Jump = true;
        }
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");

        // Keep jump state until consumed
        m_Character.Move(h, m_Jump);

        // Reset AFTER passing to character
        m_Jump = false;
    }
}

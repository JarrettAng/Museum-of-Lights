using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Animator m_animator;
    [SerializeField]
    private Rigidbody m_rb;

    [SerializeField]
    private float m_speed = 5.0f;
    [SerializeField]
    private float m_maxSpeed = 5.0f;

    private void Update() {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector2 direction = Vector2.zero;
        direction.x = (horizontal > 0.1f) ? 1 : (horizontal < -0.1f) ? -1 : 0;
        direction.y = (vertical > 0.1f) ? 1 : (vertical < -0.1f) ? -1 : 0;

        if (direction.sqrMagnitude >= 0.1f) {
            m_animator.SetBool("Moving", true);

            direction.x *= m_speed * Time.deltaTime;
            direction.y *= m_speed * Time.deltaTime;

            // Move the player horizontally, transform forward and right
            m_rb.AddForce(transform.forward * direction.y, ForceMode.VelocityChange);
            m_rb.AddForce(transform.right * direction.x, ForceMode.VelocityChange);

            // Clamp the player's horiontal speeed
            m_rb.linearVelocity = new Vector3(Mathf.Clamp(m_rb.linearVelocity.x, -m_maxSpeed, m_maxSpeed), m_rb.linearVelocity.y, Mathf.Clamp(m_rb.linearVelocity.z, -m_maxSpeed, m_maxSpeed));

        }
        else {
            m_animator.SetBool("Moving", false);

            // Stop the player moving horizontally
            m_rb.linearVelocity = new Vector3(0.0f, m_rb.linearVelocity.y, 0.0f);
        }
    }
}

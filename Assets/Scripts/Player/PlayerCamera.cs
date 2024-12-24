using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField]
    private Transform m_camera;

    [SerializeField]
    private float m_speed = 2.0f;
    [SerializeField]
    private Vector2 m_upDownLimit = new Vector2(-70.0f, 70.0f);

    private Vector2 m_rotation;

    public bool m_Enabled { get; set; }

    private void Awake() {
        m_Enabled = true;
    }

    private void Update() {
        if (m_Enabled) { 
            m_rotation.x -= Input.GetAxisRaw("Mouse Y") * m_speed;
            m_rotation.y += Input.GetAxisRaw("Mouse X") * m_speed;
        }
        

        m_rotation.x = Mathf.Clamp(m_rotation.x, m_upDownLimit.x, m_upDownLimit.y);
        SetRotation(m_rotation);

        Vector3 camPos = m_camera.localPosition;
        m_camera.localPosition = camPos;
    }

    public void SetRotation(Quaternion lookRotation) {
        SetRotation(lookRotation.eulerAngles, true);
    }

    public void SetRotation(Vector3 lookRotation, bool overwrite = false) {
        // Set the camera and player rotation (camera is x, player is y)
        m_camera.localRotation = Quaternion.Euler(m_rotation.x, 0, 0);
        transform.rotation = Quaternion.Euler(0, lookRotation.y, 0);

        if (overwrite) {
            m_rotation = lookRotation;
        }
    }

    public Quaternion GetRotation() {
        return Quaternion.Euler(m_rotation);
    }
}

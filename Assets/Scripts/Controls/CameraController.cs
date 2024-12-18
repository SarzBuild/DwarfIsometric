using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour, PlayerControls.ICameraActions
{
    [SerializeField] private Transform m_target = null;
    
    [Header("Distance Related")]
    [SerializeField] [Range(0.01f, 1f)] private float m_lerpScrollSpeed = 0.0f;
    [SerializeField] [Range(MIN_DISTANCE, MAX_DISTANCE)] private float m_startingDistance = 0.0f;
    
    [Header("Rotation Related")]
    [SerializeField] [Range(0.01f, 5.0f)] private float m_rotationSpeed = 0.0f;
    [SerializeField] [Range(0.0f, 360.0f)] private float m_startingRotation = 45.0f;
    [SerializeField] [Range(0.0f, 180.0f)] private float m_rotationStep = 90.0f;
 
    private PlayerControls m_controls = null;
    
    private float m_currentDistance = 5.0f;
    private float m_desiredDistance = 5.0f;

    private float m_currentRotation = 45.0f;
    private float m_desiredRotation = 45.0f;
    
    private const float MIN_DISTANCE = 5.0f;
    private const float MAX_DISTANCE = 45.0f;
    
    private void Awake()
    {
        m_controls = new PlayerControls();
        m_controls.Camera.AddCallbacks(this);
    }

    private void Start()
    {
        m_currentDistance = m_startingDistance;
        m_desiredDistance = m_startingDistance;
        
        m_currentRotation = m_startingRotation;
        m_desiredRotation = m_startingRotation;
    }

    private void OnEnable()
    {
        m_controls.Camera.Enable();
    }

    private void OnDisable()
    {
        m_controls.Camera.Disable();
    }
    
    public void OnCameraScroll(InputAction.CallbackContext context)
    {
        SetCameraDistance(context.ReadValue<float>());
    }

    private void SetCameraDistance(float scrollDelta)
    {
        m_desiredDistance = Mathf.Clamp(m_desiredDistance + -scrollDelta, MIN_DISTANCE, MAX_DISTANCE);
    }
    
    public void OnCameraRotation(InputAction.CallbackContext context)
    {
        SetCameraRotation((int)context.ReadValue<float>());
    }

    private void SetCameraRotation(int direction)
    {
        m_desiredRotation += direction * m_rotationStep;
        m_desiredRotation = Mathf.Repeat(m_desiredRotation, 360);
    }

    private void FixedUpdate()
    {
       ApplyCameraDistance();
       ApplyCameraRotation();
    }

    private void ApplyCameraDistance()
    {
        m_currentDistance = Mathf.Lerp(m_currentDistance, m_desiredDistance, m_lerpScrollSpeed);
        Camera.main.orthographicSize = m_currentDistance;
    }

    private void ApplyCameraRotation()
    {
        m_currentRotation = Mathf.LerpAngle(transform.eulerAngles.y, m_desiredRotation, Time.fixedDeltaTime * m_rotationSpeed);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, m_currentRotation, transform.eulerAngles.z);
    }

    private void LateUpdate()
    {
        transform.position = m_target.position;
    }
}

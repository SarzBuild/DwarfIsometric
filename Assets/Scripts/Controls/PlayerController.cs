using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour, PlayerControls.IPlayerActions
{
    public static PlayerController Instance;
    
    public Inventory PlayerInventory = null;

    [SerializeField] private NavMeshAgent m_agent = null;
    [SerializeField] private ParticleSystem m_pointClickParticle = null;

    private ParticleSystem m_particleGameObject = null;
    private PlayerControls m_controls = null;
    
    private void Awake()
    {
        if (Instance is not null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        m_controls = new PlayerControls();
        m_controls.Player.AddCallbacks(this);
    }

    private void Start()
    {
        PlayerInventory = new Inventory();
        PlayerInventory.Initialize();
    }

    private void OnEnable()
    {
        m_controls.Player.Enable();
    }

    private void OnDisable()
    {
        m_controls.Player.Disable();
    }
    
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed && !EventSystem.current.IsPointerOverGameObject())
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity))
            {
                NavMesh.SamplePosition(hitInfo.point, out NavMeshHit navHit, 100, NavMesh.AllAreas);
                
                var target = navHit.position;

                if (m_particleGameObject == null)
                {
                    m_particleGameObject = Instantiate(m_pointClickParticle);
                }

                m_particleGameObject.transform.position = target += new Vector3(0.0f, 0.1f, 0.0f);
                m_particleGameObject.Play();
                
                m_agent.SetDestination(target);

                var dir = (m_agent.destination - transform.position).normalized;
                transform.rotation = Quaternion.LookRotation(new Vector3(dir.x, 0.0f, dir.z));
            }
        }
    }
}

using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerInteractor : Interactor, PlayerControls.IInteractionActions
{
    [SerializeField] private Image m_fillImage = null;
    [SerializeField] private Vector3 m_fillImageOffset = Vector3.zero;

    private bool m_interacting = false;
    private PlayerControls m_controls = null;
    
    private void Awake()
    {
        m_controls = new PlayerControls();
        m_controls.Interaction.AddCallbacks(this);
    }
    
    private void OnEnable()
    {
        m_controls.Interaction.Enable();
    }

    private void OnDisable()
    {
        m_controls.Interaction.Disable();
    }
    
    private void Update()
    {
        CheckInteractionCollision();
    }

    protected override void CheckInteractionCollision()
    {
        base.CheckInteractionCollision();

        if (m_currentInteractable != null)
        {
            m_fillImage.transform.position = m_currentInteractable.transform.position + m_fillImageOffset;
            m_fillImage.transform.rotation = Camera.main.transform.rotation;
            m_fillImage.gameObject.SetActive(true);

            if (m_interacting)
            {
                m_fillImage.fillAmount -= Time.deltaTime / m_currentInteractable.PickupTimeAmount;
                if (m_fillImage.fillAmount <= 0.0f)
                {
                    PlayerController.Instance.PlayerInventory.AddItem(m_currentInteractable.ItemData);
                    Destroy(m_currentInteractable.gameObject);

                    m_fillImage.fillAmount = 1.0f;
                }
            }
        }
        else
        {
            m_fillImage.gameObject.SetActive(false);
            m_fillImage.fillAmount = 1.0f;
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            m_interacting = true;
        }
        else if(context.canceled)
        {
            m_interacting = false;
            m_fillImage.fillAmount = 1.0f;
        }
    }
}
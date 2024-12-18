using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class InventoryUIManager : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerMoveHandler, IPointerExitHandler
{
    public static InventoryUIManager Instance;
    
    public List<InventorySlotUI> slotsUI;
    
    [SerializeField] private float m_offsetDistance = 0.0f;
    [SerializeField] private GameObject m_hoverObject = null;
    [SerializeField] private GameObject m_inventoryMenu = null;
    [SerializeField] private GameObject m_inventoryButton = null;
    
    private HoverItem m_hoverItem = default;
    private GameObject m_draggedObject = null;
    
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
    }

    private void Start()
    {
        m_hoverItem = new HoverItem(m_hoverObject);
        m_hoverItem.SetVisibility(false);
        
        m_inventoryMenu.SetActive(false);
        m_inventoryButton.SetActive(true);
    }

    public void ToggleUIInventoryElements()
    {
        m_inventoryMenu.SetActive(!m_inventoryMenu.activeSelf);
        m_inventoryButton.SetActive(!m_inventoryButton.activeSelf);
    }

    public void InitUI(InventorySlot slot)
    {
        foreach (var uiSlot in slotsUI)
        {
            uiSlot.Initialize(slot);
        }
    }

    public void UpdateSlotUI(InventorySlot slot, int index)
    {
        var uiSlot = slotsUI.Find(ui => ui.InventorySlot == slot);

        if (uiSlot != null)
        {
            uiSlot.UpdateUI();
        }
        else
        {
            slotsUI[index].Initialize(slot);
        }
    }

    public void ResetSlotUI(InventorySlot slot, int index)
    {
        slotsUI[index].Initialize(slot);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        m_hoverItem.SetVisibility(false);
        
        var item = GetMatchingItem(eventData.hovered[0].transform.parent.transform);
        
        if (item != null && item.InventorySlot.ItemData.Sprite != null)
        {
            m_draggedObject = Instantiate(item.InventorySlot.ItemData.AssociatedPrefab);
            m_draggedObject.GetComponent<Interactable>().IsInteractable = false;
            
            PlayerController.Instance.PlayerInventory.RemoveItem(item.InventorySlot.ItemData);
        }
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        if (m_draggedObject != null)
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity))
            {
                NavMesh.SamplePosition(hitInfo.point, out NavMeshHit navHit, 100, NavMesh.AllAreas);
                
                m_draggedObject.transform.position = navHit.position + new Vector3(0.0f, m_draggedObject.transform.localScale.y / 2.0f, 0.0f);
            }
        }
    }
    
    public void OnEndDrag(PointerEventData eventData)
    {
        if(m_draggedObject != null)
            m_draggedObject.GetComponent<Interactable>().IsInteractable = true;
        m_draggedObject = null;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        var item = GetMatchingItem(eventData.hovered[0].transform.parent.transform);
        
        if (item != null && item.InventorySlot.ItemData.Sprite != null)
        {
            m_hoverItem.UpdateInfo(item.InventorySlot.ItemData.Name,item.InventorySlot.ItemData.Description);
            m_hoverItem.SetVisibility(true);
        }
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        var position = eventData.position + new Vector2(m_offsetDistance, 0); 
        m_hoverItem.UpdatePosition(position);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        m_hoverItem.SetVisibility(false);
    }

    private InventorySlotUI GetMatchingItem(Transform transform)
    {
        return slotsUI.Find(obj => obj.transform == transform);
    }
}

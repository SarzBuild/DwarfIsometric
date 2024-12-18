using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NPCInteractor :  Interactor
{
    [SerializeField] private ItemData m_wantedItem = null;
    [SerializeField] private QuestGiver m_giver = null;
    
    private void Update()
    {
        CheckInteractionCollision();
    }
    
    protected override void CheckInteractionCollision()
    {
        var cols = Physics.OverlapSphere(transform.position, m_interactionRange, m_interactableLayer);
        
        if (cols.Length >= 1 && m_giver.QuestData.Status == EQuestStatus.ACCEPTED)
        {
            for (var i = 0; i < cols.Length; i++)
            {
                var item = cols[i].GetComponent<Interactable>();
                if (item.IsInteractable && item.ItemData == m_wantedItem)
                {
                    Destroy(cols[i].gameObject);
                    m_giver.QuestData.NextStatus();
                }
            }
        }
    }
}

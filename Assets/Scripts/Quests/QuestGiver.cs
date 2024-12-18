using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    [field: SerializeField] public QuestData QuestData { get; private set; } = null;

    private QuestPanel m_panel = default;
    [SerializeField] private GameObject m_panelGameobject = null;
    [SerializeField] private float m_interactionDistance = 0.0f;
    [SerializeField] private LayerMask m_playerLayerMask = default;
    [SerializeField] private List<QuestDialogueData> m_dialogueLines = new();
    [SerializeField] private Vector3 m_offset = Vector3.zero;
    
    private void Start()
    {
        m_panel = new QuestPanel(m_panelGameobject);
        m_panel.GetButton().onClick.AddListener(QuestData.NextStatus);
        
        QuestData.ResetStatus();
    }

    private void Update()
    {
        var cols = Physics.OverlapSphere(transform.position, m_interactionDistance, m_playerLayerMask);

        if (cols.Any())
        {
            switch (QuestData.Status)
            {
                case EQuestStatus.UNOBTAINED:
                    m_panel.UpdateInfo(m_dialogueLines[0].Text, true, "ACCEPT");                 
                    break;
                case EQuestStatus.ACCEPTED:
                    m_panel.UpdateInfo(m_dialogueLines[1].Text);
                    break;
                case EQuestStatus.COMPLETED:
                    m_panel.UpdateInfo(m_dialogueLines[2].Text);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            m_panel.UpdatePosition(transform.position + m_offset);
            m_panel.UpdateRotation(Camera.main.transform.rotation);
            m_panel.SetVisibility(true);
            
        }
        else
        {
            m_panel.SetVisibility(false);
        }
    }
}

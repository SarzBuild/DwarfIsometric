using System.Linq;
using UnityEngine;

public abstract class Interactor : MonoBehaviour
{
    [SerializeField] protected float m_interactionRange = 2.0f;
    [SerializeField] protected LayerMask m_interactableLayer = default;

    protected Interactable m_currentInteractable = null;
    
    protected virtual void CheckInteractionCollision()
    {
        var cols = Physics.OverlapSphere(transform.position, m_interactionRange, m_interactableLayer);
        Collider[] sortedCols; 
        
        if (cols.Length > 1)
        {
            sortedCols = cols.OrderBy(c => (transform.position - c.transform.position).sqrMagnitude).ToArray();
        }
        else
        {
            sortedCols = cols;
        }

        if (sortedCols.Any())
        {
            Debug.Log(sortedCols[0].transform.name);
            
            var interactable = sortedCols[0].GetComponent<Interactable>();

            if (interactable.IsInteractable)
            {
                m_currentInteractable = interactable;
            }
        }
        else
        {
            m_currentInteractable = null;
        }
    }
    
    #if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        
        Gizmos.DrawSphere(transform.position, m_interactionRange);
    }
    #endif
}

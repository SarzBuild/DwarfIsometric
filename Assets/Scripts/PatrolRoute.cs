using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PatrolRoute : MonoBehaviour
{
    public enum EPatrolType
    {
        None,
        Random,
        PingPong
    }

    [field: SerializeField] public List<GameObject> Positions { get; private set; } = new();
    [field: SerializeField] public List<string> PositionsNames { get; private set; } = new();
    
    public EPatrolType patrolType = EPatrolType.None;
    public bool infiniteLoop = false;
    public int loopCount = 1;

    public UnityAction OnEditorStateChange;

    private GameObject m_positionContainer = null;
    
    private void Reset()
    {
        m_positionContainer = new GameObject("Position Container of " + name);
    }

    public void AddPosition()
    {
        if (m_positionContainer == null) Reset();
        
        PositionsNames.Add("newName");
        var go = new GameObject("newPosition");
        Positions.Add(go);
        go.transform.SetParent(m_positionContainer.transform);
        go.AddComponent<PickableGizmo>();
    }

    public void RemovePosition()
    {
        RemoveObjectAtIndex(PositionsNames.Count - 1);
    }

    public void ClearPositions()
    {
        for (int i = Positions.Count; i > 0; i--)
        {
            RemoveObjectAtIndex(i - 1);
        }
    }

    private void RemoveObjectAtIndex(int index)
    {
        PositionsNames.Remove(PositionsNames[index]);
        DestroyImmediate(Positions[index]);
        Positions.Remove(Positions[index]);
    }
}

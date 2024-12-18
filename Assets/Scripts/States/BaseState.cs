using UnityEngine;

public abstract class BaseState
{
    protected NPCBrain m_brain;

    public BaseState(NPCBrain brain)
    {
        m_brain = brain;
    }

    public void OnEnter()
    {
        SetNextDestination();
    }

    public void LogicUpdate()
    {
        if(m_brain.Route.Positions.Count <= 0) return;
        
        if (HasArrivedAtDestination())
        {
            Debug.Log("Arrived at destination");
            SetNextDestination();
        }
    }

    private bool HasArrivedAtDestination() =>
        Vector3.Distance(m_brain.transform.position, m_brain.Agent.destination) <= m_brain.Agent.stoppingDistance;

    protected abstract void SetNextDestination();
}
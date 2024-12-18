using UnityEngine;

public class RandomState : BaseState
{
    public RandomState(NPCBrain brain) : base(brain) { }

    protected override void SetNextDestination()
    {
        var randomDestination = Random.Range(0, m_brain.Route.Positions.Count);
        m_brain.Agent.SetDestination(m_brain.Route.Positions[randomDestination].transform.position);
    }
}

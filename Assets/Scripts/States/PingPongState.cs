public class PingPongState : BaseState
{
    private int m_direction = 0;
    private int m_currentIndex = 0;
    
    public PingPongState(NPCBrain brain) : base(brain)
    {
        m_direction = 1;
    }

    protected override void SetNextDestination()
    {
        m_brain.Agent.SetDestination(m_brain.Route.Positions[m_currentIndex].transform.position);
        
        m_currentIndex += m_direction;
        
        if (m_currentIndex >= m_brain.Route.Positions.Count || m_currentIndex < 0)
        {
            m_direction *= -1;
            m_currentIndex += m_direction;
        }
    }
}

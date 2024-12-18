using UnityEngine;

public class NormalState : BaseState
{
    private bool m_isInfinite = false;
    private int m_currentIndex = 0;
    private int m_loopCount = 0;
    
    public NormalState(NPCBrain brain, bool isInfinite) : base(brain)
    {
        m_isInfinite = isInfinite;
        m_currentIndex = 0;
        m_loopCount = 0;
    }

    protected override void SetNextDestination()
    {
        if (m_isInfinite || m_loopCount < m_brain.Route.loopCount)
        {
            if (!m_isInfinite && m_currentIndex == m_brain.Route.Positions.Count) m_loopCount++;
            
            ResetIndex();

            m_brain.Agent.SetDestination(m_brain.Route.Positions[m_currentIndex].transform.position);

            m_currentIndex++;
        }
    }

    private void ResetIndex()
    {
        if (m_currentIndex > m_brain.Route.Positions.Count - 1)
        {
            m_currentIndex = 0;
        }
    }
}

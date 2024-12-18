using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Behavior Tree/Leaf Node/ResumePatrol", fileName = "newLeaf")]
public class ResumePatrolBehavoirLeaf : LeafEvaluationGeneric
{
    public override ENodeState Evaluate(Object caller, Node currentNode)
    {
        NPCBrain callerObj = caller as NPCBrain;

        callerObj.UpdateState();

        return ENodeState.SUCCESS; 
    }
}

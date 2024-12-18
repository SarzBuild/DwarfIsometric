using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Behavior Tree/Leaf Node/Stop Look At", fileName = "newLeaf")]
public class StopLookAtLeaf : LeafEvaluationGeneric
{
    public override ENodeState Evaluate(Object caller, Node currentNode)
    {
        NPCBrain callerObj = caller as NPCBrain;

        callerObj.Agent.SetDestination(callerObj.transform.position);
        
        callerObj.transform.LookAt(new Vector3(callerObj.Target.transform.position.x, callerObj.transform.position.y, callerObj.Target.transform.position.z));

        return ENodeState.SUCCESS;
    }
}
    
    

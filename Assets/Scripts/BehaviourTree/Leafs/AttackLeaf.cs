using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Behavior Tree/Leaf Node/Attack", fileName = "newLeaf")]
public class AttackLeaf : LeafEvaluationGeneric
{
    public override ENodeState Evaluate(Object caller, Node currentNode)
    {
        NPCBrain callerObj = caller as NPCBrain;

        callerObj.Agent.SetDestination(callerObj.Target.transform.position);

        if (callerObj.Agent.remainingDistance < 2)
        {
            callerObj.transform.LookAt(new Vector3(callerObj.Target.transform.position.x, callerObj.transform.position.y, callerObj.Target.transform.position.z));
            Debug.Log("Leaf: Attack Success!");
            return ENodeState.SUCCESS;
        }

        if (callerObj.Agent.hasPath)
        {
            Debug.Log("Leaf: attack Running");
            return ENodeState.RUNNING;
        }

        return ENodeState.FAILURE;
    }
}
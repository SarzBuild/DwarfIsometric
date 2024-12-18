using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Behavior Tree/Leaf Node/Enemy Detection", fileName = "newLeaf")]
public class EnemyDetectionLeaf : LeafEvaluationGeneric
{
    public override ENodeState Evaluate(Object caller, Node currentNode)
    {
        NPCBrain callerObj = caller as NPCBrain;

        var cols = Physics.OverlapSphere(callerObj.transform.position, callerObj.DetectionDistance,
            callerObj.PlayerLayerMask);
        
        if(cols.Any())
        {
            callerObj.Target = cols[0].transform.gameObject;
            return ENodeState.SUCCESS;
        }

        callerObj.Target = null;
        return ENodeState.FAILURE;
    }
}

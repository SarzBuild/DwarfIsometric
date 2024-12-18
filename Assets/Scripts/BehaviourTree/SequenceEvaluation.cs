using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Behavior Tree/Evaluation Node/Sequence", fileName = "newSequenceNode")]
public class SequenceEvaluation : EvaluationMethodGeneric
{
    public override ENodeState Evaluate(Object caller, Node currentNode)
    {
        Debug.Log("Sequence of: " + name + " started.");

        if (currentNode.Children.Any())
        {
            foreach (var child in currentNode.Children)
            {
                var state = child.Evaluate(caller, child);
                switch (state)
                {
                    case ENodeState.RUNNING:
                        Debug.Log(child.name + " Running.");
                        return state;
                    case ENodeState.FAILURE:
                        Debug.Log(child.name + " Failure.");
                        return state;
                    case ENodeState.SUCCESS:
                        Debug.Log(child.name + " Success.");
                        break;
                }
            }
            
            return ENodeState.SUCCESS;
        }
        
        Debug.LogError("Sequence has no children");
        return ENodeState.FAILURE;
    }
}

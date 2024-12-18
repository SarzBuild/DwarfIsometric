using System.Linq;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

[CreateAssetMenu(menuName = "Behavior Tree/Evaluation Node/Selector", fileName = "newSelectorNode")]
public class SelectorEvaluation : EvaluationMethodGeneric
{
    public override ENodeState Evaluate(Object caller, Node currentNode)
    {
        Debug.Log("Selector of: " + name + " started.");

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
                        break;
                    case ENodeState.SUCCESS:
                        Debug.Log(child.name + " Success.");
                        return state;
                }
            }
            
            Debug.LogError("No child was successful in selector " + name);
            return ENodeState.FAILURE;
        }
        
        Debug.LogError("Selector has no children");
        return ENodeState.FAILURE;
    }
}

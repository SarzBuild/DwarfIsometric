using UnityEngine;

public abstract class EvaluationMethodGeneric : ScriptableObject
{
    public abstract ENodeState Evaluate(Object caller, Node currentNode);
}

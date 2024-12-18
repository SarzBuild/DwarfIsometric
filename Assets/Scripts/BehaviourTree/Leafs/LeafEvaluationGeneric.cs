using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafEvaluationGeneric : EvaluationMethodGeneric
{
    public override ENodeState Evaluate(Object caller, Node currentNode)
    {
        return ENodeState.FAILURE;
    }
}

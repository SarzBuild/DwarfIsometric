using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

public enum ENodeState
{
    RUNNING,
    FAILURE,
    SUCCESS
}

public enum ENodeType
{
    ROOT,
    SEQUENCER,
    SELECTOR,
    DECORATOR,
    LEAF
}

[CreateAssetMenu(menuName = "Behavior Tree/Node", fileName = "newNode")]
public class Node : ScriptableObject
{
    public Func<Object, Node, ENodeState> Evaluate;
    
    public ENodeType NodeType;
    public ENodeState State;
    public List<Node> Children = new();
    public EvaluationMethodGeneric EvaluationNode;

    private void OnEnable()
    {
        if (Evaluate == null && EvaluationNode != null)
        {
            Evaluate = EvaluationNode.Evaluate;
        }
    }

    private void OnValidate()
    {
        if (NodeType == ENodeType.DECORATOR && Children.Count > 1)
        {
            Debug.Log("Decorator can only have 1 child.");
            var child = Children[0];
            Children = new List<Node>() { child };
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Node))]
public class NodeEditor : Editor
{
    private SerializedProperty m_children;
    private void OnEnable()
    {
        m_children = serializedObject.FindProperty("Children");
    }

    public override void OnInspectorGUI()
    {
        Node node = target as Node;
        if (node.NodeType == ENodeType.LEAF)
        {
            DrawPropertiesExcluding(serializedObject, "Children");
        }
        else
        {
            base.OnInspectorGUI();
        }

        serializedObject.ApplyModifiedProperties();
    }
}

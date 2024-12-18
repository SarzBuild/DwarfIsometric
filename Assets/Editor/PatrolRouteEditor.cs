using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PatrolRoute), editorForChildClasses: true)]
public class PatrolRouteEditor : Editor
{
    private PatrolRoute m_patrolRoute;

    private void OnEnable()
    {
        m_patrolRoute = target as PatrolRoute;
    }

    public override void OnInspectorGUI()
    {
        Rect r = EditorGUILayout.GetControlRect(GUILayout.Height(EditorGUIUtility.singleLineHeight));
        GUI.Box(r, "AI Properties:");

        EditorGUI.indentLevel++;
        
        EditorGUI.BeginChangeCheck();
        var toggle = EditorGUILayout.Toggle("Is Infinite Loop:", m_patrolRoute.infiniteLoop);
        
        if(EditorGUI.EndChangeCheck())
        {
            m_patrolRoute.infiniteLoop = toggle;
        }
        
        if (m_patrolRoute.infiniteLoop)
        {
            m_patrolRoute.loopCount = 1;
            
            EditorGUI.BeginChangeCheck();
            var enumPopup = (PatrolRoute.EPatrolType)EditorGUILayout.EnumPopup("Patrol type:", m_patrolRoute.patrolType);
        
            if(EditorGUI.EndChangeCheck())
            {
                m_patrolRoute.patrolType = enumPopup;
                m_patrolRoute.OnEditorStateChange?.Invoke();
            }
        }
        else
        {
            m_patrolRoute.patrolType = PatrolRoute.EPatrolType.None;
            
            EditorGUI.BeginChangeCheck();
            var count = EditorGUILayout.IntField("Loop Count:", m_patrolRoute.loopCount);

            if (EditorGUI.EndChangeCheck())
            {
                m_patrolRoute.loopCount = count;
                m_patrolRoute.OnEditorStateChange?.Invoke();
            }
        }
        EditorGUI.indentLevel--;
        
        GUILayout.Space(EditorGUIUtility.singleLineHeight);
        
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add new position"))
        {
            m_patrolRoute.AddPosition();
        }

        GUI.enabled = m_patrolRoute.Positions.Count >= 1;
        if (GUILayout.Button("Remove last position"))
        {
            m_patrolRoute.RemovePosition();
        }

        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Clear all positions"))
        {
            m_patrolRoute.ClearPositions();
        }

        GUI.enabled = true;

        Divider(1, Color.blue);
        
        foreach (var go in m_patrolRoute.Positions)
        {
            var index = m_patrolRoute.Positions.FindIndex(x => x == go);

            EditorGUILayout.BeginHorizontal();

            EditorGUI.BeginChangeCheck();
            var t = EditorGUILayout.TextField(m_patrolRoute.PositionsNames[index]);

            if (EditorGUI.EndChangeCheck())
            {
                m_patrolRoute.PositionsNames[index] = t;
                go.name = t;
            }

            EditorGUI.BeginChangeCheck();
            var v = EditorGUILayout.Vector3Field("", go.transform.position);

            if (EditorGUI.EndChangeCheck())
            {
                go.transform.position = v;
            }

            if (GUILayout.Button("Bring To Viewport"))
            {
                var view = SceneView.lastActiveSceneView;
                go.transform.position = view.camera.transform.position;
            }

            if (GUILayout.Button("Goto"))
            {
                var view = SceneView.lastActiveSceneView;

                view.pivot = go.transform.position;
                view.size = 10;
                view.Repaint();
            }

            EditorGUILayout.EndHorizontal();
        }


        if (m_patrolRoute.PositionsNames.Count >= 1)
        {
            Divider(1, Color.blue); 
        }
        

        serializedObject.ApplyModifiedProperties();
    }
    
    private void Divider(int height, Color color)
    {
        Rect r = EditorGUILayout.GetControlRect(GUILayout.Height(height));
        EditorGUI.DrawRect(r, color);
    }
}
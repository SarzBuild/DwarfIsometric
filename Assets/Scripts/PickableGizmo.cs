#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using UnityEngine.AI;

public class PickableGizmo : MonoBehaviour
    {
        [SerializeField] private Vector3 m_offset = Vector3.up * 10.0f;

        private void OnDrawGizmos()
        {
            NavMeshHit hit;

            NavMesh.SamplePosition(transform.position, out hit, 0.5f, NavMesh.AllAreas);
            
            Gizmos.color = Mathf.Abs(transform.position.y - hit.position.y) < 0.5f ? Color.green : Color.red;
            Handles.color = Gizmos.color;

            Handles.DrawWireDisc(transform.position, Vector3.up, 1.0f);

            var gizmoPosition = transform.position + m_offset;
            Gizmos.DrawSphere(gizmoPosition, 0.5f);
            Gizmos.DrawLine(gizmoPosition, transform.position);
        }
    }

#endif
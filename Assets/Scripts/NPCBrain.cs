using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(PatrolRoute), typeof(NavMeshAgent))] 
public class NPCBrain : MonoBehaviour
{
    public NavMeshAgent Agent { get; private set; } = null;
    public PatrolRoute Route { get; private set; } = null;
    public GameObject Target { get; set; } = null;

    [field: SerializeField] public float DetectionDistance { get; set; }
    [field: SerializeField] public LayerMask PlayerLayerMask { get; set; }
    [field: SerializeField] public Node BehavourTree { get; set; }
    
    private BaseState m_currentState = null;
    
    private void Awake()
    {
        Agent ??= GetComponent<NavMeshAgent>();
        Route ??= GetComponent<PatrolRoute>();
    }

    private void OnEnable()
    {
        Route.OnEditorStateChange += SetState;
    }

    private void OnDisable()
    {
        Route.OnEditorStateChange -= SetState;
    }

    private void Start()
    {
        SetState();

        StartCoroutine(Tick());
    }

    private void SetState()
    {
        m_currentState = Route.patrolType switch
        {
            PatrolRoute.EPatrolType.None => new NormalState(this, Route.infiniteLoop),
            PatrolRoute.EPatrolType.Random => new RandomState(this),
            PatrolRoute.EPatrolType.PingPong => new PingPongState(this),
            _ => throw new ArgumentOutOfRangeException()
        };
        
        m_currentState.OnEnter();
    }

    private IEnumerator Tick()
    {
        while (true)
        {
            BehavourTree.Evaluate(this, BehavourTree);
            yield return new WaitForSeconds(0.2f);
        }
    }

    public void UpdateState()
    {
        m_currentState.LogicUpdate();
    }
}

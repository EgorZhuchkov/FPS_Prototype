using System;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
  public class EnemyFollowPlayer : MonoBehaviour
  {
    public NavMeshAgent agent;
    public float minDistance = 1.0f;

    private const string PlayerTag = "Player";
    private Transform _targetTransform;

    private void Awake()
    {
      _targetTransform = GameObject.FindWithTag(PlayerTag).transform;
      agent.stoppingDistance = minDistance;
    }

    private void Update() =>
      SetDestinationForAgent();

    private void SetDestinationForAgent()
    {
      if(TargetNotReached())
        agent.destination = _targetTransform.position;
    }

    private bool TargetNotReached() => 
      Vector3.Distance(agent.transform.position, _targetTransform.position) >= minDistance;

    private void OnDisable() => 
      agent.enabled = false;

    private void OnEnable() => 
      agent.enabled = true;

    private void Reset() => 
      agent = GetComponent<NavMeshAgent>();
  }
}
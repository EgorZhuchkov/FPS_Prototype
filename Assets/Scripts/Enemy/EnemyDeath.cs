using System;
using UnityEngine;

namespace Enemy
{
  [RequireComponent(typeof(EnemyHealth))]
  public class EnemyDeath : MonoBehaviour
  {
    public EnemyHealth health;
    public EnemyFollowPlayer follow;
    public float bodyDestructionDelay = 1.0f;
    
    public event Action OnDeath;

    private void Start() => 
      health.HealthChanged += OnHealthChanged;

    private void OnDestroy()
    {
      health.HealthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged()
    {
      if (health.Current <= 0.0f) 
        Die();
    }

    private void Die()
    {
      health.HealthChanged -= OnHealthChanged;
      follow.enabled = false;
      Destroy(gameObject, bodyDestructionDelay);
      
      //Animations and other staff
      
      OnDeath?.Invoke();
    }

    private void Reset()
    {
      health = GetComponent<EnemyHealth>();
      follow = GetComponent<EnemyFollowPlayer>();
    }
  }
}
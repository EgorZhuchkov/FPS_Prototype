using System;
using UnityEngine;

namespace Enemy
{
  public class EnemyHealth : MonoBehaviour
  {
    public float maximum = 100.0f;

    public float Current { get; private set; }
    
    public event Action HealthChanged;

    private void Awake()
    {
      Current = maximum;
    }

    public void TakeDamage(float amount)
    {
      if (Current <= 0.0f) 
        return;

      Current -= amount;
      HealthChanged?.Invoke();
    }
  }
}
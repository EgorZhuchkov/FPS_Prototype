using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

namespace Enemy
{
  [RequireComponent(typeof(MeshRenderer))]
  [RequireComponent(typeof(EnemyHealth))]
  public class EnemyDeath : MonoBehaviour
  {
    public EnemyHealth health;
    public EnemyFollowPlayer follow;
    public float bodyDestructionDelay = 1.0f;

    private MeshRenderer _renderer;
    private static readonly int DissolveAmount = Shader.PropertyToID("_Amount");

    private void Awake() =>
      _renderer = GetComponent<MeshRenderer>();

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
      
      _renderer.shadowCastingMode = ShadowCastingMode.Off;
      StartCoroutine(DissolveBody(bodyDestructionDelay));
      Destroy(gameObject, bodyDestructionDelay);
      

      OnDeath?.Invoke();
    }

    private IEnumerator DissolveBody(float duration)
    {
      float progress = 0.0f;
      while (progress <= duration)
      {
        progress += Time.deltaTime;
        _renderer.material.SetFloat(DissolveAmount, Mathf.Clamp01(progress / duration));
        yield return null;
      }
    }

    private void Reset()
    {
      health = GetComponent<EnemyHealth>();
      follow = GetComponent<EnemyFollowPlayer>();
    }
  }
}
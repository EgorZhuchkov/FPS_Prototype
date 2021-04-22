using UnityEngine;

namespace Enemy
{
  public class EnemySpawner : MonoBehaviour
  {
    public GameObject enemyPrefab;
    private GameObject _currentEnemy;
    
    private void Awake() => 
      Spawn();

    private void Spawn()
    {
      if(_currentEnemy)
        _currentEnemy.GetComponent<EnemyDeath>().OnDeath -= OnEnemyDeath;
      
      _currentEnemy = Instantiate(enemyPrefab, transform);
      _currentEnemy.GetComponent<EnemyDeath>().OnDeath += OnEnemyDeath;
    }

    private void OnEnemyDeath() => 
      Spawn();

    private void OnDestroy() => 
      _currentEnemy.GetComponent<EnemyDeath>().OnDeath -= OnEnemyDeath;
  }
}
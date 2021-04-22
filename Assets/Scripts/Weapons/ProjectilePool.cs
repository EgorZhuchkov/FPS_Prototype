using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
  public class ProjectilePool : MonoBehaviour
  {
    public GameObject projectilePrefab;
    public int size;

    private readonly Queue<WeaponProjectile> _pool = new Queue<WeaponProjectile>();

    private void Awake()
    {
      for (var i = 0; i < size; i++)
      {
        GameObject projectile = Instantiate(projectilePrefab, transform);
        _pool.Enqueue(projectile.GetComponent<WeaponProjectile>());
        projectile.SetActive(false);
      }
    }

    public WeaponProjectile PopProjectile()
    {
      WeaponProjectile projectile = _pool.Dequeue();
      _pool.Enqueue(projectile);
      
      projectile.ResetProjectile();
      projectile.gameObject.SetActive(true);
      
      return projectile;
    } 
  }
}
using UnityEngine;

namespace Weapons.WeaponActions
{
  public class FireProjectileAction : WeaponAction
  {
    public Transform muzzle;
    public Camera fpsCamera;
    public AudioSource shotAudio;
    public WeaponAnimator weaponAnimator;
    public ProjectilePool projectiles;
    public float range = 100.0f;
    public float shotCooldown = 3.0f;
    public float projectileSpeed = 1000.0f;
    public float damage = 10.0f;

    private float _lastShotTime = float.MinValue;
    private bool OnCooldown => Time.time - _lastShotTime < shotCooldown;

    public override void Perform()
    {
      if (!OnCooldown)
        Shoot();
    }

    private void Shoot()
    {
      ShootProjectile();
      shotAudio.Play();
      weaponAnimator.PlayShoot();
      
      _lastShotTime = Time.time;
    }

    private void ShootProjectile()
    {
      WeaponProjectile projectile = projectiles.PopProjectile();
      projectile.Shoot(
        muzzle.position, 
        fpsCamera.transform.forward * range, 
        damage, 
        projectileSpeed);
    }

    public override void Cancel()
    {
    }

    private void Reset()
    {
      fpsCamera = GetComponentInParent<Camera>();
      weaponAnimator = GetComponent<WeaponAnimator>();
      shotAudio = GetComponent<AudioSource>();
    }
  }
}
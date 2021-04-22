using UnityEngine;

namespace Weapons.WeaponActions
{
  public class FireProjectileAction : WeaponAction
  {
    public Transform muzzle;
    public Camera fpsCamera;
    public AudioSource shotAudio;
    public WeaponAnimator weaponAnimator;
    public GameObject projectilePrefab;
    public float range = 100.0f;
    public float shotCooldown = 3.0f;
    public float projectileSpeed;

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
      Vector3 muzzlePosition = muzzle.position;
      GameObject projectile = Instantiate(projectilePrefab, muzzlePosition, Quaternion.identity);
      projectile.transform.position = muzzlePosition;
      projectile.transform.LookAt(fpsCamera.transform.forward * range);
      projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * projectileSpeed);
    }

    public override void Cancel() { }

    private void Reset()
    {
      fpsCamera = GetComponentInParent<Camera>();
      weaponAnimator = GetComponent<WeaponAnimator>();
      shotAudio = GetComponent<AudioSource>();
    }
  }
}
using System.Collections;
using Animations;
using UnityEngine;

namespace Weapons.WeaponActions
{
  [RequireComponent(typeof(WeaponAnimator))]
  public class FireBeamAction : WeaponAction
  {
    public Camera fpsCamera;
    public Transform muzzle;
    public WeaponBeam beam;
    public AudioSource shotAudio;
    public WeaponAnimator weaponAnimator;
    public float range = 100.0f;
    public float chargeDuration = 1.0f;
    public float shotCooldown = 3.0f;
    
    private float _lastShotTime = float.MinValue;
    private Coroutine _chargeCoroutine;
    private bool OnCooldown => Time.time - _lastShotTime < shotCooldown;

    private void Reset()
    {
      fpsCamera = GetComponentInParent<Camera>();
      beam = GetComponentInChildren<WeaponBeam>();
      weaponAnimator = GetComponent<WeaponAnimator>();
      shotAudio = GetComponent<AudioSource>();
    }

    public override void Perform()
    {
      if (!OnCooldown) 
        ChargeBeam();
    }

    public override void Cancel() => 
      CancelCharge();

    private void ChargeBeam()
    {
      _lastShotTime = Time.time;
      beam.ChargeBeam(muzzle.transform.position);
      weaponAnimator.BeginCharge();
      _chargeCoroutine = StartCoroutine(ShootAfterDelay(chargeDuration));
    }

    private void CancelCharge()
    {
      if (weaponAnimator.State != AnimatorState.Charge) 
        return;
      
      weaponAnimator.CancelCharge();
      beam.CancelCharge();
      _lastShotTime = float.MinValue;
      if (_chargeCoroutine != null)
        StopCoroutine(_chargeCoroutine);
    }

    private IEnumerator ShootAfterDelay(float delay)
    {
      yield return new WaitForSeconds(delay);
      Shoot();
    }

    private void Shoot()
    {
      weaponAnimator.PlayShoot();
      weaponAnimator.CancelCharge();
      
      if (IsHitting(out RaycastHit hit))
        beam.Fire(muzzle.transform.position, hit.point);
      else
        beam.Fire(muzzle.transform.position, fpsCamera.transform.forward * range);

      shotAudio.Play();
    }

    private bool IsHitting(out RaycastHit hit)
    {
      Transform cameraTransform = fpsCamera.transform;
      return Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, range);
    }
  }
}
using System.Collections;
using UnityEngine;

namespace Weapons.WeaponActions
{
  public class FireBeamAction : WeaponAction
  {
    public Camera fpsCamera;
    public Transform muzzle;
    public WeaponBeam beam;
    public AudioSource shotAudio;
    public Animator weaponAnimator;
    public float range = 100.0f;
    public float shotCooldown = 3.0f;

    private static readonly int Shot = Animator.StringToHash("Shot");
    private float _lastShotTime = float.MinValue;
    private bool OnCooldown => Time.time - _lastShotTime < shotCooldown;
    
    public override void Perform()
    {
      if (OnCooldown)
        return;

      StartCoroutine(Fire());
    }

    private IEnumerator Fire()
    {
      _lastShotTime = Time.time;
      beam.ChargeBeam(muzzle.transform.position);
      weaponAnimator.SetTrigger(Shot);
      yield return new WaitForSeconds(1.6f);
      
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

    private void Reset() => 
      fpsCamera = GetComponentInParent<Camera>();
  }
}
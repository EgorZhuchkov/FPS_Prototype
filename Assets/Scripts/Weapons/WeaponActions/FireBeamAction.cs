using UnityEngine;

namespace Weapons.WeaponActions
{
  public class FireBeamAction : WeaponAction
  {
    public Camera fpsCamera;
    public Transform muzzle;
    public WeaponBeam beam;
    public AudioSource shotAudio;
    public float range = 100.0f;
    public float shotCooldown = 3.0f;

    private float _lastShotTime = float.MinValue;
    private bool OnCooldown => Time.time - _lastShotTime < shotCooldown;
    
    public override void Perform() => 
      Fire();

    private void Fire()
    {
      if (OnCooldown)
        return;
      
      if (Hit(out RaycastHit hit))
        beam.Fire(muzzle.transform.position, hit.point);
      else
        beam.Fire(muzzle.transform.position, fpsCamera.transform.forward * range);
      
      shotAudio.Play();

      _lastShotTime = Time.time;
    }

    private bool Hit(out RaycastHit hit)
    {
      Transform cameraTransform = fpsCamera.transform;
      return Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, range);
    }

    private void Reset() => 
      fpsCamera = GetComponentInParent<Camera>();
  }
}
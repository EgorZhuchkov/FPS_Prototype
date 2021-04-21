using UnityEngine;

namespace Weapons.WeaponActions
{
  public class FireBeamAction : WeaponAction
  {
    public Camera fpsCamera;
    public Transform muzzle;
    public float range = 100.0f;
    public float shotDelay = 3.0f;

    public override void Perform()
    {
      Fire();
    }

    private void Fire()
    {
      Transform cameraTransform = fpsCamera.transform;
      if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out RaycastHit hit, range))
      {
        
      }
    }

    private void Reset() => 
      fpsCamera = GetComponentInParent<Camera>();
  }
}
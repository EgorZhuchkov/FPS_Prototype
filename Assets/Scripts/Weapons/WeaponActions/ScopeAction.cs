using System.Collections;
using UnityEngine;
using VFX.Scripts;
using Weapons.General;

namespace Weapons.WeaponActions
{
  public class ScopeAction : WeaponAction
  {
    private const string WeaponLayer = "Weapon";
    
    public WeaponAnimator weaponAnimator;
    public Camera weaponCamera;
    public Camera mainCamera;
    public GameObject scopedOverlay;
    public float scopedFov = 15.0f;
    
    private float _defaultFov;
    private Coroutine _scopeRoutine;
    private NightVisionImageEffect _nightVision;

    private void Awake()
    {
      _defaultFov = mainCamera.fieldOfView;
      _nightVision = mainCamera.GetComponent<NightVisionImageEffect>();
    }

    public override void Perform() => 
      _scopeRoutine = StartCoroutine(Aim());

    public override void Cancel() => 
      CancelAim();

    private void Reset()
    {
      weaponAnimator = GetComponent<WeaponAnimator>();
      mainCamera = Camera.main;
    }

    private IEnumerator Aim()
    {
      weaponAnimator.Aim();
      yield return new WaitForSeconds(.15f);
      if (_nightVision)
        _nightVision.enabled = true;
      
      weaponCamera.cullingMask &=  ~(1 << LayerMask.NameToLayer(WeaponLayer));
      scopedOverlay.SetActive(true);
      weaponCamera.fieldOfView = scopedFov;
      mainCamera.fieldOfView = scopedFov;
    }

    private void CancelAim()
    {
      if(_scopeRoutine != null)
        StopCoroutine(_scopeRoutine);
      
      weaponAnimator.StopAim();
      if (_nightVision)
        _nightVision.enabled = false;
      
      weaponCamera.cullingMask |=  1 << LayerMask.NameToLayer(WeaponLayer);
      scopedOverlay.SetActive(false);
      mainCamera.fieldOfView = _defaultFov;
      weaponCamera.fieldOfView = _defaultFov;
    }
  }
}
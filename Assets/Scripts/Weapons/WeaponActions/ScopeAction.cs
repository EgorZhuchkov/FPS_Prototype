using System.Collections;
using UnityEngine;
using VFX.Scripts;

namespace Weapons.WeaponActions
{
  public class ScopeAction : WeaponAction
  {
    private const string WeaponLayer = "Weapon";
    
    public WeaponAnimator weaponAnimator;
    public Camera weaponCamera;
    public Camera mainCamera;
    public GameObject scopedOverlay;
    public GameObject crosshair;
    public float scopedFov = 15.0f;
    
    private float _defaultFov;
    private Coroutine _scopeRoutine;
    private NightVisionImageEffect _nightVision;

    private void Awake()
    {
      _defaultFov = mainCamera.fieldOfView;
      _nightVision = mainCamera.GetComponent<NightVisionImageEffect>();
    }

    public override void Perform()
    {
      if(_scopeRoutine != null)
        StopCoroutine(_scopeRoutine);
      _scopeRoutine = StartCoroutine(ChangeScopeState(true));
    }

    public override void Cancel()
    {
      if(_scopeRoutine != null)
        StopCoroutine(_scopeRoutine);
      _scopeRoutine = StartCoroutine(ChangeScopeState(false));
    }

    private IEnumerator ChangeScopeState(bool scoped)
    {
      crosshair.SetActive(!scoped);

      if (scoped)
      {
        weaponAnimator.Aim();
        yield return new WaitForSeconds(.15f);
      }
      else
      {
        weaponAnimator.StopAim();
      }

      if (_nightVision)
        _nightVision.enabled = scoped;
      
      weaponCamera.cullingMask ^= 1 << LayerMask.NameToLayer(WeaponLayer);
      scopedOverlay.SetActive(scoped);
      mainCamera.fieldOfView = scoped ? scopedFov : _defaultFov;
    }

    private void Reset()
    {
      weaponAnimator = GetComponent<WeaponAnimator>();
      mainCamera = Camera.main;
    }
  }
}
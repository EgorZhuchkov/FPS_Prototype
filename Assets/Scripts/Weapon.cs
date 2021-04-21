using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float range = 100.0f;
    public Camera fpsCamera;
    
    public Animator weaponAnimator;
    public GameObject scopedOverlay;
    public GameObject crosshair;
    public GameObject weaponCamera;
    public Camera mainCamera;
    public float scopedFov = 15.0f;
    
    private bool _scoped;
    private float _defaultFov;
    private static readonly int Scoped = Animator.StringToHash("Scoped");
    
    private void Awake()
    {
        _defaultFov = mainCamera.fieldOfView;
    }
    
    private IEnumerator ChangeScopeState(bool scoped)
    {
        weaponAnimator.SetBool(Scoped, scoped);
        crosshair.SetActive(!scoped);
        
        if (scoped)
            yield return new WaitForSeconds(.15f);
        
        weaponCamera.SetActive(!scoped);
        scopedOverlay.SetActive(scoped);
        mainCamera.fieldOfView = scoped ? scopedFov : _defaultFov;
    }

    public void PerformSecondaryAction()
    {
        _scoped = !_scoped;

        StopAllCoroutines();
        StartCoroutine(ChangeScopeState(_scoped));
    }

    public void PerformPrimaryAction()
    {
        Transform cameraTransform = fpsCamera.transform;
        if(Physics.Raycast(cameraTransform.position, cameraTransform.forward, out RaycastHit hit, range))
        {
            Debug.Log(hit.collider.name);
        }
    }

    private void Reset()
    {
        fpsCamera = GetComponentInParent<Camera>();
    }
}
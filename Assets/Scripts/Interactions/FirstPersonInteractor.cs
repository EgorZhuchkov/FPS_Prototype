using UnityEngine;
using UnityEngine.InputSystem;

namespace Interactions
{
  public class FirstPersonInteractor : MonoBehaviour
  {
    public InputAction interactAction;
    public float rayDistance = 1.0f;
    public LayerMask rayMask = ~0;
    public GameObject interactionOverlay;

    private Camera _camera;
    private IInteractable _currentInteraction;
    
    private void Awake()
    {
      _camera = Camera.main;
      interactAction.performed += Interact;
    }

    private void OnDestroy() => 
      interactAction.performed -= Interact;

    private void OnEnable() => 
      interactAction.Enable();

    private void OnDisable() => 
      interactAction.Disable();

    private void FixedUpdate()
    {
      Ray ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

      if (Physics.Raycast(ray, out RaycastHit hit, rayDistance, rayMask, QueryTriggerInteraction.Ignore))
      {
        var interactable = hit.collider.GetComponent<IInteractable>();
        _currentInteraction = interactable;

        interactionOverlay.SetActive(true);
      }
      else
      {
        interactionOverlay.SetActive(false);
        _currentInteraction = null;
      }
    }

    private void Interact(InputAction.CallbackContext obj) => 
      _currentInteraction?.Interact();
  }
}
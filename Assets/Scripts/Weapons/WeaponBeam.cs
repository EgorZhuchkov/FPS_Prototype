using System.Collections;
using UnityEngine;

namespace Weapons
{
  public class WeaponBeam : MonoBehaviour
  {
    [Header("Prefabs")]
    public GameObject beamStartPrefab;
    public GameObject beamPrefab;
    public GameObject beamEndPrefab;
    [Header("Settings")] 
    public float duration = 1.0f;
    public float beamEndOffset = 1f;
    public float textureScrollSpeed = 8f;
    public float textureLengthScale = 3;

    private GameObject _beamStart;
    private GameObject _beam;
    private GameObject _beamEnd;
    private LineRenderer _beamLine;
    private readonly Vector3[] _beamLinePositions = new Vector3[2];

    private void Awake()
    {
      InstantiateBeam();
      SetActive(false);
    }

    private void Update() => 
      AnimateBeam();

    public void Fire(Vector3 from, Vector3 at)
    {
      RecalculatePositions(from, at);
      SetActive(true);
      StartCoroutine(DisableAfterDelay(duration));
    }

    public void ChargeBeam(Vector3 at)
    {
      _beamStart.transform.position = at;
      _beamStart.SetActive(true);
    }

    public void CancelCharge() => 
      _beamStart.SetActive(false);

    private IEnumerator DisableAfterDelay(float delay)
    {
      yield return new WaitForSeconds(delay);
      SetActive(false);
    }

    private void RecalculatePositions(Vector3 startPosition, Vector3 targetPosition)
    {
      Vector3 direction = targetPosition - startPosition;
      Vector3 end = targetPosition - (direction.normalized * beamEndOffset);

      _beamLinePositions[0] = startPosition;
      _beamLinePositions[1] = end;
      _beamLine.SetPositions(_beamLinePositions);
      _beamStart.transform.position = startPosition;
      _beamEnd.transform.position = end;
      _beamStart.transform.LookAt(_beamEnd.transform.position);
      _beamEnd.transform.LookAt(_beamStart.transform.position);

      var distance = Vector3.Distance(startPosition, end);
      _beamLine.sharedMaterial.mainTextureScale = new Vector2(distance / textureLengthScale, 1);
    }

    private void InstantiateBeam()
    {
      _beamStart = Instantiate(beamStartPrefab, Vector3.zero, Quaternion.identity, transform);
      _beam = Instantiate(beamPrefab, Vector3.zero, Quaternion.identity, transform);
      _beamEnd = Instantiate(beamEndPrefab, Vector3.zero, Quaternion.identity, transform);
      _beamLine = _beam.GetComponent<LineRenderer>();
    }

    private void SetActive(bool value)
    {
      _beamStart.SetActive(value);
      _beam.SetActive(value);
      _beamEnd.SetActive(value);
    }

    private void AnimateBeam() => 
      _beamLine.sharedMaterial.mainTextureOffset -= new Vector2(Time.deltaTime * textureScrollSpeed, 0);
  }
}
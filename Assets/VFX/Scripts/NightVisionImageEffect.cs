using UnityEngine;

namespace VFX.Scripts
{
  [RequireComponent(typeof(Camera))]
  [ExecuteInEditMode]
  public class NightVisionImageEffect : MonoBehaviour
  {
    private static readonly int Luminance = Shader.PropertyToID("_Luminance");
    
    public Shader shader;
    [Range(0f, 1f)] public float luminance = 0.44f;
    
    private Material _material;
    
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
      if (shader != null)
      {
        if (!_material)
        {
          _material = new Material(shader);
        }

        _material.SetVector(Luminance, new Vector4(luminance, luminance, luminance, luminance));
        Graphics.Blit(source, destination, _material);
      }
      else
      {
        Graphics.Blit(source, destination);
      }
    }
  }
}
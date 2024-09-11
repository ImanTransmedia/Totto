using System.Diagnostics;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostExposureControl : MonoBehaviour
{
    public Volume globalVolume; // Asigna el Global Volume desde el inspector
    private ColorAdjustments colorAdjustments; // Referencia a los ajustes de color
    public float exposureValue = 0f; // Variable pública para ajustar el valor de post-exposición

    void Start()
    {
        // Intentamos obtener la configuración de Color Adjustments del Volume Profile
        if (globalVolume != null && globalVolume.profile.TryGet(out colorAdjustments))
        {
           // Debug.Log("Color Adjustments encontrado y listo para ser manipulado.");
        }
        else
        {
            // Debug.LogError("No se encontró el componente de Color Adjustments en el Volume Profile.");
        }

    }

    // Este método permite ajustar la exposición manualmente si se prefiere desde otro script
    public void SetPostExposure(float newPostExposureValue)
    {
        if (colorAdjustments != null)
        {
            colorAdjustments.postExposure.value = newPostExposureValue;
            //exposureValue = newPostExposureValue;
        }
    }
}


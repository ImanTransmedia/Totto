using UnityEngine;
using System.Collections;


public static class ControladorOculus
{

    public static void VibracionControl(float intensity)
    {
        OVRInput.Controller controller = OVRInput.GetActiveController();

        // Hace que el mando vibre con la intensidad dada
        OVRInput.SetControllerVibration(intensity, intensity, controller);
    }

    public static void PararControl()
    {
        // Detiene la vibración del mando
        OVRInput.Controller controller = OVRInput.GetActiveController();
        OVRInput.SetControllerVibration(0, 0, controller);
    }

    public static void VibracionControlHaptico(AudioClip MiAudio)
    {
        OVRHapticsClip MiClip;
   
        MiClip = new OVRHapticsClip(MiAudio);

        foreach (var item in OVRHaptics.Channels)
        {
            item.Preempt(MiClip);
        }
        //OVRHaptics.Channels[1].Preempt(MiClip);
    }

    public static void TriggerHaptics(OVRInput.Controller controller, float frequency = 1f, float amplitude = 1f, float duration = 1f, bool Vibration = true)
    {
        if (!Vibration)
        {
            return;
        }
        CoroutineRunner.Instance.RunCoroutine(TriggerHapticsRoutine(controller, frequency, amplitude, duration));
    }

    private static IEnumerator TriggerHapticsRoutine(OVRInput.Controller controller, float frequency, float Amplitude, float duration)
    {
        OVRInput.SetControllerVibration(frequency, Amplitude, controller);
        yield return new WaitForSeconds(duration);
        OVRInput.SetControllerVibration(0, 0, controller);
    }
}


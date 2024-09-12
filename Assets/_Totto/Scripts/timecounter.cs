using UnityEngine;
using UnityEngine.UI;

public class TimeCounter : MonoBehaviour
{
    public float timeLimit = 40f; // Tiempo límite en segundos
    public Text timerText; // Referencia al objeto de texto para mostrar el contador de tiempo

    private float timer = 0f; // Tiempo transcurrido

    void Start()
    {
        timer = timeLimit; // Inicializar el temporizador con el tiempo límite
    }

    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime; // Actualizar el temporizador restando el tiempo transcurrido

            // Actualizar el texto del contador de tiempo
            int secondsRemaining = Mathf.CeilToInt(timer);
            timerText.text = "Tiempo restante: " + secondsRemaining.ToString() + " s";
        }
        else
        {
            timerText.text = "Tiempo agotado"; // Mostrar un mensaje cuando se agote el tiempo
            Time.timeScale = 0f; // Detener el tiempo del juego cuando el temporizador llegue a cero
        }
    }
}

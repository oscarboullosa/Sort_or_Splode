using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Instancia única del GameManager
    public static GameManager instance;

    // Referencia al panel de game over
    [SerializeField] private TMPro.TextMeshProUGUI textoGameOver;

    // Referencia al texto de puntuación
    [SerializeField] private TMPro.TextMeshProUGUI textoPuntuacion;

    // Puntuación actual del jugador
    private int puntuacion;

    // Frecuencia de aparición de bombas (en segundos)
    public float frecuenciaBombas = 1f;

    // GameObject de la puerta de la bomba roja
    public GameObject puertaBombaRoja;

    // GameObject de la puerta de la bomba azul
    public GameObject puertaBombaAzul;

    // Prefab de la bomba roja
    public GameObject prefabBombaRoja;

    // Prefab de la bomba azul
    public GameObject prefabBombaAzul;
    private bool playing = false;

    private void Awake()
    {
        // Establecemos la instancia única del GameManager
        instance = this;
    }

    private void Start()
    {
        // Iniciamos el juego generando bombas cada frecuenciaBombas segundos
        InvokeRepeating("GenerarBomba", 0f, frecuenciaBombas);
        playing = true;
    }

    // Método que genera una bomba roja o azul aleatoriamente
    private void GenerarBomba()
    {
        if (playing)
        {
            // Generamos un número aleatorio entre 0 y 1
            float aleatorio = Random.Range(0f, 1f);

            // Si el número aleatorio es menor que 0.5, generamos una bomba roja
            if (aleatorio < 0.5f)
            {
                // Creamos una bomba roja a partir del prefab
                GameObject bombaRoja = Instantiate(prefabBombaRoja, puertaBombaRoja.transform.position, Quaternion.identity);

                // Establecemos la etiqueta de la bomba roja
                bombaRoja.tag = "BombaRoja";
            }
            // Si no, generamos una bomba azul
            else
            {
                // Creamos una bomba azul a partir del prefab
                GameObject bombaAzul = Instantiate(prefabBombaAzul, puertaBombaAzul.transform.position, Quaternion.identity);

                // Establecemos la etiqueta de la bomba azul
                bombaAzul.tag = "BombaAzul";
            }
        }
    }

    // Método que aumenta la puntuación actual
    public void AumentarPuntuacion(int amount)
    {
        // Aumentamos la puntuación actual
        puntuacion += amount;

        // Actualizamos el texto de puntuación
        textoPuntuacion.text = puntuacion.ToString();
    }

    // Método que muestra el panel de game over y detiene el juego
    public void GameOver()
    {
        // Activamos el panel de game over
        textoGameOver.text = "Game Over";
        // Detenemos el juego
        Time.timeScale = 0f;
        playing = false;
    }
    /*public void GameOver(int type)
    {
        if (type == 0)
        {
            OOT.SetActive(true);
        }
        else
        {
            DKH.SetActive(true);
        }
        foreach (Yoshi yoshi in yoshis)
        {
            yoshi.StopGame();
        }
        playing = false;
        startButton.SetActive(true);
    }*/
}

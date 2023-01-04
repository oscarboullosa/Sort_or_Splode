using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Instancia �nica del GameManager
    public static GameManager instance;

    // Referencia al panel de game over
    [SerializeField] private TMPro.TextMeshProUGUI textoGameOver;

    // Referencia al texto de puntuaci�n
    [SerializeField] private TMPro.TextMeshProUGUI textoPuntuacion;

    // Puntuaci�n actual del jugador
    private int puntuacion;

    // Frecuencia de aparici�n de bombas (en segundos)
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

    //N�mero de bombas generadas, equivalente a puntos ganados. Con 100 puntos ganas.
    private int generadas;

    public int maximoGeneradas;

    private int generadasRojas;
    private int generadasAzules;

    private void Awake()
    {
        // Establecemos la instancia �nica del GameManager
        instance = this;
    }

    private void Start()
    {
        // Iniciamos el juego generando bombas cada frecuenciaBombas segundos
        InvokeRepeating("GenerarBomba", 0f, frecuenciaBombas);
        playing = true;
    }

    // M�todo que genera una bomba roja o azul aleatoriamente
    private void GenerarBomba()
    {
        if (playing && generadas <=maximoGeneradas)
        {
            
            // Generamos un n�mero aleatorio entre 0 y 1
            float aleatorio = Random.Range(0f, 1f);

            // Si el n�mero aleatorio es menor que 0.5, generamos una bomba roja
            if (aleatorio < 0.5f && generadasRojas<=50)
            {
                prefabBombaRoja.SetActive(true);
                // Creamos una bomba roja a partir del prefab
                GameObject bombaRoja = Instantiate(prefabBombaRoja, puertaBombaRoja.transform.position, Quaternion.identity);

                // Establecemos la etiqueta de la bomba roja
                bombaRoja.tag = "BombaRoja";
                generadas += 1;
                generadasRojas += 1;
            }
            // Si no, generamos una bomba azul
            else
            {
                prefabBombaAzul.SetActive(true);
                // Creamos una bomba azul a partir del prefab
                GameObject bombaAzul = Instantiate(prefabBombaAzul, puertaBombaAzul.transform.position, Quaternion.identity);

                // Establecemos la etiqueta de la bomba azul
                bombaAzul.tag = "BombaAzul";
                generadas += 1;
                generadasAzules+= 1;
            }
        }
        else
        {
            GameOver();
        }
    }

    // M�todo que aumenta la puntuaci�n actual
    public void AumentarPuntuacion(int amount)
    {
        // Aumentamos la puntuaci�n actual
        puntuacion += amount;

        // Actualizamos el texto de puntuaci�n
        textoPuntuacion.text = puntuacion.ToString();
    }

    // M�todo que muestra el panel de game over y detiene el juego
    public void GameOver()
    {
        // Activamos el panel de game over
        textoGameOver.text = "Game Over";
        // Detenemos el juego
        Time.timeScale = 0f;
        playing = false;
    }
    
}

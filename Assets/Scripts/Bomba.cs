using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomba : MonoBehaviour
{
    // El componente Rigidbody2D de la bomba
    private Rigidbody2D rb;

    // La posición inicial de la bomba (cuando el usuario comienza a arrastrarla)
    private Vector3 posicionInicial;

    public GameObject cajaRoja;
    public GameObject cajaAzul;

    // Indica si la bomba está siendo arrastrada por el usuario
    private bool estaSiendoArrastrada;

    // La velocidad de movimiento de la bomba
    public float velocidad;

    private int toques;

    private int contadorBombasRojas;
    private int contadorPulsar;
    private int puntuacion;


    private void Start()
    {
        // Obtenemos el componente Rigidbody2D de la bomba
        rb = GetComponent<Rigidbody2D>();

}
    private void Update()
    {
        // Si la bomba está siendo arrastrada por el usuario,
        // la movemos a la posición del ratón
        if (estaSiendoArrastrada)
        {
            Vector3 posicionRaton = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            posicionRaton.z = 0f;
            transform.position = posicionRaton;


        }
        // Si la bomba no está siendo arrastrada, la movemos aleatoriamente por el mapa
        else
        {
            if (cajaRoja.GetComponent<Collider2D>().bounds.Contains(transform.position) || cajaAzul.GetComponent<Collider2D>().bounds.Contains(transform.position))

            {
                GameManager.instance.AumentarPuntuacion(1);
                transform.Translate(Vector3.up * 100f);

            }
            else
            {
                // Calculamos una dirección aleatoria para mover la bomba
                float direccionX = Random.Range(-15f, 15f);
                float direccionY = Random.Range(-15f, 15f);
                // Movemos la bomba hacia la derecha o hacia la izquierda
                transform.position += transform.right * velocidad * Time.deltaTime * direccionX;

                //Movemos la bomba hacia arriba o hacia abajo
                transform.position += transform.up * velocidad * Time.deltaTime * direccionY;
                // Obtenemos las coordenadas de la bomba en pantalla
                Vector3 posicionPantalla = Camera.main.WorldToScreenPoint(transform.position);

                // Obtenemos el ancho y alto de la pantalla en pixels
                float anchoPantalla = Screen.width;
                float altoPantalla = Screen.height;

                // Si la bomba se sale de la pantalla por la derecha o por la izquierda,
                // la movemos de nuevo al borde de la pantalla correspondiente
                if (posicionPantalla.x < 0)
                {
                    Vector3 posicionIzquierda = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, posicionPantalla.z));
                    transform.position = new Vector3(posicionIzquierda.x, transform.position.y, transform.position.z);
                }
                else if (posicionPantalla.x > anchoPantalla)
                {
                    Vector3 posicionDerecha = Camera.main.ScreenToWorldPoint(new Vector3(anchoPantalla, 0, posicionPantalla.z));
                    transform.position = new Vector3(posicionDerecha.x, transform.position.y, transform.position.z);
                }
                else if (posicionPantalla.y < 0)
                {
                    Vector3 posicionAbajo = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, posicionPantalla.z));
                    transform.position = new Vector3(transform.position.x, posicionAbajo.y, transform.position.z);
                }
                else if (posicionPantalla.y > altoPantalla)
                {
                    Vector3 posicionArriba = Camera.main.ScreenToWorldPoint(new Vector3(0, altoPantalla, posicionPantalla.z));
                    transform.position = new Vector3(transform.position.x, posicionArriba.y, transform.position.z);
                }

            }


        }
    }
    private void OnMouseDown()
    {
        // Al hacer clic con el ratón sobre la bomba,
        // guardamos su posición inicial y marcamos que está siendo arrastrada
        posicionInicial = transform.position;
        estaSiendoArrastrada = true;
    }

    private void OnMouseUp()
    {
        // Al soltar el ratón, marcamos que la bomba ya no está siendo arrastrada
        // Si la bomba se encuentra dentro de la caja, no hacemos nada
        estaSiendoArrastrada = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Si la bomba entra en contacto con una caja azul
        if (other.gameObject.CompareTag("CajaAzul"))
        {
            // Si la bomba y la caja tienen el mismo color, aumentamos la puntuación
            if (other.gameObject.GetComponent<SpriteRenderer>().color == GetComponent<SpriteRenderer>().color)
            {
                //GameManager.instance.AumentarPuntuacion(1);
                transform.Translate(Vector3.up * 100f);
            }
            // Si la bomba y la caja tienen distinto color, terminamos el juego
            else
            {
                GameManager.instance.GameOver();
                GameManager.instance.AumentarPuntuacion(-1);
            }
        }
        // Si la bomba entra en contacto con una caja...
        else if (other.gameObject.CompareTag("CajaRoja"))
        {
            // Si la bomba y la caja tienen el mismo color, aumentamos la puntuación
            if (other.gameObject.GetComponent<SpriteRenderer>().color == GetComponent<SpriteRenderer>().color)
            {
                if (!estaSiendoArrastrada)
                {
                    GameManager.instance.AumentarPuntuacion(0);
                }
            }
            // Si la bomba y la caja tienen distinto color, terminamos el juego
            else
            {
                GameManager.instance.GameOver();
            }
            
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("CajaRoja"))
        {
            if (other.gameObject.GetComponent<SpriteRenderer>().color == GetComponent<SpriteRenderer>().color)
                transform.position = cajaRoja.transform.position;
        }
    }


    private void estaEnContactoConBorde()
    {
        // Obtenemos el collider de la caja y el transform de la bomba
        Collider2D colliderCaja = cajaRoja.GetComponent<Collider2D>();
        Transform transformBomba = transform;

        // Calculamos el radio de la bomba (la mitad de su ancho)
        float radioBomba = transformBomba.localScale.x / 2;
        //int toques = 0;

        // Obtenemos la posición del centro de la bomba
        Vector2 posicionCentroBomba = (Vector2)transformBomba.position + transformBomba.GetComponent<Collider2D>().offset;

        // Comprobamos si la bomba está tocando el borde de la caja en cada una de las direcciones
        if (posicionCentroBomba.x - radioBomba < colliderCaja.bounds.min.x || posicionCentroBomba.x + radioBomba > colliderCaja.bounds.max.x)
        {
            toques += 1;
            
        }
        
        if (posicionCentroBomba.y - radioBomba < colliderCaja.bounds.min.y || posicionCentroBomba.y + radioBomba > colliderCaja.bounds.max.y)
        {
            toques += 1;
            
        }

        // Si no se ha detectado ningún contacto con el borde, devolvemos false
        
    }

}

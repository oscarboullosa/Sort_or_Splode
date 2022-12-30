using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomba : MonoBehaviour
{
    // El componente Rigidbody2D de la bomba
    private Rigidbody2D rb;

    // La posici�n inicial de la bomba (cuando el usuario comienza a arrastrarla)
    private Vector3 posicionInicial;

    // Indica si la bomba est� siendo arrastrada por el usuario
    private bool estaSiendoArrastrada;

    private void Start()
    {
        // Obtenemos el componente Rigidbody2D de la bomba
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Si la bomba est� siendo arrastrada por el usuario,
        // la movemos a la posici�n del rat�n
        if (estaSiendoArrastrada)
        {
            Vector3 posicionRaton = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            posicionRaton.z = 0f;
            transform.position = posicionRaton;
        }
    }

    private void OnMouseDown()
    {
        // Al hacer clic con el rat�n sobre la bomba,
        // guardamos su posici�n inicial y marcamos que est� siendo arrastrada
        posicionInicial = transform.position;
        estaSiendoArrastrada = true;
    }

    private void OnMouseUp()
    {
        // Al soltar el rat�n, marcamos que la bomba ya no est� siendo arrastrada
        estaSiendoArrastrada = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Si la bomba entra en contacto con una caja...
        if (other.gameObject.CompareTag("CajaAzul"))
        {
            // Si la bomba y la caja tienen el mismo color, aumentamos la puntuaci�n
            if (other.gameObject.GetComponent<SpriteRenderer>().color == GetComponent<SpriteRenderer>().color)
            {
                GameManager.instance.AumentarPuntuacion(1);
            }
            // Si la bomba y la caja tienen distinto color, terminamos el juego
            else
            {
                GameManager.instance.GameOver();
            }

            // Destruimos la bomba
            Destroy(gameObject);
        }
    }
}

/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caja : MonoBehaviour
{
    // La etiqueta de las bombas del mismo color que esta caja
    public string bombTag;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Si la bomba que entra en contacto con la caja tiene la misma etiqueta
        // que esta caja, aumentamos la puntuación y destruimos la bomba
        if (other.tag == "BombaRoja" && bombTag == "CajaRoja")
        {
            GameManager.instance.AumentarPuntuacion(1);
        }
        else if (other.tag == "BombaAzul" && bombTag == "CajaAzul")
        {
            GameManager.instance.AumentarPuntuacion(1);
        }
        else if (other.tag == "BombaAzul" && bombTag == "CajaRoja")
        {
            GameManager.instance.GameOver();
        }
        else if (other.tag == "BombaRoja" && bombTag == "CajaAzul")
        {
            GameManager.instance.GameOver();
        }

        // Destruimos el objeto
        //Destroy(other.gameObject);
    }
}*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caja : MonoBehaviour
{
    // La etiqueta de las bombas del mismo color que esta caja
    public string bombTag; private void OnTriggerEnter2D(Collider2D other)
    {
        // Si la bomba que entra en contacto con la caja tiene la misma etiqueta
        // que esta caja, desactivamos el componente Rigidbody2D de la bomba y
        // aumentamos la puntuación
        if (other.tag == "BombaRoja" && bombTag == "CajaRoja" && other.GetComponent<Rigidbody2D>() != null)
        {
            other.GetComponent<Rigidbody2D>().isKinematic = true;
            GameManager.instance.AumentarPuntuacion(1);
        }
        else if (other.tag == "BombaAzul" && bombTag == "CajaAzul" && other.GetComponent<Rigidbody2D>() != null)
        {
            other.GetComponent<Rigidbody2D>().isKinematic = true;
            GameManager.instance.AumentarPuntuacion(1);
        }
        else if (other.tag == "BombaAzul" && bombTag == "CajaRoja")
        {
            GameManager.instance.GameOver();
        }
        else if (other.tag == "BombaRoja" && bombTag == "CajaAzul")
        {
            GameManager.instance.GameOver();
        }
    }
}

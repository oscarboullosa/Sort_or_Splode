using System.Collections;
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
        if (other.tag == "CajaRoja" && bombTag== "BombaRoja")
        {
            Destroy(other.gameObject);
        }
        else if(other.tag == "CajaAzul" && bombTag == "BombaAzul")
        {
            Destroy(other.gameObject);
        }
        else if (other.tag == "CajaAzul" && bombTag == "BombaRoja")
        {
            GameManager.instance.GameOver();
        }
        else if (other.tag == "CajaRoja" && bombTag == "BombaAzul")
        {
            GameManager.instance.GameOver();
        }
    }
}

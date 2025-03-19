using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    [SerializeField] private float shootSpeed = 2.5f;
    private Collider2D collider;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        //Quand un ennemi rentre la tourelle tire
    }

    // Start is called before the first frame update
    void Start()
    {
        //PLacer la tourelle sur "grass" uniquement et elle bouge plus
    }

    // Update is called once per frames
    void Update()
    {
        //On Verifie a chaque frame si il y a un ennemi dans le trigger de la tourelle
    }
}

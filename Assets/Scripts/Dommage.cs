using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dommage : MonoBehaviour{
    public int dommage;
    public float tempsAttente;

    private void OnCollisionEnter2D(Collision2D other){
        if (other.gameObject.CompareTag("Player")){
            other.gameObject.GetComponent<Vie>().perdreVie(dommage, tempsAttente);
        }
    }

    private void OnCollisionStay2D(Collision2D other){
        if (other.gameObject.CompareTag("Player")){
            other.gameObject.GetComponent<Vie>().perdreVie(dommage, tempsAttente);
        }
    }

    private void OnCollisionExit2D(Collision2D other){
        if (other.gameObject.CompareTag("Player")){
            other.gameObject.GetComponent<Vie>().doitAttendre = false;
        }
    }
}
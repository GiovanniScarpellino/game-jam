using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionEnnemi : MonoBehaviour{
    private void OnTriggerStay2D(Collider2D other){
        if (other.CompareTag("Player")) {
            transform.parent.gameObject.GetComponent<EnnemiController>().seDirigerVersLeJoueur();
            transform.parent.gameObject.GetComponent<EnnemiController>().suitJoueur = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other){
        if (other.CompareTag("Player")) {
            transform.parent.gameObject.GetComponent<EnnemiController>().trouverCheminOrcApresPerteJoueur();
            transform.parent.gameObject.GetComponent<EnnemiController>().suitJoueur = false;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionEnnemi : MonoBehaviour{
    private void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Player")) {
            transform.parent.gameObject.GetComponent<EnnemiController>().allieCible = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other){
        if (other.CompareTag("Player")) {
            transform.parent.gameObject.GetComponent<EnnemiController>().allieCible = null;
            transform.parent.gameObject.GetComponent<EnnemiController>().trouverCheminOrcApresPerteJoueur();
        }
    }
}
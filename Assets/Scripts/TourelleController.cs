using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using UnityEngine;

public class TourelleController : MonoBehaviour{
    public GameObject missile;
    private float compteur;
    private GameObject cible;

    private bool faireFeu;
    private float startTime;
    public int cadence = 1;

    private void Start(){
        cible = null;
    }

    private void Update(){
        if (cible != null){
            var dir = cible.transform.position - transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            startTime += Time.deltaTime;
    
            if (faireFeu)
                Fire();
            else if (startTime >= cadence){
                startTime = 0;
                faireFeu = true;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other){
        if (cible == null && other.CompareTag("Ennemi")) cible = other.gameObject;
    }

    private void OnTriggerExit2D(Collider2D other){
        if (other.gameObject == cible){
            cible = null;
        }
    }
    
    private void Fire(){
        faireFeu = false;
        Instantiate(missile, transform.position, Quaternion.identity).GetComponent<MissileController>().position = cible.transform.position;
    }
}
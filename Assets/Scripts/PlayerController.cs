using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour{
    private GameObject mapGenerator;
    private Vector2 mousePosition;

    public float speed;
    private List<Vector2> chemins;
    private bool peutBouger;
    private bool peutPasserALaCaseSuivant;

    private void Start(){
        mapGenerator = GameObject.Find("MapGenerator");
    }

    private void Update(){
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.x = Mathf.Floor(mousePosition.x + 0.5f);
        mousePosition.y = Mathf.Floor(mousePosition.y + 0.5f);
        if (Input.GetMouseButtonDown(0)){
            chemins = mapGenerator.GetComponent<oPathFinding>().FindPath(transform.position, mousePosition);
            peutBouger = true;
        }
        if (peutBouger){
            var step = speed * Time.deltaTime;
            for (var i = 0; i < chemins.Count; i++){
                if (i == 0){
                    transform.position = Vector2.MoveTowards(transform.position, chemins[0], step);
                } else{
                    transform.position = Vector2.MoveTowards(chemins[i - 1], chemins[i], step);
                }
            }
            peutBouger = false;
        }
    }
}
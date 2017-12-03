using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour{
    public GameObject pathFinding;
    private Grille grille;
    public float speed;
    private bool test;

    private void Start(){
        grille = pathFinding.GetComponent<Grille>();
    }

    private void Update(){
        if (Input.GetMouseButtonDown(1)){
            test = !test;
        }
        if (test){
            if (grille.chemin != null){
                foreach (var noeud in grille.chemin){
                    var step = speed * Time.deltaTime;
                    transform.position = Vector3.MoveTowards(transform.position, noeud.position, step);
                }
            }
        }
    }
}
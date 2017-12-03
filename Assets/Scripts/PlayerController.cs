using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour{
    private List<Vector2> pathPoint;
    private int currentPathPoint; //point courant du chemin que nous tentons de rejoindre

    private const float VITESSE = 5f;
    private Rigidbody2D body;

    private float timer;

    private GameObject mapGenerator;
    private bool enDeplacement;
    private Vector2 mousePosition;
    
    // Use this for initialization
    private void Start(){
        body = GetComponent<Rigidbody2D>(); //on récupère le rigidbody de notre ennemi
        mapGenerator = GameObject.Find("MapGenerator");
    }

    // Update is called once per frame
    private void Update(){
        if (Input.GetMouseButtonDown(0)){
            enDeplacement = true;
            mousePosition = new Vector2(0, 0);
            mousePosition.x = Mathf.Floor(Camera.main.ScreenToWorldPoint(Input.mousePosition).x + .5f);
            mousePosition.y = Mathf.Floor(Camera.main.ScreenToWorldPoint(Input.mousePosition).y + .5f);
            pathPoint = mapGenerator.GetComponent<oPathFinding>().FindPath(transform.position, mousePosition);
        }
        if (enDeplacement){
            print(currentPathPoint + " " + pathPoint.Count);
            if (currentPathPoint < pathPoint.Count){
                var target = pathPoint[currentPathPoint];

                var moveDirection = target - (Vector2) transform.position;
                var velocity = body.velocity;

                if (moveDirection.magnitude < .1){
                    currentPathPoint++;
                } else{
                    velocity = moveDirection.normalized * VITESSE;
                }

                body.velocity = velocity;
            } else{
                body.velocity = Vector2.zero;
                currentPathPoint = 0;
                enDeplacement = false;
            }
        }
    }
}
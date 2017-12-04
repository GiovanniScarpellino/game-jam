using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiController : Controllers {

    public Vector2 positionCampOrc;
    public Vector2 positionCampAllie;

    public float vitesse;
    
    public List<Vector2> cheminOrc;
    private int currentPathPoint;

    private Rigidbody2D body;

    public void trouverCheminOrc(Vector2 _positionCampOrc, Vector2 _positionCampAllie) {
        positionCampAllie = _positionCampAllie;
        positionCampOrc = _positionCampOrc;
        body = GetComponent<Rigidbody2D>();
        cheminOrc = GameObject.Find("MapGenerator").GetComponent<oPathFinding>().FindPath(_positionCampOrc, _positionCampAllie, false);
        currentPathPoint = 0;
    }

    private void Update(){
        if (cheminOrc != null) {
            if (currentPathPoint < cheminOrc.Count){
                var target = cheminOrc[currentPathPoint];

                var moveDirection = target - (Vector2) transform.position;
                var velocity = body.velocity;

                if (moveDirection.magnitude < .1){
                    currentPathPoint++;
                } else{
                    velocity = moveDirection.normalized * vitesse;
                }

                body.velocity = velocity;
            } else{
                body.velocity = Vector2.zero;
            }
        }
    }
}
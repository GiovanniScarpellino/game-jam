using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiController : MonoBehaviour{
    public Vector2 positionCampOrc;
    public Vector2 positionCampAllie;

    public float vitesse;

    private List<Vector2> cheminOrc;
    private int currentPathPoint;

    public GameObject allieCible;

    private Rigidbody2D body;
    private GameObject camp;

    private void Start(){
        camp = GameObject.Find("Camp(Clone)");
        GetComponent<AnimatorController>().enDeplacement = true;
    }

    public void trouverCheminOrc(Vector2 _positionCampOrc, Vector2 _positionCampAllie){
        positionCampAllie = _positionCampAllie;
        positionCampOrc = _positionCampOrc;
        body = GetComponent<Rigidbody2D>();
        cheminOrc = GameObject.Find("MapGenerator").GetComponent<oPathFinding>().FindPath(_positionCampOrc, _positionCampAllie, false);
        currentPathPoint = 0;
    }

    public void trouverCheminOrcApresPerteJoueur(){
        Vector2 positionOrc = transform.position;
        positionOrc.x = Mathf.Floor(positionOrc.x + .5f);
        positionOrc.y = Mathf.Floor(positionOrc.y + .5f);
        cheminOrc = GameObject.Find("MapGenerator").GetComponent<oPathFinding>().FindPath(positionOrc, positionCampAllie, false);
        currentPathPoint = 0;
    }

    private void Update(){
        if (allieCible == null){
            if (cheminOrc != null){
                if (currentPathPoint < cheminOrc.Count){
                    var target = cheminOrc[currentPathPoint];

                    Vector2 dir;
                    try{
                        dir = target - cheminOrc[currentPathPoint - 1];
                    } catch{
                        dir = target - (Vector2) transform.position;
                    }
                    var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;

                    GetComponent<AnimatorController>().angle = angle;
                    
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
                    Destroy(gameObject);
                    camp.GetComponent<Vie>().perdreVie(1, gameObject);
                }
            }
        } else{
            Vector2 moveDirection = (allieCible.transform.position - transform.position);
            Vector2 velocite = moveDirection.normalized * vitesse;
            body.velocity = velocite;
        }
    }
}
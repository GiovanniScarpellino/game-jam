using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiController : MonoBehaviour{
    public Vector2 positionCampAllie;
    public float vitesse;
    public bool suitJoueur{ set; private get; }

    private List<Vector2> cheminOrc;
    private int currentPathPoint;
    
    private Rigidbody2D body;
    private GameObject mapGenerator;
    private GameObject player;
    private GameObject camp;

    private void Start(){
        camp = GameObject.Find("Camp(Clone)");
        GetComponent<AnimatorController>().enDeplacement = true;
        player = GameObject.Find("Joueur(Clone)");
        body = GetComponent<Rigidbody2D>();
    }

    public void trouverCheminOrc(Vector2 positionCampAllie){
        mapGenerator = GameObject.Find("MapGenerator");
        this.positionCampAllie = positionCampAllie;
        cheminOrc = trouverChemin(positionCampAllie);
        currentPathPoint = 0;
    }

    public void trouverCheminOrcApresPerteJoueur(){
        cheminOrc = trouverChemin(positionCampAllie);
        currentPathPoint = 0;
    }

    public void seDirigerVersLeJoueur(){
        Vector2 position = new Vector2(Mathf.Floor(player.transform.position.x + 0.5f), Mathf.Floor(player.transform.position.y + 0.5f));
        cheminOrc = trouverChemin(position);
        currentPathPoint = 0;
    }

    private void Update(){
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
                if (!suitJoueur){
                    Destroy(gameObject);
                    camp.GetComponent<Vie>().perdreVie(1, gameObject);   
                }
            }
        }
    }

    private List<Vector2> trouverChemin(Vector2 position){
        return mapGenerator.GetComponent<oPathFinding>().FindPath(new Vector2(Mathf.Floor(transform.position.x + 0.5f), Mathf.Floor(transform.position.y + 0.5f)), position, false);
    }
}
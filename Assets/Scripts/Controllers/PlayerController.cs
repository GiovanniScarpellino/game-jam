using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class PlayerController : Controllers{
    public List<Vector2> pathPoint{ set; get; }
    public int currentPathPoint{ set; private get; }

    private const float VITESSE = 5f;
    private Rigidbody2D body;

    private float timer;

    private GameObject mapGenerator;
    public bool enDeplacement{ set; private get; }
    private Vector2 mousePosition;

    // Use this for initialization
    private void Start(){
        body = GetComponent<Rigidbody2D>(); //on récupère le rigidbody de notre ennemi
        mapGenerator = GameObject.Find("MapGenerator");
    }

    // Update is called once per frame
    private void Update(){
        if (Input.GetMouseButton(0)){
            mousePosition = new Vector2(0, 0);
            mousePosition.x = Mathf.Floor(Camera.main.ScreenToWorldPoint(Input.mousePosition).x + .5f);
            mousePosition.y = Mathf.Floor(Camera.main.ScreenToWorldPoint(Input.mousePosition).y + .5f);
            enDeplacement = true;
            currentPathPoint = 0;
            pathPoint = trouverChemin(mousePosition);
        }

        //PathFinding vers Arbre
        if (Input.GetMouseButtonDown(1)){
            mousePosition = new Vector2();
            mousePosition.x = Mathf.Floor(Camera.main.ScreenToWorldPoint(Input.mousePosition).x + .5f);
            mousePosition.y = Mathf.Floor(Camera.main.ScreenToWorldPoint(Input.mousePosition).y + .5f);
            enDeplacement = true;
            currentPathPoint = 0;
            pathPoint = trouverChemin(mousePosition);

            //Position la plus proche vers l'arbre sans diagonales
            if (mapGenerator.GetComponent<MapGenerator>().arbreSurPosition(mousePosition) != null && (mousePosition - new Vector2(transform.position.x, transform.position.y)).magnitude > 1.2){
                int distanceMinimum = 9999;
                List<Vector2> meilleurChemin = new List<Vector2>();
                for (int i = -1; i <= 1; i++){
                    for (int j = -1; j <= 1; j++){
                        if ((3 * (i + 1) + j + 1) % 2 == 1){
                            Vector2 positionActuelle = new Vector2(mousePosition.x + i, mousePosition.y + j);
                            if (positionActuelle.x >= 0 && positionActuelle.x < mapGenerator.GetComponent<MapGenerator>().largeur &&
                                positionActuelle.y >= 0 && positionActuelle.y < mapGenerator.GetComponent<MapGenerator>().hauteur){
                                List<Vector2> cheminTrouve = trouverChemin(positionActuelle);
                                if (cheminTrouve.Count > 0 && cheminTrouve.Count < distanceMinimum){
                                    distanceMinimum = cheminTrouve.Count;
                                    meilleurChemin = cheminTrouve;
                                }
                            }
                        }
                    }
                }
                if (meilleurChemin.Count != 0)
                    pathPoint = meilleurChemin;
            }
        }

        if (enDeplacement){
            if (currentPathPoint < pathPoint.Count){
                try{
                    targetAnimation = pathPoint[currentPathPoint + 1];
                } catch{
                    GetComponent<AnimationController>().pendantParcous = false;
                    targetAnimation = pathPoint[currentPathPoint];
                }
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
                enDeplacement = false;
            }
        }
    }

    private List<Vector2> trouverChemin(Vector2 position){
        GetComponent<AnimationController>().pendantParcous = true;
        return mapGenerator.GetComponent<oPathFinding>().FindPath(new Vector2(Mathf.Floor(transform.position.x + 0.5f), Mathf.Floor(transform.position.y + 0.5f)), position, true);
    }
}
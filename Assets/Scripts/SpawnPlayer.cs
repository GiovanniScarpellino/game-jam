using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour{
    public GameObject joueur;
    public GameObject camp{ set; private get; }
    private bool joueurPlace;
    private GameObject parentDuSol;

    private List<Vector2> casesPossibles;
    private List<GameObject> uniteBlanches;
    public GameObject uniteBlanche{ set; private get; }

    private GameObject mapGenerator;

    private void Start(){
        casesPossibles = new List<Vector2>();
        uniteBlanches = new List<GameObject>();
        joueur = Instantiate(joueur);
        parentDuSol = GameObject.Find("Parent du sol");
        mapGenerator = GameObject.Find("MapGenerator");

        MapGenerator componentMapGenerator = mapGenerator.GetComponent<MapGenerator>();
        for (int y = -2; y <= 2; y++) {
            for (int x = -2; x <= 2; x++) {
                if (y == -2 || y == 2 || x == -2 || x ==2) {
                    Vector2 positionVerification = new Vector2(camp.transform.position.x + x, camp.transform.position.y + y);
                    if (positionVerification.x >= 0 && positionVerification.x < componentMapGenerator.largeur &&
                        positionVerification.y >= 0 && positionVerification.y < componentMapGenerator.hauteur &&
                        componentMapGenerator.tuileSurPosition(positionVerification) != MapGenerator.TypeTuile.Eau) {
                        casesPossibles.Add(positionVerification);
                        var uniteBlancheColore = Instantiate(uniteBlanche, positionVerification, Quaternion.identity);
                        uniteBlancheColore.GetComponent<SpriteRenderer>().color = new Color(0, 255, 0, 0.5f);
                        uniteBlancheColore.GetComponent<SpriteRenderer>().sortingLayerName = "PlacementJoueur";
                        uniteBlanches.Add(uniteBlancheColore);
                    }
                }
            }
        }
    }

    private void Update(){
        if (!joueurPlace){
            Vector2 positionJoueur = new Vector2();
            positionJoueur.x = Mathf.Floor(Camera.main.ScreenToWorldPoint(Input.mousePosition).x + .5f);
            positionJoueur.y = Mathf.Floor(Camera.main.ScreenToWorldPoint(Input.mousePosition).y + .5f);
            if (casesPossibles.Contains(positionJoueur)){
                joueur.transform.position = positionJoueur;
                if (Input.GetMouseButtonDown(0) && !joueurPlace){
                    joueur.GetComponent<AnimationController>().triggerActuel = "Idle";
                    foreach (var uniteBlanche in uniteBlanches){
                        Destroy(uniteBlanche);
                    }
                    Camera.main.GetComponent<CameraController>().enabled = true;
                    joueurPlace = true;
                }
            }
        } else{
            for (int i = 0; i < parentDuSol.transform.childCount; i++){
                if (casesPossibles.Contains(parentDuSol.transform.GetChild(i).transform.position)){
                    parentDuSol.transform.GetChild(i).GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);
                }
            }
            joueur.GetComponent<PlayerController>().enabled = true;
            Destroy(this);
        }
    }
}
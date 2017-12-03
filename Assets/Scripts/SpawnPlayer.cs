using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour{
    public GameObject joueur;
    public GameObject camp{ set; private get; }
    private bool joueurPlace;
    private GameObject parentDuSol;
    private GameObject parentDesArbres;

    private List<Vector2> casesPossibles;
    private List<GameObject> uniteBlanches;
    public GameObject uniteBlanche{ set; private get; }

    private void Start(){
        casesPossibles = new List<Vector2>();
        uniteBlanches = new List<GameObject>();
        joueur = Instantiate(joueur);
        parentDuSol = GameObject.Find("Parent du sol");
        parentDesArbres = GameObject.Find("Parent des arbres");
        for (var i = 0; i < parentDuSol.transform.childCount; i++){
            var child = parentDuSol.transform.GetChild(i);
            if (child.position.x == camp.transform.position.x - 2 && child.position.y <= camp.transform.position.y + 2 && child.position.y >= camp.transform.position.y - 2
                || child.position.x == camp.transform.position.x + 2 && child.position.y <= camp.transform.position.y + 2 && child.position.y >= camp.transform.position.y - 2
                || child.position.x >= camp.transform.position.x - 2 && child.position.x <= camp.transform.position.x + 2 && child.position.y == camp.transform.position.y - 2
                || child.position.x >= camp.transform.position.x - 2 && child.position.x <= camp.transform.position.x + 2 && child.position.y == camp.transform.position.y + 2){
                casesPossibles.Add(child.position);
                var uniteBlancheColore = Instantiate(uniteBlanche, child.transform.position, Quaternion.identity);
                uniteBlancheColore.GetComponent<SpriteRenderer>().color = new Color(0, 255, 0, 0.5f);
                uniteBlanches.Add(uniteBlancheColore);
            }
        }
        parentDesArbres.SetActive(false);
    }

    private void Update(){
        if (!joueurPlace){
            Vector2 positionJoueur = new Vector2();
            positionJoueur.x = Mathf.Floor(Camera.main.ScreenToWorldPoint(Input.mousePosition).x + .5f);
            positionJoueur.y = Mathf.Floor(Camera.main.ScreenToWorldPoint(Input.mousePosition).y + .5f);
            if (casesPossibles.Contains(positionJoueur)){
                joueur.transform.position = positionJoueur;
            }
        } else{
            for (int i = 0; i < parentDuSol.transform.childCount; i++){
                if (casesPossibles.Contains(parentDuSol.transform.GetChild(i).transform.position)){
                    parentDuSol.transform.GetChild(i).GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);
                }
            }
        }
        if (Input.GetMouseButtonDown(0)){
            for (var i = 0; i < uniteBlanches.Capacity; i++){
                Destroy(uniteBlanches[i]);
            }
            Camera.main.GetComponent<CameraController>().enabled = true;
            Destroy(this);
            joueurPlace = true;
        }
    }
}
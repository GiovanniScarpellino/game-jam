using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class CouperLesArbres : MonoBehaviour{
    public GameObject arbre;
    private MapGenerator mapGenerator;
    private PlayerInfo playerInfo;

    private Vector3 vecteurDistance;
    private float distance;
    private const float TEMPS_INITIAL = 2.0f;
    private float tempsRestant = TEMPS_INITIAL;
    private bool couperArbre = true;
    private GameObject arbreACouper;
    private Animation anim;

    // Use this for initialization
    void Start(){
        mapGenerator = GameObject.FindGameObjectWithTag("MapGenerator").GetComponent<MapGenerator>();
        playerInfo = Camera.main.GetComponent<PlayerInfo>();
    }

    // Update is called once per frame
    void Update(){
        if (!couperArbre){
            tempsRestant -= Time.deltaTime;
            if (tempsRestant <= 0){
                couperArbre = true;
                tempsRestant = 2f;
                Destroy(arbreACouper);
                playerInfo.ajoutBois();
                GameObject.FindGameObjectWithTag("MapGenerator").GetComponent<oPathFinding>().definirNoeudArbre(arbreACouper.transform.position, false);
                Camera.main.GetComponent<IconCursor>().effacerIconCursor();
            }

            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)){
                tempsRestant = TEMPS_INITIAL;
                arbreACouper.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 1);
                arbreACouper.transform.GetChild(1).GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 1);
                couperArbre = true;
                anim.Stop();
            }
        } else if (Input.GetMouseButtonDown(1) && couperArbre){
            Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouse.x = Mathf.Floor(mouse.x + 0.5f);
            mouse.y = Mathf.Floor(mouse.y + 0.5f);
            if (mapGenerator.arbreSurPosition(mouse) != null){
                distance = 0f;
                vecteurDistance = gameObject.transform.position - new Vector3(mouse.x, mouse.y, 0);
                distance = vecteurDistance.magnitude;
                distance = Mathf.Floor(distance + 0.5f);
                if (distance <= 1){
                    couperArbre = false;
                    arbreACouper = mapGenerator.arbreSurPosition(mouse);
                    anim = arbreACouper.gameObject.GetComponent<Animation>();
                    anim.Play();
                    jouerSon();
                    Invoke("jouerSon", 0.9f);
                    Invoke("jouerSon", 1.8f);
                }
            }
        }
    }

    private void jouerSon(){
        AudioSource[] sons = Camera.main.GetComponents<AudioSource>();
        sons[1].Play();
    }
}
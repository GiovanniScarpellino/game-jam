﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CouperLesArbres : MonoBehaviour {
    public GameObject arbre;
    public MapGenerator mapGenerator;

    private Vector3 vecteurDistance;
    private float distance;
    private float tempsRestant = 2.0f;
    private bool couperArbre = true;
    private GameObject arbreACouper;

    public enum couleurArbre {blanc, bleu}
    public couleurArbre couleurArbreSelectionnee = couleurArbre.blanc;
    public Animation animationArbre;

	// Use this for initialization
	void Start () {
        mapGenerator = GameObject.FindGameObjectWithTag("MapGenerator").GetComponent<MapGenerator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!couperArbre){
            tempsRestant -= Time.deltaTime;
            if (tempsRestant <= 0){
                couperArbre = true;
                tempsRestant = 2f;
                Destroy(arbreACouper);
            }
        }
        else if (Input.GetMouseButtonDown(1) && couperArbre)
        {
            Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouse.x = Mathf.Floor(mouse.x + 0.5f);
            mouse.y = Mathf.Floor(mouse.y + 0.5f);
            if (mapGenerator.arbreSurPosition(mouse) != null)
            {
                distance = 0f;
                vecteurDistance = this.gameObject.transform.position - new Vector3(mouse.x, mouse.y, 0);
                distance = vecteurDistance.magnitude;
                distance = Mathf.Floor(distance + 0.5f);
                print("distance : " + distance);
                if (distance <= 1)
                {
                    couperArbre = false;
                    arbreACouper = mapGenerator.arbreSurPosition(mouse);
                    Animation anim = arbreACouper.gameObject.GetComponent<Animation>();
                    anim.Play();
                }
            }
        }
    }
}

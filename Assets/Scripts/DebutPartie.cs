using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebutPartie : MonoBehaviour {
	public enum EtatDebutPartie {
		Dezoom,
		ChoixCamp,
		Zoom
	}

	[Header("Zoom")]
	public float dezoomMax; //Zoom maximal sur la carte
	private float startZoom; //Zoom initial
	private float etatDezoom; //Stade des zoom dans la fonction Sigmoid (de -1 à 1)
	public float incrementationDezoom; //Vitesse de la fonction Sigmoid

	[Header("Choix du camp")]
	public GameObject prefabCamp; //Prefab du camp a poser
	private GameObject campAPoser; //Camp actuellement en main

	[Header("Deplacement caméra")]
	public float vitesseCamera; //Vitesse de déplacement de la camera
	public int nombreTuilesX;
	public int nombreTuilesY;
	public Vector2 offsetLimitesCamera; //Offset de la limite du déplacement de la caméra

	private EtatDebutPartie etatDebutPartie;

	// Use this for initialization
	void Start() {
		startZoom = Camera.main.orthographicSize;
		etatDebutPartie = EtatDebutPartie.Dezoom;
		etatDezoom = -1;

		MapGenerator mapGenerator = GameObject.FindGameObjectWithTag("MapGenerator").GetComponent<MapGenerator>();
		nombreTuilesX = mapGenerator.largeur;
		nombreTuilesY = mapGenerator.hauteur;
	}

	// Update is called once per frame
	void Update() {
		switch (etatDebutPartie) {
			case EtatDebutPartie.Dezoom:
				float valeurIncrementationDezoom = sigmoid(etatDezoom);
				etatDezoom += incrementationDezoom * Time.deltaTime;
				if (etatDezoom >= 1) {
					Camera.main.orthographicSize = dezoomMax;
					etatDebutPartie = EtatDebutPartie.ChoixCamp;
				}
				else {
					Camera.main.orthographicSize = startZoom + ((dezoomMax - startZoom) * valeurIncrementationDezoom);
				}
				break;
			case EtatDebutPartie.ChoixCamp:
				if (campAPoser == null)
					campAPoser = Instantiate(prefabCamp);
				Vector2 positionCamp = new Vector2();
				positionCamp.x = Mathf.Floor(Camera.main.ScreenToWorldPoint(Input.mousePosition).x);
				positionCamp.y = Mathf.Floor(Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
				campAPoser.transform.position = positionCamp;
				break;
		}

		if (etatDebutPartie == EtatDebutPartie.ChoixCamp) {
			Vector2 positionSouris = Input.mousePosition;
			Vector3 positionCamera = Camera.main.transform.position;
			if (positionSouris.x <= 0 && Camera.main.ScreenToWorldPoint(Vector3.zero).x >= -offsetLimitesCamera.x)
				positionCamera.x -= vitesseCamera * Time.deltaTime;
			if (positionSouris.x >= Screen.width && Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height)).x <= nombreTuilesX - offsetLimitesCamera.x)
				positionCamera.x += vitesseCamera * Time.deltaTime;
			if (positionSouris.y <= 0 && Camera.main.ScreenToWorldPoint(Vector3.zero).y >= -offsetLimitesCamera.y)
				positionCamera.y -= vitesseCamera * Time.deltaTime;
			if (positionSouris.y >= Screen.height && Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height)).y <= nombreTuilesY - offsetLimitesCamera.y)
				positionCamera.y += vitesseCamera * Time.deltaTime;
			Camera.main.transform.position = positionCamera;
		}
	}

	//Fonction qui permet de calculer les animations de zoom
	float sigmoid(float x) {
		return 1 / (1 + Mathf.Exp(-5.85f * x));
	}
}
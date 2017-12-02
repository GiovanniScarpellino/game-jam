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
	public GameObject uniteBlanche; //Unite blanche
	public string nomGameObjectPlacementBatiment;
	private GameObject campAPoser; //Camp actuellement en main

	[Header("Deplacement caméra")]
	public float vitesseCamera; //Vitesse de déplacement de la camera
	public int nombreTuilesX;
	public int nombreTuilesY;
	public Vector2 offsetLimitesCamera; //Offset de la limite du déplacement de la caméra

	private EtatDebutPartie etatDebutPartie; //Etat du début de la partie (Dezoom, choix de l'emplacement du camp, zoom)

	private MapGenerator mapGenerator; //Instance de la map generator
	
	//Zone autour du camp
	private int debutXCamp = -1;
	private int finXCamp = 1;
	private int debutYCamp = -1;
	private int finYCamp = 1;

	// Use this for initialization
	void Start() {
		startZoom = Camera.main.orthographicSize;
		etatDebutPartie = EtatDebutPartie.Dezoom;
		etatDezoom = -1;

		mapGenerator = GameObject.FindGameObjectWithTag("MapGenerator").GetComponent<MapGenerator>();
		nombreTuilesX = mapGenerator.largeur;
		nombreTuilesY = mapGenerator.hauteur;
	}

	// Update is called once per frame
	void Update() {
		switch (etatDebutPartie) {
			//Dezoom général
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
			//Choix de la position du camp
			case EtatDebutPartie.ChoixCamp:
				Vector2 positionCamp = new Vector2();
				positionCamp.x = Mathf.Floor(Camera.main.ScreenToWorldPoint(Input.mousePosition).x + .5f);
				positionCamp.y = Mathf.Floor(Camera.main.ScreenToWorldPoint(Input.mousePosition).y + .5f);
				if (!Input.GetMouseButtonDown(0)) {
					if (campAPoser == null) {
						campAPoser = Instantiate(prefabCamp);
						GameObject placementBatiment = Instantiate(uniteBlanche);
						placementBatiment.name = nomGameObjectPlacementBatiment;
						placementBatiment.transform.parent = campAPoser.transform;
						placementBatiment.transform.localScale = new Vector3(.96f, .92f, 1);
						placementBatiment.transform.position = campAPoser.transform.position;
						placementBatiment.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, .4f);
					}
					campAPoser.transform.position = positionCamp;
					//Coloration du camp pour une position disponible
					bool positionValide = true;
					for (int y = debutYCamp; y <= finYCamp; y++) {
						for (int x = debutXCamp; x <= finXCamp; x++) {
							//Verification des tuiles autours du camp
							try {
								if (mapGenerator.tuilesMap[(int) positionCamp.y + y, (int) positionCamp.x + x] == MapGenerator.TypeTuile.Eau)
									positionValide = false;
							}
							catch (IndexOutOfRangeException exception) {
								positionValide = false;
							}
						}
					}
					campAPoser.GetComponent<SpriteRenderer>().color = positionValide ? Color.white : Color.red;
				}
				else {
					bool positionValide = true;
					for (int y = debutYCamp; y <= finYCamp; y++) {
						for (int x = debutXCamp; x <= finXCamp; x++) {
							//Verification des tuiles autours du camp
							//Si c'est de l'eau ou en dehors de la carte la position n'est plus valide
							try {
								if (mapGenerator.tuilesMap[(int) positionCamp.y + y, (int) positionCamp.x + x] == MapGenerator.TypeTuile.Eau)
									positionValide = false;
							}
							catch (IndexOutOfRangeException exception) {
								positionValide = false;
							}
						}
					}
					if (positionValide) {
						//Détruit les arbres autours
						campAPoser.transform.Find("ColliderPoseCamp").GetComponent<BoxCollider2D>().enabled = true;
						//Enlève la zone noir de visualisation de placement
						Destroy(campAPoser.transform.Find(nomGameObjectPlacementBatiment).gameObject);
						//Passe au prochain état du début de partie : Le zoom qui recentre sur le camp
						etatDebutPartie = EtatDebutPartie.Zoom;
					}
				}
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
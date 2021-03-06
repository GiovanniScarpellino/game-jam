﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapGenerator : MonoBehaviour {
	[Serializable]
	public class Count {
		public int minimum;
		public int maximum;

		public Count(int minimum, int maximum) {
			this.minimum = minimum;
			this.maximum = maximum;
		}
	}

	public enum TypeTuile {
		Eau,
		Terre,
		Herbe
	}

	[Header("Génération")]
	//Données de la génération
	public int largeur;
	public int hauteur;
	public int seed;
	public float scale;
	public int offsetSeedArbre;
	public float scaleArbre;

	[Space]
	[Header("Tuiles")]
	//Tuiles utilisées
	public float tailleTuile;
	public GameObject[] prefabsTerre;
	public GameObject[] prefabsHerbe;
	public GameObject[] prefabsEau;
	public GameObject prefabArbre;

	//Parent des tuiles
	private Transform parentSol;
	private Transform parentArbres;

	public TypeTuile[,] tuilesMap { get; private set; }
	public List<GameObject> listeArbres { get; private set; }

	private void Start() {
		//Création des parents des objets de la génération
		if(parentSol != null)
			Destroy(parentSol.gameObject);
		parentSol = new GameObject("Parent du sol").transform;
		if(parentArbres != null)
			Destroy(parentArbres.gameObject);
		parentArbres = new GameObject("Parent des arbres").transform;

		//Création d'un seed pour la map
		seed = Random.Range(0, 999999);
		Random.InitState(seed);
		
		//Générer la map
		tuilesMap = new TypeTuile[hauteur, largeur];
		listeArbres = new List<GameObject>();
		genererMap();
		Camera.main.transform.position = new Vector3(largeur / 2f, hauteur / 2f, -10);
		
		//Creer la grille pour le pathFinding
        if(GetComponent<oGrille>() != null)
		    GetComponent<oGrille>().CreateGrid();
	}

	public void genererMap() {
		for (int y = 0; y < hauteur; y++) {
			for (int x = 0; x < largeur; x++) {
				//Valeurs pour la génération
				float valeurMaxEau = .35f;
				float valeurMaxTerre = .55f;
				
				//Récupération de la valeur aléatoire pour la génération du terrain
				float xCoord = ((float) x + seed) / largeur * scale ;
				float yCoord = ((float) y + seed) / hauteur * scale;
				float valeurPerlin = Mathf.PerlinNoise(xCoord, yCoord);
				
				//Instantiation de la tuile du sol
				GameObject nouvelleTuile;
				if (valeurPerlin < valeurMaxEau) {
					nouvelleTuile = Instantiate(prefabsEau[Random.Range(0, prefabsEau.Length)]);
					tuilesMap[y, x] = TypeTuile.Eau;
				}
				else if (valeurPerlin < valeurMaxTerre) {
					nouvelleTuile = Instantiate(prefabsTerre[Random.Range(0, prefabsTerre.Length)]);
					tuilesMap[y, x] = TypeTuile.Terre;
				}
				else {
					nouvelleTuile = Instantiate(prefabsHerbe[Random.Range(0, prefabsHerbe.Length)]);
					tuilesMap[y, x] = TypeTuile.Herbe;
				}
				nouvelleTuile.transform.position = new Vector3(x * tailleTuile, y * tailleTuile);
				nouvelleTuile.transform.parent = parentSol;
				
				//Instantiation des arbres
				if (valeurPerlin >= valeurMaxEau) {
					//Récupération de la valeur aléatoire pour la génération des arbres
					float xCoordArbre = ((float) x + seed + offsetSeedArbre) / largeur * scaleArbre ;
					float yCoordArbre = ((float) y + seed + + offsetSeedArbre) / hauteur * scaleArbre;
					float valeurPerlinArbre = Mathf.PerlinNoise(xCoordArbre, yCoordArbre);
					if (valeurPerlinArbre > .52f) {
						GameObject nouvelArbre = Instantiate(prefabArbre);
						nouvelArbre.transform.position = new Vector3(x * tailleTuile, y * tailleTuile);
						nouvelArbre.transform.parent = parentArbres;
						listeArbres.Add(nouvelArbre);
					}
				}
			}
		}
	}

	public GameObject arbreSurPosition(Vector2 position) {
		for (int i = 0; i < parentArbres.childCount; i++) {
			Vector3 positionArbre = parentArbres.GetChild(i).transform.position;
			if (position == new Vector2(positionArbre.x, positionArbre.y))
				return parentArbres.GetChild(i).gameObject;
		}
        return null;
	}

	public TypeTuile tuileSurPosition(Vector2 position){
		return tuilesMap[(int) position.y, (int) position.x];
	}
}
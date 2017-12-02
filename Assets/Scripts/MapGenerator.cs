using System;
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

	[Header("Génération")]
	//Données de la génération
	public int largeur;
	public int hauteur;
	public Count nombreDeMurs;
	public Count nombreDeRivieres;
	
	[Space]
	[Header("Tuiles")]
	//Tuiles utilisées
	public float tailleTuile;
	public GameObject[] tuilesSol;
	public GameObject[] tuilesMur;
	public GameObject[] tuilesBordure;
	public GameObject[] tuilesEau;

	//Parent des tuiles
	private Transform parentTuiles;

	private void Start() {
		parentTuiles = new GameObject("Parent des tuiles").transform;
		
		genererMap();
	}

	public void genererMap() {
		for (int y = 0; y < hauteur; y++) {
			for (int x = 0; x < largeur; x++) {
				GameObject tuileSol = (GameObject) Instantiate(tuilesSol[Random.Range(0, tuilesSol.Length)]);
				tuileSol.transform.position = new Vector3(x * tailleTuile, y * tailleTuile);
				tuileSol.transform.parent = parentTuiles;
			}
		}
	}
}

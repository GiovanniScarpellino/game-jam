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
	public int seed;
	public float scale;

	[Space]
	[Header("Tuiles")]
	//Tuiles utilisées
	public float tailleTuile;
	public GameObject prefabTerre;
	public GameObject prefabHerbe;
	public GameObject prefabEau;

	//Parent des tuiles
	private Transform parentTuiles;

	private void Start() { //Utilise Update pour une mise a jour en temps réel
		if(parentTuiles != null)
			Destroy(parentTuiles.gameObject);
		parentTuiles = new GameObject("Parent des tuiles").transform;
		
		genererMap();
	}

	public void genererMap() {
		for (int y = 0; y < hauteur; y++) {
			for (int x = 0; x < largeur; x++) {
				float xCoord = ((float) x + seed) / largeur * scale ;
				float yCoord = ((float) y + seed) / hauteur * scale;
				float valeurPerlin = Mathf.PerlinNoise(xCoord, yCoord);
				GameObject nouvelleTuile;
				if(valeurPerlin < .2f)
					nouvelleTuile = Instantiate(prefabEau);
				else if (valeurPerlin < .45f)
					nouvelleTuile = Instantiate(prefabTerre);
				else
					nouvelleTuile = Instantiate(prefabHerbe);
				nouvelleTuile.transform.position = new Vector3(x * tailleTuile, y * tailleTuile);
				nouvelleTuile.transform.parent = parentTuiles;
			}
		}
	}
}
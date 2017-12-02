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
	public GameObject prefabArbre;

	//Parent des tuiles
	private Transform parentSol;
	private Transform parentArbres;

	private void Start() { //Utilise Update pour une mise a jour en temps réel
		if(parentSol != null)
			Destroy(parentSol.gameObject);
		parentSol = new GameObject("Parent du sol").transform;
		if(parentArbres != null)
			Destroy(parentArbres.gameObject);
		parentArbres = new GameObject("Parent des arbres").transform;

		seed = Random.Range(0, 999999);
		
		Random.InitState(seed);
		
		genererMap();
	}

	public void genererMap() {
		for (int y = 0; y < hauteur; y++) {
			for (int x = 0; x < largeur; x++) {
				//Valeurs pour la génération
				float valeurMaxEau = .35f;
				float valeurMaxTerre = .55f;
				
				//Récupération de la valeur aléatoire
				float xCoord = ((float) x + seed) / largeur * scale ;
				float yCoord = ((float) y + seed) / hauteur * scale;
				float valeurPerlin = Mathf.PerlinNoise(xCoord, yCoord);
				
				//Instantiation de la tuile du sol
				GameObject nouvelleTuile;
				if(valeurPerlin < valeurMaxEau)
					nouvelleTuile = Instantiate(prefabEau);
				else if (valeurPerlin < valeurMaxTerre)
					nouvelleTuile = Instantiate(prefabTerre);
				else
					nouvelleTuile = Instantiate(prefabHerbe);
				nouvelleTuile.transform.position = new Vector3(x * tailleTuile, y * tailleTuile);
				nouvelleTuile.transform.parent = parentSol;
				
				//Instantiation des arbres
				if (valeurPerlin >= valeurMaxEau) {
					float valeurMaxArbre = .3f;
					if (Random.Range(0f, 1f) < valeurMaxArbre) {
						GameObject nouvelArbre = Instantiate(prefabArbre);
						nouvelArbre.transform.position = new Vector3(x * tailleTuile, y * tailleTuile);
						nouvelArbre.transform.parent = parentArbres;
					}
				}
			}
		}
	}
}
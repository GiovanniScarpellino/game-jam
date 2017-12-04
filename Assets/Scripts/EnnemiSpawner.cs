using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiSpawner : MonoBehaviour {

	public GameObject prefabCampEnnemi;
	public GameObject prefabOrc;
	
	private GameObject campAllie;
	private GameObject campOrc;

	private GameObject parentOrcs;

	private bool debutVaguesEnnemie;
	private bool spawnEnnemis;

	public float tempsAvantDebutVagues;
	public float frequenceSpawnEnnemi;
	public float decrementationFrequenceSpawnEnnemi;
	public float frequenceMinimumSpawnEnnemis;
	private float compteurSpawnEnnemi;

	public void Start() {
		debutVaguesEnnemie = false;
		spawnEnnemis = false;
		
		compteurSpawnEnnemi = 0;
		
		parentOrcs = new GameObject("Parent des orcs");
	}

	private void Update() {
		//Compteur avant debut vagues ennemies
		if (debutVaguesEnnemie) {
			compteurSpawnEnnemi += Time.deltaTime;
			if (compteurSpawnEnnemi >= tempsAvantDebutVagues) {
				compteurSpawnEnnemi = 0;
				spawnEnnemis = true;
				debutVaguesEnnemie = false;
			}
		}
		//Spawn des ennemis
		if (spawnEnnemis) {
			compteurSpawnEnnemi += Time.deltaTime;
			if (compteurSpawnEnnemi >= frequenceSpawnEnnemi) {
				//Remise à zéro du compteur
				compteurSpawnEnnemi = 0;
				//La fréquence de spawn des monstres augmente
				if (frequenceSpawnEnnemi - decrementationFrequenceSpawnEnnemi <= frequenceMinimumSpawnEnnemis)
					frequenceSpawnEnnemi = frequenceMinimumSpawnEnnemis;
				else
					frequenceSpawnEnnemi -= decrementationFrequenceSpawnEnnemi;
				//Spawn des ennemis
				GameObject nouvelOrc = Instantiate(prefabOrc);
				nouvelOrc.transform.position = campOrc.transform.position;
				nouvelOrc.transform.parent = parentOrcs.transform;
				nouvelOrc.GetComponent<EnnemiController>().vitesse = 2.35f;
				nouvelOrc.GetComponent<EnnemiController>().trouverCheminOrc(campOrc.transform.position, campAllie.transform.position);
			}
		}
	}

	public void spawnCampEnnemi(GameObject _campAllie) {
		campAllie = _campAllie;

		oPathFinding pathFinding = GetComponent<oPathFinding>();
		MapGenerator mapGenerator = GetComponent<MapGenerator>();
		
		Vector2 positionCamp = new Vector2(campAllie.transform.position.x, campAllie.transform.position.y);

		Vector2 meilleurePosition = new Vector2();
		int longueurMeilleurePosition = 0;
		foreach (oNoeud noeud in pathFinding.grid.grid) {
			//On récupère la position du noeud testé
			Vector2 positionNoeud = new Vector2(noeud.gridX, noeud.gridY);

			//On regarde si il y a rien en 3x3 autour de la position testée
			bool positionValide = true;
			for (int y = -1; y <= 1; y++) {
				for (int x = -1; x <= 1; x++) {
					if (y == -1 || y == 1 || x == -1 || x == 1) {
						Vector2 positionValidationTeste = new Vector2(positionNoeud.x + x, positionNoeud.y + y);
						if (positionValidationTeste.x < 0 || positionValidationTeste.x >= mapGenerator.largeur ||
							positionValidationTeste.y < 0 || positionValidationTeste.y >= mapGenerator.hauteur ||
							mapGenerator.tuileSurPosition(positionValidationTeste) == MapGenerator.TypeTuile.Eau)
							positionValide = false;
					}
				}
			}
			
			//On regarde si le chemin vers cette position est plus loin que le chemin sauvegardé
			if (positionValide) {
				List<Vector2> cheminTeste = pathFinding.FindPath(positionCamp, positionNoeud, false);
				if (cheminTeste.Count > longueurMeilleurePosition) {
					meilleurePosition = positionNoeud;
					longueurMeilleurePosition = cheminTeste.Count;
				}
			}
		}

		//Instantiation du camp ennemi
		campOrc = Instantiate(prefabCampEnnemi);
		campOrc.transform.position = meilleurePosition;
		
		//Suppresion des arbres alentours sur la grille du path finding
		for (int i = -2; i <= 2; i++) {
			for (int j = -2; j <= 2; j++) {
				Vector2 positionVerification = new Vector2(campOrc.transform.position.x + i, campOrc.transform.position.y + j);
				//Bordures autour du camp
				if (i == -2 || i == 2 || j == -2 || j == 2) {
					if (mapGenerator.arbreSurPosition(positionVerification))
						pathFinding.definirNoeudArbre(positionVerification, false);
				}
				else { //Intérieur du camp
					pathFinding.definirNoeudArbre(positionVerification, true);
				}	
			}
		}
		
		//Debut de l'instantiation des ennemis
		debutVaguesEnnemie = true;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiSpawner : MonoBehaviour {

	public GameObject uniteBlanche;
	private GameObject camp;
	
	public void spawnCampEnnemi(GameObject _camp) {
		camp = _camp;

		oPathFinding pathFinding = GetComponent<oPathFinding>();
		MapGenerator mapGenerator = GetComponent<MapGenerator>();
		
		Vector2 positionCamp = new Vector2(camp.transform.position.x, camp.transform.position.y);

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

		Instantiate(uniteBlanche).transform.position = meilleurePosition;
		print(meilleurePosition);
	}
}

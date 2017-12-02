using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.VR.WSA;

public class PathFinding : MonoBehaviour {
	
	Grid grille;//notre objet grille qui va contenir notre grille de tuiles représentant notre monde
	public Transform depart;
	private Transform arrivee;

	void Awake() {
		grille = GetComponent<Grid>();
	}		
	
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			Vector3  mouseInScreen = Input.mousePosition;
			Vector3  mouseInWorld = Camera.main.ScreenToWorldPoint(mouseInScreen);
			for (int x = 0; x < grille.dimensionMonde.x; x++) {
				for (int y = 0; y < grille.dimensionMonde.y; y++) {
					if (mouseInWorld.x >= grille.getGrille()[x,y].position.x - grille.rayonNoeud &&
					    mouseInWorld.x <= grille.getGrille()[x,y].position.x + grille.rayonNoeud &&
					    mouseInWorld.y >= grille.getGrille()[x,y].position.y - grille.rayonNoeud &&
					    mouseInWorld.y <= grille.getGrille()[x,y].position.y + grille.rayonNoeud) {
						Debug.Log(grille.getGrille()[x,y].position);
						trouverChemin(depart.position,grille.getGrille()[x,y].position);
					}
				}
			}
		}
	}

	public void trouverChemin(Vector3 startPos, Vector3 targetPos)
	{//on trouve les noeuds associés à nos positions
		Noeud noeudDepart = grille.noeudVsPoint (startPos);
		Noeud noeudArrivee = grille.noeudVsPoint (targetPos);

		List<Noeud> openList = new List<Noeud> ();
		List<Noeud> closedList = new List<Noeud> ();

		openList.Add (noeudDepart);

		while (openList.Count > 0) //tant qu'il nous reste des noeuds à évaluer
		{
			Noeud noeudCourant = openList [0];// on prend le premier noeud de la liste

			for (int i = 1; i < openList.Count; i++) //s'il y a plus d'un noeud dans la liste, on détermine celui au coût le plus bas
			{
				int fCost = openList [i].fCost();
				int hCost = openList [i].hCost;

				if (fCost < noeudCourant.fCost() || (fCost == noeudCourant.fCost() && hCost < noeudCourant.hCost))
					noeudCourant = openList[i];
			}

			openList.Remove (noeudCourant);//retire le noeud de la liste a évaluer
			closedList.Add (noeudCourant);//on le rajoute dans la liste de ceux déjà évalué

			if (noeudCourant == noeudArrivee) //si nous avons trouvé la tuile d'arrivée 
			{
				tracerChemin (noeudDepart, noeudArrivee);//on trace le chemin

				return; //on termine la fonction
			}
			else 
			{
				List<Noeud> voisins = grille.retourneVoisins (noeudCourant);//on trouve les voisins de notre noeud

				foreach (Noeud voisin in voisins) 
				{
					if (!voisin.walkable || closedList.Contains (voisin))//s'il n'est pas marchable ou s'il est déjà dans la closed list
						continue;
					//recalculer le coût de ce noeud
					int nouveauGCost = noeudCourant.gCost + getDistance (noeudCourant, voisin);
					//si notre nouveau calcul arrive à un coût plus bas, ou si c'est la première que l'on calcul son coût
					if (nouveauGCost < voisin.gCost || !openList.Contains (voisin)) 
					{//attribuer les coût à notre voisin
						voisin.gCost = nouveauGCost;
						voisin.hCost = getDistance (voisin, noeudArrivee);
						//conserver en mémoire qui est son parent
						voisin.parent = noeudCourant;

						if (!openList.Contains (voisin))//l'ajouter au besoin dans la open list
							openList.Add (voisin);
					}
				}				
			}
		}
	} 
	
	private void tracerChemin(Noeud depart, Noeud arrivee) {
		List<Noeud> chemin = new List<Noeud> ();
		Noeud noeudCourant = arrivee;//on place notre noeud courant sur la tuile d'arrivée

		while (noeudCourant.parent != depart) //on remonte la chaine de parent jusqu'à la tuile de départ
		{
			chemin.Add (noeudCourant);
			noeudCourant = noeudCourant.parent;
		}

		chemin.Add (noeudCourant);//on oublie pas d'ajouter la tuile de départ dans notre chemin

		chemin.Reverse (); //on inverse pour que le chemin commence à la tuile de départ

		grille.chemin = chemin; //on indique à l'objet grille quel est le chemin puisque c'est cet objet qui va dessiner la grille contenant le chemin
	}

	private int getDistance(Noeud noeudA, Noeud noeudB) {
		int distanceX = Mathf.Abs(noeudA.grilleX - noeudB.grilleX);
		int distanceY = Mathf.Abs(noeudA.grilleY - noeudB.grilleY);

		if (distanceX < distanceY) {
			return 14 * distanceY + 10 * (distanceX - distanceY);
		}

		return 14 * distanceX + 10 * (distanceY - distanceX);
	}

	public Grid getGrille() {
		return grille;
	}
}

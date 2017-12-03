using UnityEngine;
using System.Collections;
/// <summary>
/// Cette classe représente chacun des tuiles de notre monde
/// </summary>
public class Noeud{
	public bool walkable; 
	public Vector3 position; //position dans unity de notre tuile.
	public int gCost; //coût depuis la tuile de départ
	public int hCost; //coût estimé vers la tuile d'arrivée
	public int grilleX; //coordonnées en X et Y de notre tuile dans la grille de tuiles
	public int grilleY;
	public Noeud parent; //noeud parent utilisé dans l'algorithme A*

	public Noeud(bool walkable, Vector3 position, int grilleX, int grilleY)	{
		this.walkable = walkable;
		this.position = position;		
		this.grilleX = grilleX;
		this.grilleY = grilleY;
	}
	/// <summary>
	/// Retourne le coût total de notre tuile, le f cost.  C'est l'addition du gCost et du hCost.
	/// </summary>
	/// <returns>The cost.</returns>
	public int fCost(){
		return gCost + hCost;
	}
}

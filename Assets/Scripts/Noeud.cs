using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using UnityEngine;

public class Noeud
{
	public bool walkable; 
	public Vector3 position; //position dans unity de notre tuile.
	public int gCost; //coût depuis la tuile de départ
	public int hCost; //coût estimé vers la tuile d'arrivée
	public int grilleX; //coordonnées en X et Y de notre tuile dans la grille de tuiles
	public int grilleY;
	public Noeud parent; //noeud parent utilisé dans l'algorithme A*

	public Noeud(bool walkable, Vector3 position, int grilleX, int grilleY)
	{
		this.walkable = walkable;
		this.position = position;
		this.grilleX = grilleX;
		this.grilleY = grilleY;
	}


	public int fCost() {
		return gCost + hCost;
	}
}


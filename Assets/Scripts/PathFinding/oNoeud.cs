using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oNoeud {
	
	public bool walkable;
	public bool arbre;
	public int gridX;
	public int gridY;

	public int gCost;
	public int hCost;
	public oNoeud parent;
	
	public oNoeud(bool _walkable, bool _arbre, int _gridX, int _gridY) {
		walkable = _walkable;
		arbre = _arbre;
		gridX = _gridX;
		gridY = _gridY;
	}

	public int fCost {
		get {
			return gCost + hCost;
		}
	}
}
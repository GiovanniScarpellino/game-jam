using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oGrille : MonoBehaviour {
	public MapGenerator mapGenerator;
	public oNoeud[,] grid;

	public void CreateGrid() {
		grid = new oNoeud[mapGenerator.largeur,mapGenerator.hauteur];
		for (int x = 0; x < mapGenerator.largeur; x ++) {
			for (int y = 0; y < mapGenerator.hauteur; y ++) {
				bool walkable = mapGenerator.tuilesMap[y, x] != MapGenerator.TypeTuile.Eau;
				grid[x,y] = new oNoeud(walkable, x,y);
			}
		}
	}

	public List<oNoeud> GetNeighbours(oNoeud node) {
		List<oNoeud> neighbours = new List<oNoeud>();

		for (int x = -1; x <= 1; x++) {
			for (int y = -1; y <= 1; y++) {
				if (x == 0 && y == 0)
					continue;

				int checkX = node.gridX + x;
				int checkY = node.gridY + y;

				if (checkX >= 0 && checkX < mapGenerator.largeur && checkY >= 0 && checkY < mapGenerator.hauteur) {
					neighbours.Add(grid[checkX,checkY]);
				}
			}
		}

		return neighbours;
	}
	

	public oNoeud NodeFromWorldPoint(Vector2 position) {
		return grid[(int)position.x, (int)position.y];
	}
}
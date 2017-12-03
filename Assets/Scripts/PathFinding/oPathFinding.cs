using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oPathFinding : MonoBehaviour{
    oGrille grid;
    public GameObject uniteBlanche;

    void Awake(){
        grid = GetComponent<oGrille>();
    }

    public List<Vector2> FindPath(Vector2 startPos, Vector2 targetPos){
        oNoeud startNode = grid.NodeFromWorldPoint(startPos);
        oNoeud targetNode = grid.NodeFromWorldPoint(targetPos);

        List<oNoeud> openSet = new List<oNoeud>();
        HashSet<oNoeud> closedSet = new HashSet<oNoeud>();
        openSet.Add(startNode);

        while (openSet.Count > 0){
            oNoeud node = openSet[0];
            for (int i = 1; i < openSet.Count; i++){
                if (openSet[i].fCost < node.fCost || (openSet[i].fCost == node.fCost && openSet[i].hCost < node.hCost)){
                    if (openSet[i].hCost < node.hCost)
                        node = openSet[i];
                }
            }

            openSet.Remove(node);
            closedSet.Add(node);

            //Chemin trouvé
            if (node == targetNode){
                List<oNoeud> noeudsCheminFinal = RetracePath(startNode, targetNode);
                List<Vector2> cheminFinal = new List<Vector2>();
                foreach (oNoeud noeud in noeudsCheminFinal){
                    cheminFinal.Add(new Vector2(noeud.gridX, noeud.gridY));
                }
                return cheminFinal;
            }

            foreach (oNoeud neighbour in grid.GetNeighbours(node)){
                //Verification du type de case
                if (!neighbour.walkable || closedSet.Contains(neighbour)){
                    continue;
                }
                
                //Verification de la diagonale dans les murs
                Vector2 differencePositionsVoisinSoi = (new Vector2(neighbour.gridX, neighbour.gridY) - new Vector2(node.gridX, node.gridY));
                if (!grid.NodeFromWorldPoint(new Vector2(node.gridX + differencePositionsVoisinSoi.x, node.gridY)).walkable ||
                    !grid.NodeFromWorldPoint(new Vector2(node.gridX, node.gridY + differencePositionsVoisinSoi.y)).walkable)
                    continue;

                //Verification des voisins
                int newCostToNeighbour = node.gCost + GetDistance(node, neighbour);
                if (newCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour)){
                    neighbour.gCost = newCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, targetNode);
                    neighbour.parent = node;

                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                }
            }
        }
        return new List<Vector2>();
    }

    List<oNoeud> RetracePath(oNoeud startNode, oNoeud endNode){
        List<oNoeud> path = new List<oNoeud>();
        oNoeud currentNode = endNode;

        while (currentNode != startNode){
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();
        return path;
    }

    int GetDistance(oNoeud nodeA, oNoeud nodeB){
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (dstX > dstY)
            return 14 * dstY + 10 * (dstX - dstY);
        return 14 * dstX + 10 * (dstY - dstX);
    }
}
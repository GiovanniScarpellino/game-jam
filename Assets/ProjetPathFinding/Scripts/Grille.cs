using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;

/// <summary>
/// Cette classe va servir à diviser le monde en grille de tuiles.  Certaines de ces tuiles seront des obstacles (rouge), 
/// d'autres des tuiles "marchables" (blanche).  Enfin, deux de ces tuiles seront respectivement nos points de départ et d'arrivée (cyan)
/// Si un chemin est possible, les tuiles de ce chemin seront colorées en noires.  
/// 
/// Le script grille sera rattaché à notre GameObject A* qui est un gameobject tout simplement vide.
/// 
/// Pour notre monde, le plancher est un objet 3D plane qui est redressé de sorte à ne pas voir l'axe des Z.
/// Les obstacles sont des objets cube redressé pour les mêmes raisons.
/// Les cases de départ et d'arrivée sont des capsules.
/// </summary>
/// 
public class Grille : MonoBehaviour{
    private Noeud[,] grille; //notre monde sera une grille de noeud
    public Vector2 dimensionMonde;
    public float rayonNoeud; //le rayon d'une tuile
    public LayerMask impossibleMarcherMasque; //layer où se trouve les cubes représentant les obstacles
    private float diametreNoeud;
    private int dimensionGrilleX, dimensionGrilleY; //dimensions de notre grille en X et Y.
    public Transform player;
    public List<Noeud> chemin; //notre chemin déterminé par l,algorithme A*.  Il est public car on va le remplir à partir d'un autre script
    public bool afficherGrille;

    void Start(){
        construireGrille();
    }

    /// <summary>
    /// Cette méthode va construire notre grille de noeud 
    /// </summary>
    private void construireGrille(){
        diametreNoeud = rayonNoeud * 2;
        dimensionGrilleX = Mathf.RoundToInt(dimensionMonde.x / diametreNoeud);
        dimensionGrilleY = Mathf.RoundToInt(dimensionMonde.y / diametreNoeud);

        grille = new Noeud[dimensionGrilleX, dimensionGrilleY];

        Vector3 noeudBasGauche = transform.position - Vector3.right * dimensionMonde.x / 2 - Vector3.up * dimensionMonde.y / 2;

        for (int x = 0; x < dimensionGrilleX; x++){
            for (int y = 0; y < dimensionGrilleY; y++){
                Vector3 point = noeudBasGauche + Vector3.right * (x * diametreNoeud + rayonNoeud) + Vector3.up * (y * diametreNoeud + rayonNoeud);
                //la prochaine ligne vérifie si la tuile évaluée entre en collision avec un des cubes.  Si c'est le cas, la tuile ne sera pas marchable
                bool marchable = !(Physics.CheckSphere(point, rayonNoeud, impossibleMarcherMasque));
                //on créer un noeud que l'on place dans notre monde
                //notre noeud sait s'il est marchable, connait sa position à l'aide d'un vector3 et sait sa position par rapport à la grille
                grille[x, y] = new Noeud(marchable, point, x, y);
            }
        }
    }

    /// <summary>
    /// Retourne les noeuds voisins du noeud évalué.  Sera utilisé dans la classe PathFinding
    /// </summary>
    /// <returns>La liste des noeuds voisins à notre noeud évalué</returns>
    /// <param name="noeud">Le noeud évalué</param>
    public List<Noeud> retourneVoisins(Noeud noeud){
        List<Noeud> voisins = new List<Noeud>();

        for (int x = -1; x <= 1; x++){
            for (int y = -1; y <= 1; y++){
                if (x == 0 && y == 0) //si c'est le noeud concerné
                    continue;

                int checkX = noeud.grilleX + x;
                int checkY = noeud.grilleY + y;
                //si le noeud est à l'intérieur des dimension de la grille
                if (checkX >= 0 && checkX < dimensionGrilleX && checkY >= 0 && checkY < dimensionGrilleY){
                    voisins.Add(grille[checkX, checkY]);
                }
            }
        }

        return voisins;
    }

    /// <summary>
    /// Cette méthode reçoit une position dans le monde et retourne le noeud associé à cette position.
    /// Sera utile pour déterminer le noeud associé à la position de mon personnage entre autre.
    /// </summary>
    /// <returns>le noeud pointé par la position</returns>
    /// <param name="positionMonde">la position recherché dans notre monde</param>
    public Noeud noeudVsPoint(Vector3 positionMonde){
        float pourcentX = (positionMonde.x + dimensionMonde.x / 2) / dimensionMonde.x;
        float pourcentY = (positionMonde.y + dimensionMonde.y / 2) / dimensionMonde.y;

        pourcentX = Mathf.Clamp01(pourcentX);
        pourcentY = Mathf.Clamp01(pourcentY);

        int x = Mathf.RoundToInt((dimensionGrilleX - 1) * pourcentX);
        int y = Mathf.RoundToInt((dimensionGrilleY - 1) * pourcentY);

        return grille[x, y];
    }

    /// <summary>
    /// Méthode qui va dessiner la grille avec les bonnes couleurs incluant le chemin s'il en existe un.
    /// </summary>
    void OnDrawGizmos(){
        Gizmos.DrawWireCube(transform.position, new Vector3(dimensionMonde.x, dimensionMonde.y, 1));

        construireGrille(); //construction de la grille
        //pour que ça marche, il faut que le script PathFinding soit rattaché au même GameObject que le script Grille
        PathFinding pathFinding = GetComponent<PathFinding>();

        GameObject depart = GameObject.Find("Player"); //trouve le joueur qui est le point de départ
        GameObject arrivee = GameObject.Find("arrivee"); //trouve l'arrivée
        //on demande à la classe Pathfinding de trouver le chemin
        pathFinding.trouverCheminGizmos(depart.transform.position, arrivee.transform.position, this);

        if (grille != null && afficherGrille) //si la grille a bel et bien été créé
        {
            Noeud noeudPlayer = noeudVsPoint(player.position);

            foreach (Noeud n in grille) //pour tous les noeuds de la grille
            {
                if (n.walkable) //si le noeud est marchable
                {
                    if (noeudPlayer == n) //si c'est le joueur
                        Gizmos.color = Color.cyan;
                    else if (chemin != null && chemin.Contains(n)) //si le noeud fait partie du chemin trouvé
                        Gizmos.color = Color.black;
                    else //si ce n'est qu'une tuile marchable qui n'est pas dans le chemin trouvé
                        Gizmos.color = Color.white;
                } else //si c'est un obstacle
                    Gizmos.color = Color.red;
                //on dessine un cube à la position de notre noeud et de la bonne dimension.
                Gizmos.DrawCube(n.position, Vector3.one * (diametreNoeud - .1f));
            }
        }
    }
}
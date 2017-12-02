using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEditor; //nécessaire pour modifier l'éditeur de unity

[AddComponentMenu("Custom/Editor")] //va rattacher le script à un nouveau menu dans l'éditeur de unity

/// <summary>
/// Cette classe va servir a tracer une grille dans l'éditeur directement sur la scène.  cette grille nous servira de guide 
/// lors de l'édition de notre niveau de jeu.
/// </summary>
public class Grid : MonoBehaviour {
    private Noeud[,] grille; //notre monde sera une grille de noeud
    public float width = 1.0f;
    public float height = 1.0f;
    public Color color = Color.yellow; //couleur du trait de la grille
    public Vector2 dimensionMonde;
    private int dimensionGrilleX, dimensionGrilleY;
    private float diametreNoeud;
    public float rayonNoeud;
    public LayerMask impossibleMarcherMasque; //layer où se trouve les cubes représentant les obstacles
    public List<Noeud> chemin; //Le chemin déterminé par l'algorithme     A*

    private void Start() {
        diametreNoeud = rayonNoeud * 2;
        dimensionGrilleX = Mathf.RoundToInt(dimensionMonde.x / diametreNoeud);
        dimensionGrilleY = Mathf.RoundToInt(dimensionMonde.y / diametreNoeud);

        construireGrille();
    }

    private void construireGrille() {
        diametreNoeud = rayonNoeud * 2;
        dimensionGrilleX = Mathf.RoundToInt(dimensionMonde.x / diametreNoeud);
        dimensionGrilleY = Mathf.RoundToInt(dimensionMonde.y / diametreNoeud);

        grille = new Noeud[dimensionGrilleX, dimensionGrilleY];

        Vector3 noeudBasGauche = transform.position - Vector3.right * dimensionMonde.x / 2 -
                                 Vector3.up * dimensionMonde.y / 2;

        for (int x = 0; x < dimensionGrilleX; x++) {
            for (int y = 0; y < dimensionGrilleY; y++) {
                Vector3 point = noeudBasGauche + Vector3.right * (x * diametreNoeud + rayonNoeud) +
                                Vector3.up * (y * diametreNoeud + rayonNoeud);
                //la prochaine ligne vérifie si la tuile évaluée entre en collision avec un des cubes.  Si c'est le cas, la tuile ne sera pas marchable
                bool marchable = !(Physics.CheckSphere(point, rayonNoeud, impossibleMarcherMasque));
                //on créer un noeud que l'on place dans notre monde
                //notre noeud sait s'il est marchable, connait sa position à l'aide d'un vector3 et sait sa position par rapport à la grille
                grille[x, y] = new Noeud(marchable, point, x, y);
            }
        }
    }

    void OnDrawGizmos() //va nous permettre de dessiner sur la scène en mode édition
    {
        Vector3 pos = Camera.current.transform.position; //va chercher la position de la camera

        Gizmos.color = color; //indique a Gizmos de quelle couleur on va dessiner

        //va tracer 500 lignes en ce centrant sur la position de la camera
        for (float y = pos.y - 250.0f; y < pos.y + 250.0f; y += height) {
            Gizmos.DrawLine
            (
                new Vector3(-250.0f, Mathf.Floor(y / height) * height, 0.0f),
                new Vector3(250.0f, Mathf.Floor(y / height) * height, 0.0f)
            );
        }
        //va tracer 500 colonnes en ce centrant sur la position de la camera
        for (float x = pos.x - 250.0f; x < pos.x + 250.0f; x += width) {
            Gizmos.DrawLine
            (
                new Vector3(Mathf.Floor(x / width) * width, -250.0f, 0.0f),
                new Vector3(Mathf.Floor(x / width) * width, 250.0f, 0.0f)
            );
        }
    }

    public Noeud noeudVsPoint(Vector3 positionMonde) {
        float pourcentX = (positionMonde.x + dimensionMonde.x / 2) / dimensionMonde.x;
        float pourcentY = (positionMonde.y + dimensionMonde.y / 2) / dimensionMonde.y;

        pourcentX = Mathf.Clamp01(pourcentX);
        pourcentY = Mathf.Clamp01(pourcentY);

        int x = Mathf.RoundToInt((dimensionGrilleX - 1) * pourcentX);
        int y = Mathf.RoundToInt((dimensionGrilleY - 1) * pourcentY);

        return grille[x, y];
    }

    public List<Noeud> retourneVoisins(Noeud noeud) {
        List<Noeud> voisins = new List<Noeud>();

        for (int x = -1; x <= 1; x++) {
            for (int y = -1; y <= 1; y++) {
                if (x == 0 && y == 0) continue; // si c'est le noeud concerné

                int checkX = noeud.grilleX + x;
                int checkY = noeud.grilleY + y;

                if (checkX >= 0 && checkX < dimensionGrilleX && checkY >= 0 && checkY < dimensionGrilleY) {
                    voisins.Add(grille[checkX, checkY]);
                }
            }
        }
        return voisins;
    }

}
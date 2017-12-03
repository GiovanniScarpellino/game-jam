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
    public float width = 1.0f;
    public float height = 1.0f;
    public Color color = Color.yellow; //couleur du trait de la grille

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
}
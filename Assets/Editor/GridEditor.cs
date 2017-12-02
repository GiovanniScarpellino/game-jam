using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor; //c'est le package qui va nous permettre d'hériter de la classe Editor

[CustomEditor(typeof(Grid))] //spécifie que notre classe Editor sera basée sur la classe Grid

public class GridEditor : Editor //va hériter de Editor pour nous permettre de surcharger l'éditeur de Unity
{
    Grid grid;
    private bool keyPressed = false;

    public void OnEnable() //va activer le scipt quand on est en mode édition
    {
        grid = (Grid)target;

        SceneView.onSceneGUIDelegate += gridUpdate;
    }

    public void OnDisable() //va désactiver le scipt quand nous serons en run-time
    {
        SceneView.onSceneGUIDelegate -= gridUpdate;
    }

    public void gridUpdate(SceneView sceneView)
    {
        Event e = Event.current; //récupère l'événement en cours

        Ray rayon = Camera.current.ScreenPointToRay(
            new Vector3(e.mousePosition.x, -e.mousePosition.y + Camera.current.pixelHeight, 0.0f)
        );

        Vector3 mousePos = rayon.origin; //position de la souris

        //si on tape sur un bouton et que c'est le bouton ctrl
        if (e.ToString() == "Repaint" && e.control && !keyPressed)
        {
            keyPressed = true;

            GameObject obj;

            //va rÉcupérer la référence de l'objet prefab sélectionné
            Object prefab = PrefabUtility.GetPrefabParent(Selection.activeObject);

            obj = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
            //pour aligner notre prefab sur la grille en fonction de la position de la souris
            Vector3 aligned = new Vector3(
                                  Mathf.Floor(mousePos.x / grid.width) * grid.width + grid.width * .5f,
                                  Mathf.Floor(mousePos.y / grid.height) * grid.height + grid.height * .5f
                              );

            obj.transform.position = aligned;
        }
        else if (e.ToString() == "Repaint" && e.control && keyPressed)
            keyPressed = false;
    }
}

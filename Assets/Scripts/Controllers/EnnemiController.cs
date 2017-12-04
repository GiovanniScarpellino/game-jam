using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiController : Controllers {

    public Vector2 positionCampOrc;
    public Vector2 positionCampAllie;
    
    public List<Vector2> cheminOrc;

    public void trouverCheminOrc(Vector2 _positionCampOrc, Vector2 _positionCampAllie) {
        positionCampAllie = _positionCampAllie;
        positionCampOrc = _positionCampOrc;
        cheminOrc = GameObject.Find("MapGenerator").GetComponent<oPathFinding>().FindPath(_positionCampOrc, _positionCampAllie, false);
    }

    private void Update(){
        if (cheminOrc != null) {
            //TODO : BOUGER ORC
        }
    }
}
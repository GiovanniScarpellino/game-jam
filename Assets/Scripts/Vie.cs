using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vie : MonoBehaviour{
    public float vieMax;
    private float vieActuel{ get; set; }

    public RectTransform foreground;

    private void Start(){
        vieActuel = vieMax;
        perdreVie(1);
    }

    public void perdreVie(int dommage){
        vieActuel -= dommage;
        foreground.sizeDelta = new Vector2(vieActuel / vieMax * 300, foreground.sizeDelta.y);
    }
}
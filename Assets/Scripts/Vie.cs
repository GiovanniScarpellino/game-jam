using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vie : MonoBehaviour{
    public float vieMax;
    private float vieActuel;
    public bool doitAttendre{ set; private get; }
    private float tempsAttente;

    public RectTransform foreground;

    private void Start(){
        vieActuel = vieMax;
    }

    public void perdreVie(int dommage, float tempsAttente){
        if (doitAttendre){
            this.tempsAttente += Time.deltaTime;
            if (this.tempsAttente >= tempsAttente){
                vieActuel -= dommage;
                foreground.sizeDelta = new Vector2(vieActuel / vieMax * 300, foreground.sizeDelta.y);
                this.tempsAttente = 0;
            }
        } else{
            vieActuel -= dommage;
            foreground.sizeDelta = new Vector2(vieActuel / vieMax * 300, foreground.sizeDelta.y);
            doitAttendre = true;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void perdreVie(int dommage){
        vieActuel -= dommage;
        foreground.sizeDelta = new Vector2(vieActuel / vieMax * 300, foreground.sizeDelta.y);
        if (vieActuel == 0) Destroy(gameObject);
    } 
    
    public void perdreVie(int dommage, GameObject gameObject){
        vieActuel -= dommage;
        foreground.sizeDelta = new Vector2(vieActuel / vieMax * 300, foreground.sizeDelta.y);
        if (gameObject.CompareTag("Ennemi")){
            if(vieActuel == 0) SceneManager.LoadScene("FinDePartie", LoadSceneMode.Single);
        }
    }
}
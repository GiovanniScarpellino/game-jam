using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour {
    private int nombreDeBois;
    private IU uI;
    public static int score;

    private void Start(){
        score = 150;
        uI = Camera.main.GetComponent<IU>();
    }

    public void ajoutBois(){
        uI = Camera.main.GetComponent<IU>();
        nombreDeBois += 100;
        uI.mettreAJourBois(nombreDeBois);
    }
}

using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class IU : MonoBehaviour{
    public Text textNombreDeBois;
    public Text textNombreOr;
    public Text textScore;
    public GameObject miniMap;

    // Update is called once per frame
    void Update(){
        textScore.GetComponent<Text>().text = PlayerInfo.score + "";
        textNombreOr.GetComponent<Text>().text = PlayerInfo.or + "";
        miniMap.SetActive(Input.GetButton("Afficher carte"));
    }

    public void mettreAJourBois(int nombreBois){
        textNombreDeBois.text = nombreBois.ToString();
    }
}
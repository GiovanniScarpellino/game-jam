using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class IU : MonoBehaviour {
    public Text textNombreDeBois;
	private Text textNombreOr;
	private Text textScore;

	private void Start(){
		GameObject.Find("NombreOr");
		GameObject.Find("NombreBois");
	}

	// Update is called once per frame
	void Update (){
		textScore.GetComponent<Text>().text = PlayerInfo.score+"";
		textNombreOr.GetComponent<Text>().text = PlayerInfo.or+"";
	}

    public void mettreAJourBois(int nombreBois)
    {
        textNombreDeBois.text = nombreBois.ToString();
    }
}

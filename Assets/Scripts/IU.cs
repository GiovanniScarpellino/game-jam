using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IU : MonoBehaviour {
    public Text textNombreDeBois;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void mettreAJourBois(int nombreBois)
    {
        textNombreDeBois.text = nombreBois.ToString();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebutPartie : MonoBehaviour {

	public enum EtatDebutPartie {
		Dezoom,
		ChoixCamp,
		Zoom
	}

	private float startZoom;
	public float dezoomMax;
	
	private float etatDezoom;
	public float incrementationDezoom;

	private EtatDebutPartie etatDebutPartie;

	// Use this for initialization
	void Start () {
		startZoom = Camera.main.orthographicSize;
		etatDebutPartie = EtatDebutPartie.Dezoom;
		etatDezoom = -1;
	}
	
	// Update is called once per frame
	void Update () {
		switch (etatDebutPartie) {
				case EtatDebutPartie.Dezoom:
					float valeurIncrementationDezoom = sigmoid(etatDezoom);
					etatDezoom += incrementationDezoom * Time.deltaTime;
					if (etatDezoom >= 1) {
						Camera.main.orthographicSize = dezoomMax;
						etatDebutPartie = EtatDebutPartie.ChoixCamp;
					}
					else {
						Camera.main.orthographicSize = startZoom + ((dezoomMax - startZoom) * valeurIncrementationDezoom);
					}
					break;	
		}
	}

	//Fonction qui permet de calculer les animations de zoom
	float sigmoid(float x) {
		return 1 / (1 + Mathf.Exp(-5.85f * x));
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoserTourelle : MonoBehaviour {
	
	public bool joueurPoseTourelle;

	private GameObject tourelleAPoser;

	public GameObject prefabBalista;
	public GameObject prefabMur;

	private void Start() {
		joueurPoseTourelle = false;
	}

	private void Update() {
		//On regarde si il veut poser une tourelle
		if(Input.GetKeyDown(KeyCode.A))
			inverserJoueurPoseTourelle(prefabBalista);

		//On met la tourelle sur la souris
		if (joueurPoseTourelle) {
			Vector2 positionSouris = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			positionSouris.x = Mathf.Floor(positionSouris.x + 0.5f);
			positionSouris.y = Mathf.Floor(positionSouris.y + 0.5f);
			tourelleAPoser.transform.position = positionSouris;
			tourelleAPoser.GetComponent<TourelleController>().enabled = false;
		}
		
		//Si il pose la tourelle
		if (joueurPoseTourelle && Input.GetMouseButtonDown(0)) {
			poserTourelle();
		}
	}

	public void poserTourelle() {
		if (tourelleAPoser != null && joueurPoseTourelle && PlayerInfo.or >= 400) {
			PlayerInfo.or -= 400;
			tourelleAPoser.GetComponent<TourelleController>().enabled = true;
			joueurPoseTourelle = false;
		}
	}

	public void inverserJoueurPoseTourelle(GameObject tourelle) {
		//On regarde si ce n'est pas le debut de la partie
		if(GetComponent<DebutPartie>() == null)
			joueurPoseTourelle = !joueurPoseTourelle;

		//Si on ne pose plus de tourelle mais qu'on en posait
		if (!joueurPoseTourelle && tourelleAPoser != null) {
			Destroy(tourelleAPoser);
		}
		
		//Si on pose maintenant une tourelle
		if (joueurPoseTourelle) {
			tourelleAPoser = Instantiate(tourelle);
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinDePartie : MonoBehaviour {

	public Button quitter;

	// Use this for initialization
	void Start () {
		quitter.onClick.AddListener(RetourAuMenu);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void RetourAuMenu() {
		SceneManager.LoadScene("Menu",LoadSceneMode.Single);
	}
}

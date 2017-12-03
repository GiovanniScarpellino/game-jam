using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionCampSurArbre : MonoBehaviour {
	public void OnTriggerEnter2D(Collider2D collider) {
		if (collider.CompareTag("Camp")) {
			Destroy(gameObject);
			//Suppression des arbres sur le PathFinding
			GameObject mapManager = GameObject.Find("MapGenerator");
			if (mapManager != null) {
				oPathFinding pathFinding = mapManager.GetComponent<oPathFinding>();
				pathFinding.definirNoeudMarchable(new Vector2(transform.position.x, transform.position.y), true);
			}
		}
	}
}

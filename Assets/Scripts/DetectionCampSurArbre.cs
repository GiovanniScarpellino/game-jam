using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionCampSurArbre : MonoBehaviour {
	public void OnTriggerEnter2D(Collider2D collider) {
		if (collider.CompareTag("Camp")) {
			Destroy(gameObject);
		}
	}
}

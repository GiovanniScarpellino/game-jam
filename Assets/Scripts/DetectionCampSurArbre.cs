using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionCampSurArbre : MonoBehaviour {
	public void OnTriggerEnter2D(Collider2D collider) {
		if (collider.CompareTag("Camp")) {
			Destroy(gameObject);
		}
	}

	private void OnMouseEnter(){
		Camera.main.GetComponent<IconCursor>().afficherIconCursor(Icon.Hache);
	}

	private void OnMouseExit(){
		Camera.main.GetComponent<IconCursor>().effacerIconCursor();
	}
}

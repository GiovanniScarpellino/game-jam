using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public GameObject pathfinding;
	public float speed;
	private Vector2 mouseInWorld;
	private bool estArrive;
	
	// Use this for initialization
	void Start () {
		mouseInWorld = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			Vector3 mouseInScreen = Input.mousePosition;
			mouseInWorld = Camera.main.ScreenToWorldPoint(mouseInScreen);
			estArrive = false;
		} else if ((Vector2)transform.position == mouseInWorld){
			mouseInWorld = transform.position;
			estArrive = true;
		}
		if (!estArrive) {
			var step = speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards(transform.position, mouseInWorld, step);	
		}
		
	}
}

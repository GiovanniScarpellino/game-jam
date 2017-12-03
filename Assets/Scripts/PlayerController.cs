using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour{
    private GameObject mapGenerator;
    private Vector2 mousePosition;

    private void Start(){
        mapGenerator = GameObject.Find("MapGenerator");
    }

    private void Update(){
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.x = Mathf.Floor(mousePosition.x + 0.5f);
        mousePosition.y = Mathf.Floor(mousePosition.y + 0.5f);
        if (Input.GetMouseButtonDown(0)){
            
        }
    }
}
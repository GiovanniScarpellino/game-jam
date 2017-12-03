using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class CameraController : MonoBehaviour{

    public float vitesseCamera;
    public Vector2 offsetLimitesCamera;
    
    private GameObject gameManager;
    private GameObject mapGenerator;

    private void Start(){
        gameManager = GameObject.Find("GameManager");
        mapGenerator = GameObject.Find("MapGenerator");
    }

    private void Update(){
        var nombreTuilesX = mapGenerator.GetComponent<MapGenerator>().largeur;
        var nombreTuilesY = mapGenerator.GetComponent<MapGenerator>().hauteur;
        
        Vector2 positionSouris = Input.mousePosition;
        Vector3 positionCamera = Camera.main.transform.position;
        if (positionSouris.x <= 0 && Camera.main.ScreenToWorldPoint(Vector3.zero).x >= -offsetLimitesCamera.x)
            positionCamera.x -= vitesseCamera * Time.deltaTime;
        if (positionSouris.x >= Screen.width && Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height)).x <= nombreTuilesX - offsetLimitesCamera.x)
            positionCamera.x += vitesseCamera * Time.deltaTime;
        if (positionSouris.y <= 0 && Camera.main.ScreenToWorldPoint(Vector3.zero).y >= -offsetLimitesCamera.y)
            positionCamera.y -= vitesseCamera * Time.deltaTime;
        if (positionSouris.y >= Screen.height && Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height)).y <= nombreTuilesY - offsetLimitesCamera.y)
            positionCamera.y += vitesseCamera * Time.deltaTime;
        Camera.main.transform.position = positionCamera;
    }
}
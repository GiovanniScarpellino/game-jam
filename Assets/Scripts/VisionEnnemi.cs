﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionEnnemi : MonoBehaviour{
    private void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Player")){
            transform.parent.GetComponent<EnnemiController>().target = other.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other){
        if (other.CompareTag("Player")){
            transform.parent.GetComponent<EnnemiController>().target = transform.parent.GetComponent<EnnemiController>().camp;
        }
    }
}
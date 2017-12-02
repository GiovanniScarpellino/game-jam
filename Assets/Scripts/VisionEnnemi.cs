using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionEnnemi : MonoBehaviour{
    public float speed;
    public Transform target;

    private void Update(){
        var step = speed * Time.deltaTime;
        transform.parent.position = Vector3.MoveTowards(transform.parent.position, target.position, step);
    }

    private void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Player")){
            GetComponentInParent<Animator>().SetTrigger("DiagoBas");
            target = other.transform;
        }
    }
}
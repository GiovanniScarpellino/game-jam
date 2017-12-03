using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiController : Controllers{
    public float speed;
    public Transform camp;

    private void Start(){
        targetAnimation = camp.position;
    }

    private void Update(){
        var step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetAnimation, step);
    }
}
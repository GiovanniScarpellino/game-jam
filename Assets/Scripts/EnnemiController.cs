using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiController : MonoBehaviour{
    public float speed;
    public Transform target{ set; get; }
    public Transform camp;

    private void Start(){
        target = camp;
    }

    private void Update(){
        var step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }
}
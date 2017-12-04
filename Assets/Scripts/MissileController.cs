using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour{
    public float vitesse;
    public Vector2 position{ set; private get; }
    public int dommage = 1;

    private void Start(){
        var dir = position - (Vector2) transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void Update(){
        var step = vitesse * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, position, step);
    }

    private void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Ennemi")){
            other.GetComponent<Vie>().perdreVie(dommage);
            Destroy(gameObject);
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour{
    private Animator animator;
    private Controllers controllers;
    public string triggerActuel{ set; private get; }

    private void Start(){
        animator = GetComponent<Animator>();
        triggerActuel = "Idle";
        if (GetComponent<EnnemiController>() != null){
            controllers = GetComponent<EnnemiController>();
        } else{
            controllers = GetComponent<PlayerController>();
        }
    }

    private void Update(){
        var vecteurNormalized = (controllers.targetAnimation - (Vector2) transform.position).normalized;
        print(vecteurNormalized);
        if (vecteurNormalized.x == 1){
            triggerActuel = "Droite";
            GetComponent<SpriteRenderer>().flipX = false;
            animator.SetTrigger("Droite");
        } else if (vecteurNormalized.x == -1){
            triggerActuel = "Gauche";
            GetComponent<SpriteRenderer>().flipX = true;
            animator.SetTrigger("Droite");
        } else if (vecteurNormalized.y == 1){
            triggerActuel = "Haut";
            animator.SetTrigger("Haut");
        } else if (vecteurNormalized.y == -1){
            triggerActuel = "Bas";
            animator.SetTrigger("Bas");
        }
    }
}
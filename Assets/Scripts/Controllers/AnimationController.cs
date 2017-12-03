using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour{
    private Animator animator;
    private Controllers controllers;
    private string triggerActuel;

    private void Start(){
        animator = GetComponent<Animator>();
        triggerActuel = "Droite";
        if (GetComponent<EnnemiController>() != null){
            controllers = GetComponent<EnnemiController>();
        } else{
            controllers = GetComponent<PlayerController>();
        }
    }

    private void Update(){
        var vecteurNormalized = (controllers.targetAnimation - (Vector2) transform.position).normalized;
        var angle = Vector2.Angle(Vector2.right, vecteurNormalized);
        if (vecteurNormalized.y > 0){
            if (angle > 0 && angle <= 45 && triggerActuel != "Droite"){
                triggerActuel = "Droite";
                GetComponent<SpriteRenderer>().flipX = false;
                animator.SetTrigger("Droite");
            } else if (angle > 45 && angle <= 135 && triggerActuel != "Haut"){
                triggerActuel = "Haut";
                animator.SetTrigger("Haut");
            } else if (angle > 135 && angle <= 180 && triggerActuel != "Gauche"){
                triggerActuel = "Gauche";
                GetComponent<SpriteRenderer>().flipX = true;
                animator.SetTrigger("Droite");
            }
        } else{
            if (angle > 0 && angle <= 45 && triggerActuel != "Droite"){
                triggerActuel = "Droite";
                GetComponent<SpriteRenderer>().flipX = false;
                animator.SetTrigger("Droite");
            } else if (angle > 45 && angle <= 135 && triggerActuel != "Bas"){
                triggerActuel = "Bas";
                GetComponent<SpriteRenderer>().flipX = false;
                animator.SetTrigger("Bas");
            } else if (angle > 135 && angle <= 180 && triggerActuel != "Gauche"){
                triggerActuel = "Gauche";
                GetComponent<SpriteRenderer>().flipX = true;
                animator.SetTrigger("Droite");
            }
        }
    }
}
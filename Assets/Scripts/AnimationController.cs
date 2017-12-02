using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour{
    private Animator animator;
    private EnnemiController ennemiController;
    private string triggerActuel;

    private void Start(){
        animator = GetComponent<Animator>();
        triggerActuel = "Droite";
        ennemiController = GetComponent<EnnemiController>();
    }

    private void Update(){
        Vector2 vecteurNormalized = (ennemiController.target.position - transform.position).normalized;
        float angle = Vector2.Angle(Vector2.right, vecteurNormalized);
        if (vecteurNormalized.y > 0){
            if (angle > 0 && angle < 45 && triggerActuel != "Droite"){
                triggerActuel = "Droite";
                GetComponent<SpriteRenderer>().flipX = false;
                animator.SetTrigger("Droite");
            } else if (angle > 45 && angle < 135 && triggerActuel != "Haut"){
                triggerActuel = "Haut";
                animator.SetTrigger("Haut");
            } else if (angle > 135 && angle < 180 && triggerActuel != "Gauche"){
                triggerActuel = "Gauche";
                GetComponent<SpriteRenderer>().flipX = true;
                animator.SetTrigger("Droite");
            }
        } else{
            if (angle > 0 && angle < 45 && triggerActuel != "Droite"){
                triggerActuel = "Droite";
                GetComponent<SpriteRenderer>().flipX = false;
                animator.SetTrigger("Droite");
            }else if (angle > 45 && angle < 135 && triggerActuel != "Bas"){
                triggerActuel = "Bas";
                GetComponent<SpriteRenderer>().flipX = false;
                animator.SetTrigger("Bas");
            }else if (angle > 135 && angle < 180 && triggerActuel != "Gauche"){
                triggerActuel = "Gauche";
                GetComponent<SpriteRenderer>().flipX = true;
                animator.SetTrigger("Droite");
            }
        }
    }
}
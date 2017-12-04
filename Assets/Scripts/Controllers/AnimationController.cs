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
        if (vecteurNormalized.x == 1 && triggerActuel != "Droite"){
            triggerActuel = "Droite";
            GetComponent<SpriteRenderer>().flipX = false;
            animator.SetTrigger("Droite");
        } else if (vecteurNormalized.x == -1 && triggerActuel != "Gauche"){
            triggerActuel = "Gauche";
            GetComponent<SpriteRenderer>().flipX = true;
            animator.SetTrigger("Droite");
        } else if (vecteurNormalized.y == 1 && triggerActuel != "Haut"){
            triggerActuel = "Haut";
            GetComponent<SpriteRenderer>().flipX = false;
            animator.SetTrigger("Haut");
        } else if (vecteurNormalized.y == -1 && triggerActuel != "Bas"){
            triggerActuel = "Bas";
            GetComponent<SpriteRenderer>().flipX = false;
            animator.SetTrigger("Bas");
        } else if (vecteurNormalized.x.ToString("#.#") == ".7" && vecteurNormalized.y.ToString("#.#") == ".7" && triggerActuel != "DiagoHautD"){
            triggerActuel = "DiagoHautD";
            GetComponent<SpriteRenderer>().flipX = false;
            animator.SetTrigger("DiagoHaut");
        } else if (vecteurNormalized.x.ToString("#.#") == "-.7" && vecteurNormalized.y.ToString("#.#") == "-.7" && triggerActuel != "DiagoBasG"){
            triggerActuel = "DiagoBasG";
            GetComponent<SpriteRenderer>().flipX = true;
            animator.SetTrigger("DiagoBas");
        } else if (vecteurNormalized.x.ToString("#.#") == "-.7" && vecteurNormalized.y.ToString("#.#") == ".7" && triggerActuel != "DiagoHautG"){
            triggerActuel = "DiagoHautG";
            GetComponent<SpriteRenderer>().flipX = true;
            animator.SetTrigger("DiagoHaut");
        } else if (vecteurNormalized.x.ToString("#.#") == ".7" && vecteurNormalized.y.ToString("#.#") == "-.7" && triggerActuel != "DiagoBasD"){
            triggerActuel = "DiagoBasD";
            GetComponent<SpriteRenderer>().flipX = false;
            animator.SetTrigger("DiagoBas");
        }
    }
}
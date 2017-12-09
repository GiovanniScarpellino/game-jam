using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour{
    public float angle{ set; private get; }
    public bool enDeplacement{ set; private get; }
    private Animator animator;

    private enum CurrentAnimation{
        Droite,
        Gauche,
        Haut,
        Bas,
        DiagoHD,
        DiagoHG,
        DiagoBG,
        DiagoBD,
        Idle
    }

    private CurrentAnimation currentAnimation;

    private void Start(){
        angle = 0;
        animator = GetComponent<Animator>();
        currentAnimation = CurrentAnimation.Idle;
    }
    
    private void Update(){
        if (enDeplacement){
            if (angle > -10 && angle < 10 && currentAnimation != CurrentAnimation.Haut){
                currentAnimation = CurrentAnimation.Haut;
                animator.SetTrigger(currentAnimation + "");
            } else if (angle > -55 && angle < -35 && currentAnimation != CurrentAnimation.DiagoHD){
                currentAnimation = CurrentAnimation.DiagoHD;
                animator.SetTrigger(currentAnimation + "");
            } else if (angle > -100 && angle < -80 && currentAnimation != CurrentAnimation.Droite){
                currentAnimation = CurrentAnimation.Droite;
                animator.SetTrigger(currentAnimation + "");
            } else if (angle > -145 && angle < -125 && currentAnimation != CurrentAnimation.DiagoBD){
                currentAnimation = CurrentAnimation.DiagoBD;
                animator.SetTrigger(currentAnimation + "");
            } else if (angle > -190 && angle < -170 && currentAnimation != CurrentAnimation.Bas){
                currentAnimation = CurrentAnimation.Bas;
                animator.SetTrigger(currentAnimation + "");
            } else if (angle > -235 && angle < -215 && currentAnimation != CurrentAnimation.DiagoBG){
                currentAnimation = CurrentAnimation.DiagoBG;
                animator.SetTrigger(currentAnimation + "");
            } else if (angle > 80 && angle < 100 && currentAnimation != CurrentAnimation.Gauche){
                currentAnimation = CurrentAnimation.Gauche;
                animator.SetTrigger(currentAnimation + "");
            } else if (angle > 35 && angle < 55 && currentAnimation != CurrentAnimation.DiagoHG){
                currentAnimation = CurrentAnimation.DiagoHG;
                animator.SetTrigger(currentAnimation + "");
            }
        } else{
            animator.SetTrigger(CurrentAnimation.Idle + "");
        }
    }
}
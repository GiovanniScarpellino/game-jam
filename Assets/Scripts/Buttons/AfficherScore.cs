using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AfficherScore : MonoBehaviour{
    private void Start(){
        GetComponent<Text>().text = "Score : " + PlayerInfo.score + "";
    }
}
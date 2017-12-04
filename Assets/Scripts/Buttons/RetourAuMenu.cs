using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetourAuMenu : MonoBehaviour{
    public void retourAuMenu(){
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
}
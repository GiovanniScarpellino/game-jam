using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour{
    public void quitter(){
        Application.Quit();
    }


    public void jouer(){
        SceneManager.LoadScene("MapAleatoire", LoadSceneMode.Single);
    }
}
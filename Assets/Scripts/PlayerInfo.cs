using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour {
    private int nombreDeBois;
    private IU uI;

    void start()
    {
        uI = Camera.main.GetComponent<IU>();
        print("UI" + uI);
    }

    public void ajoutBois()
    {
        uI = Camera.main.GetComponent<IU>();
        nombreDeBois += 100;
        uI.mettreAJourBois(nombreDeBois);
    }
}

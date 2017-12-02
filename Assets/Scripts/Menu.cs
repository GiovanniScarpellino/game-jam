using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour {
    public Button bouttonJouer;
    public Button boutonQuitter;

	public void quitter()
    {
        Application.Quit();
    }

    public void jouer()
    {
        bouttonJouer.gameObject.SetActive(false);
        boutonQuitter.gameObject.SetActive(false);
    }
}

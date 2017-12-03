using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CouperLesArbres : MonoBehaviour {
    public GameObject arbre;
    public MapGenerator mapGenerator;

    private Vector3 vecteurDistance;
    private float distance;

	// Use this for initialization
	void Start () {
        mapGenerator = GameObject.FindGameObjectWithTag("MapGenerator").GetComponent<MapGenerator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(1))
        {
            Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouse.x = Mathf.Floor(mouse.x + 0.5f);
            mouse.y = Mathf.Floor(mouse.y + 0.5f);
            print(mapGenerator.arbreSurPosition(mouse));
            if (mapGenerator.arbreSurPosition(mouse))
            {
                print("J'ai touché");
                distance = 0f;
                vecteurDistance = this.gameObject.transform.position - new Vector3(mouse.x, mouse.y, 0);
                distance = vecteurDistance.magnitude;
                distance = Mathf.Floor(distance - 0.5f);
                print("distance : " + distance);
                if (distance <= 1)
                {
                    print(distance);
                }
            }
        }
    }
}

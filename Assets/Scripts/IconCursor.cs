using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Icon{
    Hache,
    Epe
}

public class IconCursor : MonoBehaviour{
    public GameObject prefab;
    public Sprite hacheSprite;

    private GameObject iconCursor;

    public void afficherIconCursor(Icon icon){
        switch (icon){
            case Icon.Hache:
                iconCursor = Instantiate(prefab, transform.position, Quaternion.identity);
                iconCursor.GetComponent<SpriteRenderer>().sprite = hacheSprite;
                break;
        }
    }

    public void effacerIconCursor(){
        Destroy(iconCursor);
    }

    private void Update(){
        if (iconCursor != null){
            iconCursor.transform.position = new Vector2(
                Camera.main.ScreenToWorldPoint(Input.mousePosition).x + .5f,
                Camera.main.ScreenToWorldPoint(Input.mousePosition).y - .5f);
        }
    }
}
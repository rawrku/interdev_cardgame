using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public Sprite faceSprite;

    Sprite backSprite;

    SpriteRenderer myRenderer;

    public CardGameManager gameMan;

    private void Start()
    {
        myRenderer = GetComponent<SpriteRenderer>();
        gameMan = GetComponent<CardGameManager>();

        backSprite = myRenderer.sprite;

    }

    public void FlipCards()
    {
        myRenderer.sprite = faceSprite;
    }

    private void Update()
    {

    }

    //public void OnMouseOver()
    //{
    //    Vector3 hover = myRenderer.transform.position;
    //    hover.y = hover.y + 5f;
       
    //}

    //public void OnMouseExit()
    //{
    //    Vector3 hover = myRenderer.transform.position;
    //    hover.y = hover.y - 5f;
    //}
}

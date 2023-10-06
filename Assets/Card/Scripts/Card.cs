using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public Sprite faceSprite;

    Sprite backSprite;

    SpriteRenderer myRenderer;

    bool mouseOver = false;

    public CardGameManager gameMan;

    private void Start()
    {
        myRenderer = GetComponent<SpriteRenderer>();
        gameMan = GetComponent<CardGameManager>();

        backSprite = myRenderer.sprite;
    }

    public void FlipCards()
    {
        //for (var i = 0; i < gameMan.playerHand.Count; i++)
        //{
        //    gameMan.playerHand[i].myRenderer = faceSprite;
        //}
        myRenderer.sprite = faceSprite;




    }

    private void Update()
    {
        //if (mouseOver)
        //{
          
        //}
    }

    //void OnMouseDown()
    //{
    //    mouseOver = true;
    //}
}

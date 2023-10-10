using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public Sprite faceSprite;

    Sprite backSprite;
 
    SpriteRenderer myRenderer;

    public CardGameManager gameMan;

    public DeckManager deckMan;

    private Vector3 targetPos;
    float moveSpeed = 0.05f;

    public bool hovered;
    public bool picked;

    public enum CardValues
    {
        ROCK,
        PAPER,
        SCISSORS
    }

    public CardValues cardValue;


    private void Start()
    {
        myRenderer = GetComponent<SpriteRenderer>();
        gameMan = GetComponent<CardGameManager>();
        deckMan = GetComponent<DeckManager>();


        backSprite = myRenderer.sprite;

        targetPos = transform.position;

    }



    public void FlipCards()
    {
        myRenderer.sprite = faceSprite;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed);

        if (cardValue == CardValues.ROCK)
        {
            for (int i = 0; i < deckMan.cardFaces.Length; i++)
            {
                if (deckMan.cardFaces[i].sprite == "rock")
                {
                    
                }
            }
        }
    }

    public void SetTargetPos(Vector3 newPos)
    {
        targetPos = newPos;
        targetPos.z = 0;
    }

    public void OnMouseEnter()
    {
        hovered = true;
    }

    public void OnMouseExit()
    {
        hovered = false;    
    }

    public void OnMouseDown()
    {
        picked = true;
    }
}

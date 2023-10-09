using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public Sprite faceSprite;

    Sprite backSprite;
 
    SpriteRenderer myRenderer;

    public CardGameManager gameMan;

    private Vector3 targetPos;
    float moveSpeed = 0.05f;

    public bool hovered;

    private void Start()
    {
        myRenderer = GetComponent<SpriteRenderer>();
        gameMan = GetComponent<CardGameManager>();

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
}

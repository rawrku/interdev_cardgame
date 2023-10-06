using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGameManager : MonoBehaviour
{

    public enum GameState
    {
        DEAL,
        CHOOSE,
        RESOLVE,
        DISCARD,
        RESHUFFLE
    }

    public static GameState state;

    //player hand vars
    public List<GameObject> playerHand = new List<GameObject>();
    public int playerHandCount;
    public Transform playerPos;

    //copmuter hand vars
    public List<GameObject> computerHand = new List<GameObject>();
    public int computerHandCount;
    public Transform computerPos;



    private void Start()
    {
        state = GameState.DEAL;
    }

    private void Update()
    {
        switch (state)
        {
            case GameState.DEAL:
                if (playerHand.Count < playerHandCount)
                {
                    DealCard();

                } else 
                {
                    state = GameState.CHOOSE;
                }
                break;
            case GameState.CHOOSE:
                break;
            case GameState.RESOLVE:
                break;
            case GameState.DISCARD:
                break;
            case GameState.RESHUFFLE:
                break;
        }
    }

    void DealCard()
    {

        //player cards
        GameObject nextCard = DeckManager.deck[DeckManager.deck.Count - 1];
        Vector3 newPos = playerPos.transform.position;
        newPos.x = newPos.x + (2f * playerHand.Count);
        nextCard.transform.position = newPos;
        playerHand.Add(nextCard);

        for (int i = 0; i < playerHand.Count; i++)
        {
            playerHand[i].GetComponent<Card>().FlipCards();
        }

        DeckManager.deck.Remove(nextCard);

        //copmuter cards
        GameObject compNextCard = DeckManager.deck[DeckManager.deck.Count - 1];
        Vector3 compNewPos = computerPos.transform.position;
        compNewPos.x = compNewPos.x + (2f * computerHand.Count);
        compNextCard.transform.position = compNewPos;
        computerHand.Add(compNextCard);
        DeckManager.deck.Remove(compNextCard);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGameManager : MonoBehaviour
{

    public enum GameState
    {
        COMPDEAL,
        PLAYERDEAL,
        COMPCHOOSE,
        PLAYERCHOOSE,
        RESOLVE,
        DISCARD,
        RESHUFFLE
    }

    public static GameState state;

    //player hand vars
    public List<GameObject> playerHand = new List<GameObject>();
    public int playerHandCount;
    public Transform playerPos;
    public Transform playerCard;
    GameObject playerPlayed;

    //copmuter hand vars
    public List<GameObject> computerHand = new List<GameObject>();
    public int computerHandCount;
    public Transform computerPos;
    public Transform compCard;
    GameObject compPlayed;

    int maxTimer = 20;
    int timer = 20;
    int revealTimer;
    float hoverAmount = 0.5f;

    private void Start()
    {
        state = GameState.COMPDEAL;
    }

    private void FixedUpdate()
    {

        switch (state)
        {
            case GameState.COMPDEAL:
                timer--;
                if (timer <= 0)
                {
                    if (computerHand.Count < computerHandCount)
                    {
                        CompDealCard();
                    }
                    else
                    {
                        state = GameState.PLAYERDEAL;
                    }

                   timer = maxTimer;
                }
                break;
            case GameState.PLAYERDEAL:
                timer--;
                revealTimer++;
                if (timer <= 0)
                {
                    if (playerHand.Count < playerHandCount)
                    {
                        PlayerDealCard();
                        timer = maxTimer;
                    }
                }
                if (revealTimer >= 70)
                {
                    for (int i = 0; i < playerHand.Count; i++)
                    {
                        playerHand[i].GetComponent<Card>().FlipCards();
                        state = GameState.COMPCHOOSE;
                    }
                    revealTimer = 0;
                }
                break;
            case GameState.COMPCHOOSE:
                timer--;
                if (timer <= -35)
                {
                    if (computerHand.Count == 3)
                    {
                        CompChooseCard();
                        state = GameState.PLAYERCHOOSE;
                    }
                    timer = maxTimer;
                }
                break;
            case GameState.PLAYERCHOOSE:
                foreach (GameObject card in playerHand)
                {
                    if (card.GetComponent<Card>().hovered)
                    {
                        card.GetComponent<Card>().SetTargetPos(new Vector3(card.transform.position.x, playerPos.position.y + hoverAmount));

                    } else
                    {
                        card.GetComponent<Card>().SetTargetPos(new Vector3(card.transform.position.x, playerPos.position.y));
                    }

                }
                break;
            case GameState.RESOLVE:
                break;
            case GameState.DISCARD:
                break;
            case GameState.RESHUFFLE:
                break;
        }
    }

    void CompDealCard()
    {
        //copmuter cards
        GameObject nextCard = DeckManager.deck[DeckManager.deck.Count - 1];
        Vector3 newPos = computerPos.transform.position;
        newPos.x = newPos.x + (2f * computerHand.Count);
        nextCard.GetComponent<Card>().SetTargetPos(newPos);
        computerHand.Add(nextCard);
        DeckManager.deck.Remove(nextCard);
    }
    void PlayerDealCard()
    {
        //player cards
        GameObject nextCard = DeckManager.deck[DeckManager.deck.Count - 1];
        Vector3 newPos = playerPos.transform.position;
        newPos.x = newPos.x + (2f * playerHand.Count);
        nextCard.GetComponent<Card>().SetTargetPos(newPos);
        playerHand.Add(nextCard);
        DeckManager.deck.Remove(nextCard);
    }

    void CompChooseCard()
    {
        GameObject randomCard = computerHand[Random.Range(0, 2)];
        Vector3 newPos = compCard.transform.position;
        randomCard.GetComponent<Card>().SetTargetPos(newPos);
        compPlayed = randomCard;
    }
}

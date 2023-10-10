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

    public Transform discardPile;
    public static List<GameObject> discardDeck = new List<GameObject>();

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

    //timer vars
    int maxTimer = 20;
    int timer = 20;
    int revealTimer;

    //hover
    float hoverAmount = 0.5f;

    //eval vars
    bool eval;
    public Score scoreMan;
    public Card card;

    private void Start()
    {
        state = GameState.COMPDEAL;

        scoreMan = GetComponent<Score>();
        card = GetComponent<Card>();
    }

    private void FixedUpdate()
    {
        // re-setting the eval varaible to false so it can eval again after a shuffle
        eval = false;
        switch (state)
        {
            case GameState.COMPDEAL:
                timer--;
                //after the set time has passed
                if (timer <= 0)
                {
                    //if the 3 or less cards in the opponents's hand
                    if (computerHand.Count < computerHandCount)
                    {
                        // run the function to deal the computer cards
                        CompDealCard();
                    }
                    else
                    {
                        // otherwise go to the next state
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

                for (int i = 0; i < playerHand.Count; i++)
                {
                    if (playerHand[i].GetComponent<Card>().hovered)
                    {
                        playerHand[i].GetComponent<Card>().SetTargetPos(new Vector3(playerHand[i].transform.position.x, playerPos.position.y + hoverAmount));

                    }
                    else
                    {
                        playerHand[i].GetComponent<Card>().SetTargetPos(new Vector3(playerHand[i].transform.position.x, playerPos.position.y));
                    }
                    if (playerHand[i].GetComponent<Card>().picked)
                    {
                        GameObject pickedCard = playerHand[i];
                        Vector3 newPos = playerCard.transform.position;
                        pickedCard.GetComponent<Card>().SetTargetPos(newPos);
                        playerPlayed = pickedCard;
                        state = GameState.RESOLVE;
                    }
                }
                break;
            case GameState.RESOLVE:
                timer--;
                if (timer <= -100)
                {
                    // check the comp hand of cards
                    for (int i = 0; i < computerHand.Count; i++)
                    {
                        // if the card in the comp hand was the played card
                        if (computerHand[i] == compPlayed)
                        {
                            // reveal the face
                            computerHand[i].GetComponent<Card>().FlipCards();

                            if (eval == false)
                            {
                                Evaluate();
                            }
                            eval = true;
                        }
                    }

                    timer = maxTimer;
                }
                break;
            case GameState.DISCARD:
                timer--;
                if (timer <= -85)
                {
                    for (var i = 0; i < computerHand.Count; i++)
                    {
                        if (computerHand[i] == compPlayed)
                        {
                            GameObject card = computerHand[i];
                            Vector3 newPos = discardPile.transform.position;
                            card.GetComponent<Card>().SetTargetPos(newPos);
                            computerHand.Remove(card);
                            discardDeck.Add(card);
                        }
                    }
                }

                if (timer <= -100)
                {
                    for(var i = 0; i < playerHand.Count; i++)
                    {
                        if (playerHand[i] == playerPlayed)
                        {
                            // add to discard pile and remove from hand
                            GameObject card = playerHand[i];
                            Vector3 newPos = discardPile.transform.position;
                            card.GetComponent<Card>().SetTargetPos(newPos);
                            playerHand.Remove(card);
                            discardDeck.Add(card);
                        }
                    }
                }
                //15 sec later, if opponent hand has 2 cards
                if (timer <= -150 && computerHand.Count == 2)
                {
                    for (var i = 0; i < computerHand.Count; i++)
                    {
                        //reveal card
                        computerHand[i].GetComponent<Card>().FlipCards();
                        // add to discard pile and remove from hand
                        GameObject card = computerHand[i];
                        Vector3 newPos = discardPile.transform.position;
                        card.GetComponent<Card>().SetTargetPos(newPos);
                        computerHand.Remove(card);
                        discardDeck.Add(card);
                    }
                }

                if (timer <= -130 && computerHand.Count == 1)
                {
                    for (var i = 0; i < computerHand.Count; i++)
                    {
                        //reveal card
                        computerHand[i].GetComponent<Card>().FlipCards();
                        // add to discard pile and remove from hand
                        GameObject card = computerHand[i];
                        Vector3 newPos = discardPile.transform.position;
                        card.GetComponent<Card>().SetTargetPos(newPos);
                        computerHand.Remove(card);
                        discardDeck.Add(card);
                    }
                }

                //15 sec later, if player hand has 2 cards
                if (timer <= -145 && playerHand.Count == 2)
                {
                    for (var i = 0; i < playerHand.Count; i++)
                    {
                        // add to discard pile and remove from hand
                        GameObject card = playerHand[i];
                        Vector3 newPos = discardPile.transform.position;
                        card.GetComponent<Card>().SetTargetPos(newPos);
                        playerHand.Remove(card);
                        discardDeck.Add(card);
                    }
                }

                //15 sec later, if player hand has 1 card
                if (timer <= -160 && playerHand.Count == 1)
                {
                    for (var i = 0; i < playerHand.Count; i++)
                    {
                        // add to discard pile and remove from hand
                        GameObject card = playerHand[i];
                        Vector3 newPos = discardPile.transform.position;
                        card.GetComponent<Card>().SetTargetPos(newPos);
                        playerHand.Remove(card);
                        discardDeck.Add(card);
                    }
                }

                if (timer <= -175)
                {
                    if (DeckManager.deck.Count > 0)
                    {
                        state = GameState.COMPDEAL;
                    } else
                    {
                        state = GameState.RESHUFFLE;
                    }

                    timer = maxTimer;
                }
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
    void Evaluate()
    {
        if (compPlayed.GetComponent<Card>().GetValue() == Card.CardValues.ROCK)
        {
            switch (playerPlayed.GetComponent<Card>().GetValue())
            {
                case Card.CardValues.ROCK:
                    Tie();
                    break;
                case Card.CardValues.PAPER:
                    Win();
                    break;
                case Card.CardValues.SCISSORS:
                    Loose();
                    break;
            }
        }
        if (compPlayed.GetComponent<Card>().GetValue() == Card.CardValues.PAPER)
        {
            switch (playerPlayed.GetComponent<Card>().GetValue())
            {
                case Card.CardValues.ROCK:
                    Loose();
                    break;
                case Card.CardValues.PAPER:;
                    Tie();
                    break;
                case Card.CardValues.SCISSORS:
                    Win();
                    break;
            }
        }

        if (compPlayed.GetComponent<Card>().GetValue() == Card.CardValues.SCISSORS)
        {
            switch (playerPlayed.GetComponent<Card>().GetValue())
            {
                case Card.CardValues.ROCK:
                    Win();
                    break;
                case Card.CardValues.PAPER:
                    Loose();
                    break;
                case Card.CardValues.SCISSORS:
                    Tie();
                    break;
            }
        }

    }
    void Win()
    {
        scoreMan.playerScore += 1;
        scoreMan.compScore -= 1;
        state = GameState.DISCARD;
    }
    void Tie()
    {
        state = GameState.DISCARD;
    }
    void Loose()
    {
        scoreMan.playerScore -= 1;
        scoreMan.compScore += 1;
        state = GameState.DISCARD;
    }
}

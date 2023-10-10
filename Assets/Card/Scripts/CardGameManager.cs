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

    //timer vars
    int maxTimer = 20;
    int timer = 20;
    int revealTimer;

    //hover
    float hoverAmount = 0.5f;

    //eval vars
    bool eval;
    public bool pWin;;
    public bool pLoose;
    public bool cWin;;
    public bool cLoose;
    public Score scoreMan;

    private void Start()
    {
        state = GameState.COMPDEAL;

        scoreMan = GetComponent<Score>();
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
                foreach (GameObject card in playerHand)
                {
                    if (card.GetComponent<Card>().hovered)
                    {
                        card.GetComponent<Card>().SetTargetPos(new Vector3(card.transform.position.x, playerPos.position.y + hoverAmount));

                    } else
                    {
                        card.GetComponent<Card>().SetTargetPos(new Vector3(card.transform.position.x, playerPos.position.y));
                    }
                    if (card.GetComponent<Card>().picked)
                    {
                        GameObject pickedCard = card;
                        Vector3 newPos = playerCard.transform.position;
                        pickedCard.GetComponent<Card>().SetTargetPos(newPos);
                        playerHand.Remove(pickedCard);
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
                        }
                    }
                }
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

    void Evaluate()
    {
 
    }
    void Win()
    {
        if (pWin == true)
        {
            scoreMan.playerScore += 1;
            pWin = false;
        }
        if (cWin == true)
        {
            scoreMan.compScore += 1;
            cWin = false;
        }
        state = GameState.DISCARD;
    }
    void Tie()
    {
        state = GameState.DISCARD;
    }
    void Loose()
    {
        f(pLoose == true)
        {
            scoreMan.playerScore += 1;
            ploose = false;
        }
        if (cLoose == true)
        {
            scoreMan.compScore += 1;
            cLoose = false;
        }
        state = GameState.DISCARD;
    }
}

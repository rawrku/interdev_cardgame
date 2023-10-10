using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    //hold prefab of the card
    public GameObject cardPrefab;

    //Array of the card faces
    public Sprite[] cardFaces;

    //the count in the deck
    public int deckCount;

    //list for the deck
    public static List<GameObject> deck = new List<GameObject>();

    //List<int> allScores = new List<int>();
    //List<string> studentNames = new List<string>();

    private void Start()
    {
       for (int i = 0; i < deckCount; i++)
        {
            GameObject newCard = Instantiate(cardPrefab, gameObject.transform);
            Card newCardScript = newCard.GetComponent<Card>();
            newCardScript.faceSprite = cardFaces[i % 3];
            deck.Add(newCard);
        }
    }
}

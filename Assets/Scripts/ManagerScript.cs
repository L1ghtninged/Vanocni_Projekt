using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ManagerScript : MonoBehaviour
{
    public GameObject cardPrefab;
    public Sprite[] cardSprites;
    public GameObject playerCard;
    public GameObject deckCard;
    public Player playerOnTurn = null;
    public Text playerOnTurnText; 
    public Text deckCards; 
    public Text stackCards; 
    public Text handCards;
    public Text finalPlayerWonText;
    public static int index = 0;
    public static int indexCard = 0;
    public GameObject gameOverScreen;
    public static bool isOverPlayed = false;
    public GameObject isOverPlayedScreen;
    public static bool isGameOver = false;

    

    // Start is called before the first frame update
    void Start()
    {
        newGame();
    }
    public void SetPlayerText(int index) {
        playerOnTurnText.text = index.ToString();
    }
    public void SetCardText()
    {
        int l = LogicScript.deck.Count;
        deckCards.text = l.ToString();
    }
    public void SetStackText()
    {
        int l = LogicScript.playedDeck.Count;
        stackCards.text = l.ToString();
    }
    public void SetHandText(Player player)
    {
        int l = player.GetCards().Count;
        handCards.text = l.ToString();
    }
    

    private void newTurn()
    {
        if(isGameOver||isOverPlayed)
        {
            return;
        }
        
        if(index==LogicScript.players.Count-1) 
        {
            index = 0;
        }
        else
        {
            index++;
        }
        SetPlayerText(index+1);

        indexCard = 0;
        SetPlayerOnTurn(index);
        SetPlayerCard(playerOnTurn.GetCards()[indexCard]);
        SetCardText();
        SetStackText();
        SetHandText(playerOnTurn);
        

    }
    private void newGame()
    {
        isGameOver = false;
        SetPlayerOnTurn(index);
        
        foreach(Player player in LogicScript.players)
        {
            for(int i = 0; i < 4; i++)
            {
                player.DrawCard();

            }
        }
        Debug.Log(index);
        Debug.Log(indexCard);
        Debug.Log(playerOnTurn.GetCards());
        SetPlayerCard(playerOnTurn.GetCards()[indexCard]);
        var script = deckCard.GetComponent<CardScript>();
        var random = new System.Random();
        var card = LogicScript.deck[random.Next(LogicScript.deck.Count)];
        LogicScript.RemoveCard(card);
        LogicScript.playedDeck.Push(card);
        script.SetCard(LogicScript.playedDeck.Peek());
        SetCardText();
        SetStackText();
        SetHandText(playerOnTurn);

    }
    public void PlayCard()
    {
        if(isGameOver||isOverPlayed)
        {
            return;
        }
        var script = playerCard.GetComponent<CardScript>();
        var cardScript = deckCard.GetComponent<CardScript>();
        if (playerOnTurn.IsCardPlayable(script.card, cardScript.card))
        {
            cardScript.SetCard(script.card);
            playerOnTurn.PlayCard(script.card);
            if (script.card.GetCardValue() == Value.Over)
            {
                isOverPlayed = true;
                isOverPlayedScreen.SetActive(true);

            }
            if (playerOnTurn.GetCards().Count == 0)
            {
                GameOver(playerOnTurn);
                return;
            }
            newTurn();
        }

    }
    public void SetPlayerOnTurn(int index)
    {
        playerOnTurn = LogicScript.players[index];
    }

    public void SetPlayerCard(Card card)
    {
        var cardScript = playerCard.GetComponent<CardScript>();
        cardScript.SetCard(card);
    }
    public void DrawCard()
    {
        if (isGameOver||isOverPlayed)
        {
            return;
        }
        if (!(LogicScript.playedDeck.Count == 1 && LogicScript.deck.Count == 0))
        {

            playerOnTurn.DrawCard();
            newTurn();
        }
    }
    public void LoadTitleScreen()
    {
        SceneManager.LoadScene("TitleScreen");
    }
    public void ShowPreviusCard()
    {
        if (isGameOver)
        {
            return;
        }

        if (indexCard == playerOnTurn.GetCards().Count - 1)
        {
            indexCard = 0;
        }
        else
        {
            indexCard++;
        }
        SetPlayerCard(playerOnTurn.GetCards()[indexCard]);
        
        
    }
    public void ShowNextCard()
    {
        if(isGameOver)
        {
            return;
        }
        if (indexCard == 0)
        {
            indexCard = playerOnTurn.GetCards().Count - 1;
        }
        else
        {
            indexCard--;
        }
        SetPlayerCard(playerOnTurn.GetCards()[indexCard]);
    }
    public void GameOver(Player player)
    {
        gameOverScreen.SetActive(true);
        finalPlayerWonText.text = player.ToString()+" Won";
        isGameOver = true;
        

    }
    public void ChooseAcorns()
    {
        deckCard.GetComponent<CardScript>().SetCard(new Card(Type.Acorns, Value.Over));
        isOverPlayedScreen.SetActive(false);
        isOverPlayed = false;
        newTurn();
    }
    public void ChooseLeaves()
    {
        deckCard.GetComponent<CardScript>().SetCard(new Card(Type.Leaves, Value.Over));
        isOverPlayedScreen.SetActive(false);
        isOverPlayed = false;
        newTurn();
    }
    public void ChooseHearts()
    {
        deckCard.GetComponent<CardScript>().SetCard(new Card(Type.Hearts, Value.Over));
        isOverPlayedScreen.SetActive(false);
        isOverPlayed = false;
        newTurn();
    }
    public void ChooseBells()
    {
        deckCard.GetComponent<CardScript>().SetCard(new Card(Type.Bells, Value.Over));
        isOverPlayedScreen.SetActive(false);
        isOverPlayed = false;
        newTurn();
    }




    public static int GetSpriteIndex(Type type, Value value)
    {
        int index = 0;
        
        switch (type)
        {
            case Type.Bells: 
                index += 8;
                break;
            case Type.Leaves: 
                index += 16;
                break;
            case Type.Acorns: 
                index += 24;
                break;
            
        }
        switch (value)
        {
            case Value.Seven: 
                index += 1;
                break;
            case Value.Eight:
                index += 2;
                break;
            case Value.Nine:
                index += 3;
                break;
            case Value.Ten:
                index += 4;
                break;
            case Value.Under:
                index += 5;
                break;
            case Value.Over:
                index += 6;
                break;
            case Value.King:
                index += 7;
                break;
            case Value.Ace:
                index += 8;
                break;
        }

        return index;
    }
    
}


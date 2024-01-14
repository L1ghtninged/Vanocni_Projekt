using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using System;
using System.Linq;
using UnityEditor;
using System.Threading;
using Unity.Mathematics;

public class LogicScript : MonoBehaviour
{
    private static Type[] types = {Type.Hearts, Type.Bells, Type.Leaves, Type.Acorns};
    private static Value[] values = { Value.Seven, Value.Eight, Value.Nine, Value.Ten, Value.Under, Value.Over, Value.King, Value.Ace};
    public static List<Player> players = new List<Player>();
    public static List<Card> deck = new List<Card>();
    public static Stack<Card> playedDeck = new Stack<Card>();
    public static int countSeven = 0;
    public static bool isAced = false;
    public static int turnIndex = 0;
    public static Type currentType;




    private void Start()
    {
        newDeck();
    }

    public static void newDeck()
    {
        deck.Clear();
        foreach(Type type in types)
        {
            foreach(Value value in values)
            {
                deck.Add(new Card(type, value));
            }
        }
        
    }
    public static string PrintDeck()
    {
        string temp = "";
        foreach(Card card in deck)
        {
            temp += card.ToString()+"\n";
        }


        return temp;
    }
    public static void FlipDeck()
    {
        if(playedDeck.Count <= 1) {
            return;
        }
        var card = playedDeck.Peek();
        deck = playedDeck.ToList<Card>();
        deck.Remove(card);
        playedDeck.Clear();
        playedDeck.Push(card);
    }
    public static void RemoveCard(Card card)
    {
        if (deck.Count == 0)
        {
            return;
        }
        deck.Remove(card);
        if(deck.Count == 0 ) 
        {
            FlipDeck();
        }
    }
    
    
    

}

public class Player
{
    private string name;
    private List<Card> cards;
    
    public override string ToString()
    {
        return this.name;
    }
    public Player(string name)
    {
        this.name = name;
        cards= new List<Card>();
    }
    public List<Card> GetCards()
    {
        return cards;
    }

    public void PlayCard(int index)
    {
        PlayCard(cards[index]);
    }
    public void PlayCard(Card card)
    {
        

            LogicScript.playedDeck.Push(card);
            cards.Remove(card);

         
            DoSpecialAction(card);
        }
    
    public void DrawCard()
    {
        
        if (LogicScript.isAced)
        {
            LogicScript.isAced = false;
            return;
        }
        if(LogicScript.playedDeck.Count > 1 && LogicScript.deck.Count == 0)
        {
            LogicScript.FlipDeck();
            
        }

        if (LogicScript.countSeven == 0)
        {
            GetCardFromDeck();
        }
        else
        {
            for(int i = 0; i < LogicScript.countSeven; i++)
            {
                GetCardFromDeck();
                if (LogicScript.playedDeck.Count > 1 && LogicScript.deck.Count == 0)
                {
                    LogicScript.FlipDeck();
                    GetCardFromDeck();
                }
                else if (LogicScript.playedDeck.Count == 1 && LogicScript.deck.Count == 0)
                {
                    return;
                }

            }
            LogicScript.countSeven = 0;
        }



    }
    private void GetCardFromDeck()
    {
        var random = new System.Random();
        var card = LogicScript.deck[random.Next(LogicScript.deck.Count)];
        LogicScript.RemoveCard(card);
        cards.Insert(0, card);
    }
    public bool IsCardPlayable(Card card, Card nextCard)

    {

            if (LogicScript.isAced && card.GetCardValue()!=Value.Ace)
            {
                return false;
            }
                if(LogicScript.countSeven != 0 && !(card.GetCardValue() == Value.Seven))
            {
            return false;
            }
            if (card.GetCardValue() == Value.Over)
            {
                
                return true;
            }

            if (card.GetCardType() == nextCard.GetCardType())
            {
                
                return true;
            }
            if (card.GetCardValue() == nextCard.GetCardValue())
            {
            
            return true;
            }
        
        return false;
    }
    public void chooseSuit(Type type)
    {
        LogicScript.currentType = type;
    }
    public bool ContainsValue(Value value)
    {
        foreach(Card card in cards)
        {
            if (card.GetCardValue() == value)
            return true;
        }
        return false;
    }

    public void DoSpecialAction(Card card)
    {
        switch(card.GetCardValue()) 
        {
            case Value.Over:

                break;
            case Value.Seven:
                LogicScript.countSeven += 2;
                break;
            case Value.Ace:
                LogicScript.isAced = true;
                break;
            default: break;
                
        }


    }
    



}


public class Card
{
    private Type type;
    private Value value;


    public Card(Type type, Value value)
    {
        this.type = type;
        this.value = value;
    }
    public Type GetCardType()
    {
        return type;
    }
    public Value GetCardValue()
    {
        return value;
    }
    
    public override string ToString()
    {
        return type.ToString()+"_"+value.ToString();
    }


}


public enum Type
{
    Hearts,
    Bells,
    Leaves,
    Acorns
}
public enum Value
{
    Seven,
    Eight,
    Nine,
    Ten,
    Under,
    Over,
    King,
    Ace

}


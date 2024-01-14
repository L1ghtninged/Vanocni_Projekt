using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardScript : MonoBehaviour
{
    public GameObject manager;
    private SpriteRenderer cardRenderer;
    public Card card;

    private void Awake()
    {
        cardRenderer= GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        if(cardRenderer == null)    
        cardRenderer = GetComponent<SpriteRenderer>();

   
    }

    public void SetCardImage(Sprite cardSprite)
    {
            cardRenderer.sprite = cardSprite;
        
    }
    public void SetCard(Card card)
    {
        var managerScript = manager.GetComponent<ManagerScript>();
        this.card = card;
        SetCardImage(managerScript.cardSprites[ManagerScript.GetSpriteIndex(card.GetCardType(), card.GetCardValue())]);
    }

    

    public void SetName(string name)
    {
        this.name = name;
    }
    public void SetName(Type type, Value value)
    {
        this.name = type.ToString() +"_"+value.ToString();
    }






}


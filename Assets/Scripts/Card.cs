using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card {

    public int manaCost;

    public int damagePoints;

    public string typeCard;

    public Action<bool> OnSelectedCard;

    public Action OnCardsUse;


    public Card(int mana, int damage, string type)
    {
        manaCost = mana;
        damagePoints = damage;
        typeCard = type;
    }

    public void SelectedCard(bool selection){

        if(OnSelectedCard != null)
        {
            OnSelectedCard(selection);
        }
    }

    public void WhenCardsUse()
    {
        OnCardsUse();
    }
}

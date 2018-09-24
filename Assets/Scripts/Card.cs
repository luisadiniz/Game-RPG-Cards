using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card {

    public int manaCost;

    public int damagePoints;

    public string typeCard;

    public Card(int mana, int damage, string type)
    {
        manaCost = mana;
        damagePoints = damage;
        typeCard = type;
    }
}

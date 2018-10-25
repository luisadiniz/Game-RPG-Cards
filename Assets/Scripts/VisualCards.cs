using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VisualCards : MonoBehaviour {

    public TextMeshProUGUI manaText;
    public TextMeshProUGUI damageText;
    public TextMeshProUGUI typeText;
    public Image cardImage;
    public GameObject cardSettings;

    public Color greenButton;
    public Color white;

    public Card card;

    public void SetCard(Card targetCard)
    {
        if (card != null)
        {
            card.OnSelectedCard -= HighlightCard;

            card.OnCardsUse -= OnCardUse;
        }

        card = targetCard;
        CardTexts();
        card.OnSelectedCard += HighlightCard;

        card.OnCardsUse += OnCardUse;
    }


    public void CardTexts()
    {
        manaText.text = card.manaCost.ToString();
        damageText.text = card.damagePoints.ToString();
        typeText.text = card.typeCard.ToString();
    }

    public void HighlightCard(bool highlight){

        if (highlight)
        {
            cardImage.color = greenButton;
        }

        else 
        {
            cardImage.color = white;
        }
    }

    public void EnableCards(bool active)
    {
        cardSettings.SetActive(active);
    }

    private void OnCardUse()
    {
        cardSettings.SetActive(false);

    }

}

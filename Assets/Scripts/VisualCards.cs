using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VisualCards : MonoBehaviour {

    public TextMeshProUGUI manaText;
    public TextMeshProUGUI damageText;
    public TextMeshProUGUI typeText;
    public Image card;

    public Color greenButton;
    public Color white;

    public void ChangeManaText(string manaVisualText){

        manaText.text = manaVisualText;
    }

    public void ChangeDamageText(string damageVisualText)
    {

        damageText.text = damageVisualText;
    }

    public void ChangeTypeText(string typeVisualText)
    {
        typeText.text = typeVisualText;
    }

    public void HighlightCard(bool highlight){

        if (highlight)
        {
            card.color = greenButton;
        }

        else {
            card.color = white;
        }
    }



}

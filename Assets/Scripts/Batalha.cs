using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Batalha : MonoBehaviour {

    List<string> cardType = new List<string>
    { "Ataque", "Defesa" };

    List<int> mana = new List<int>
    { 1, 2, 4, 6 };

    List<int> damage = new List<int>
    { 1, 2, 4 };


    [SerializeField]
    private List<VisualCards> cardsVisual = new List<VisualCards>();


    private List<Card> playerDeck = new List<Card>();

    private List<Card> pressedCards = new List<Card>();


    [SerializeField]
    private TextMeshProUGUI warriorLifeText;
    [SerializeField]
    private TextMeshProUGUI warriorManaText;
    [SerializeField]
    private TextMeshProUGUI enemyLifeText;
    [SerializeField]
    private TextMeshProUGUI enemyAttackText;
    [SerializeField]
    private TextMeshProUGUI warningText;

    private int warriorLife;
    private int warriorMana;
    private int enemyLife;
    private int enemyAttack;
    private int manaCount;


    [SerializeField]
    private Button playButton;

    [SerializeField]
    private Image enemyImage;
    [SerializeField]
    private Sprite deadEnemy;



    void Start () {

        playButton.enabled = false;

        enemyLife = 20;
        warriorLife = 15;
        warriorMana = 15;
        enemyAttack = 5;
        UpDateTexts();

        DisplayCardsDeck(5);
	}

    public void DisplayCardsDeck(int numberCards) {

        for (int i = 0; i < numberCards; i++)
        {
            playerDeck.Add(new Card());
        }

        for (int i = 0; i < numberCards; i++)
        {
            int randomIndexMana = Random.Range(0, mana.Count);
            int randomIndexDamage = Random.Range(0, damage.Count);
            int randomIndexType = Random.Range(0, cardType.Count);

            playerDeck[i].manaCost = mana[randomIndexMana];
            playerDeck[i].damagePoints = damage[randomIndexDamage];
            playerDeck[i].typeCard = cardType[randomIndexType]; 

            cardsVisual[i].ChangeManaText(mana[randomIndexMana].ToString());
            cardsVisual[i].ChangeDamageText(damage[randomIndexDamage].ToString());
            cardsVisual[i].ChangeTypeText(cardType[randomIndexType]);
        }


     }

    public void SelectCards(int card){

        manaCount = warriorMana;

        if (pressedCards.Contains(playerDeck[card]))
          {
              cardsVisual[card].HighlightCard(false);
              pressedCards.Remove(playerDeck[card]);

            for (int i = 0; i < pressedCards.Count; i++)
            {
                manaCount += pressedCards[i].manaCost;
            }
          }

        else
        {
             pressedCards.Add(playerDeck[card]);
             cardsVisual[card].HighlightCard(true);

            for (int i = 0; i < pressedCards.Count; i++)
            {
                manaCount -= pressedCards[i].manaCost;
            }
        }

        if(pressedCards.Count!= 0 && manaCount >= 0){
            playButton.enabled = true;
        }

        else if (manaCount < 0)
            {
               playButton.enabled = false;
               warningText.text = "Você não tem mana suficiente!";
            } 

        Debug.Log(manaCount);

      }

    public void PlayButton()
    {
        manaCount = 0;

        UsingCards(pressedCards);

        playButton.enabled = false;

        for (int i = 0; i < cardsVisual.Count; i++)
        {
            cardsVisual[i].GetComponent<Button>().enabled = true;
            cardsVisual[i].HighlightCard(false);
        }
    }
   
    public void UsingCards(List<Card> selectedCards){

        for (int i = 0; i < selectedCards.Count; i++)
            {
                if (selectedCards[i].typeCard == cardType[0])
                {
                    enemyLife -= selectedCards[i].damagePoints;

                    warriorMana -= selectedCards[i].manaCost;
                }

                else
                {
                    warriorLife += selectedCards[i].damagePoints;

                    warriorMana -= selectedCards[i].manaCost;
                }

                playerDeck.Remove(selectedCards[i]);

            }
            
        warriorLife -= enemyAttack;

        DisplayCardsDeck(selectedCards.Count);
        selectedCards.Clear();

        UpDateTexts();
        GameOver();

    }

    public void UpDateTexts(){
        warriorLifeText.text = warriorLife.ToString();
        warriorManaText.text = warriorMana.ToString();
        enemyLifeText.text = enemyLife.ToString();
        enemyAttackText.text = enemyAttack.ToString();
    }

    public void GameOver(){

        if(enemyLife <= 0){

            enemyImage.sprite = deadEnemy;
        }

        if(warriorLife <= 0){
            warningText.text = "Game Over!";
                
        }
    }
        


}

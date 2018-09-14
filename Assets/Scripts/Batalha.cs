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

    Card card1 = new Card();
    Card card2 = new Card();
    Card card3 = new Card();
    Card card4 = new Card();
    Card card5 = new Card();

    private List<Card> playerDeck = new List<Card>();

    private List<Card> pressedCards = new List<Card>();


    [SerializeField]
    private TextMeshProUGUI warriorLifeText;
    [SerializeField]
    private TextMeshProUGUI warriorManaText;
    [SerializeField]
    private TextMeshProUGUI enemyLifeText;

    private int warriorLife;
    private int warriorMana;
    private int enemyLife;
    private int enemyAttack;

    [SerializeField]
    private Button playButton;

    [SerializeField]
    private Image enemyImage;
    [SerializeField]
    private Sprite deadEnemy;



    void Start () {

        playButton.enabled = false;

        enemyLife = 5;
        warriorLife = 15;
        warriorMana = 15;
        enemyAttack = 2;
        UpDateTexts();

        DisplayCardsDeck();
	}
	



    public void DisplayCardsDeck() {

        playerDeck.Add(card1);
        playerDeck.Add(card2);
        playerDeck.Add(card3);
        playerDeck.Add(card4);
        playerDeck.Add(card5);

        for (int i = 0; i < playerDeck.Count; i++)
        {
            int randomIndexMana = Random.Range(0, mana.Count);
            int randomIndexDamage = Random.Range(0, damage.Count);
            int randomIndexType = Random.Range(0, cardType.Count);

            playerDeck[i].manaCost = mana[randomIndexMana];
            playerDeck[i].damagePoints = damage[randomIndexDamage];
            playerDeck[i].typeCard = cardType[randomIndexType];

            for (int j = 0; j < cardsVisual.Count; j++)
            {
                cardsVisual[i].ChangeManaText(mana[randomIndexMana].ToString());
                cardsVisual[i].ChangeDamageText(damage[randomIndexDamage].ToString());
                cardsVisual[i].ChangeTypeText(cardType[randomIndexType].ToString());
            }
        }

     }

    public void SelectCards(int card){

        pressedCards.Add(playerDeck[card]);

        playButton.enabled = true;

        cardsVisual[card].HighlightCard();

        for (int i = 0; i < pressedCards.Count; i++)
        {
            cardsVisual[card].GetComponent<Button>().enabled = false;
        }
    }

    public void PlayButton()
    {
        UsingCards(pressedCards);

        playButton.enabled = false;

    }
   
    public void UsingCards(List<Card> selectedCards){

        for (int i = 0; i < selectedCards.Count; i++)
        {
            if (selectedCards[i].typeCard == cardType[0])
            {
                enemyLife -= selectedCards[i].damagePoints;

                warriorMana -= selectedCards[i].manaCost;

                warriorLife -= enemyAttack;
            }

            else
            {
                warriorLife += selectedCards[i].damagePoints;

                warriorMana -= selectedCards[i].manaCost;
            }
        }

        UpDateTexts();

        GameOver();

    }

    public void UpDateTexts(){
        warriorLifeText.text = warriorLife.ToString();
        warriorManaText.text = warriorMana.ToString();
        enemyLifeText.text = enemyLife.ToString();
    }

    public void GameOver(){

        if(enemyLife <= 0){

            enemyImage.sprite = deadEnemy;
        }

        if(warriorLife == 0){
            
        }
    }
        


}

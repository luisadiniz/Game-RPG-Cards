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
    private TextMeshProUGUI warningText;

    private int warriorLife;
    private int warriorMana;
    private int manaCount;
    private int damageCount;

    private bool running;



    [SerializeField]
    private Button playButton;
    [SerializeField]
    private GameObject newEnemyButton;


    [SerializeField]
    VisualEnemy visualEnemy;

    [SerializeField]
    private TextMeshProUGUI warningEnemyText;
    [SerializeField]
    private TextMeshProUGUI warningWarriorText;

    private int roundNum;

    void Start () {

        playButton.enabled = false;
        ClearWarningTexts();

        visualEnemy.CreateNewEnemy(3, 5);

        warriorLife = 15;
        warriorMana = 15;

        roundNum = 0;
        UpDateTexts();
        visualEnemy.EnemyTexts();


        DisplayCardsDeck(5);

        newEnemyButton.SetActive(false);

	}

    public void DisplayCardsDeck(int numberCards) {

        for (int i = 0; i < numberCards; i++)
        {
            int randomIndexMana = Random.Range(0, mana.Count);
            int randomIndexDamage = Random.Range(0, damage.Count);
            int randomIndexType = Random.Range(0, cardType.Count);
         
            playerDeck.Add(new Card(mana[randomIndexMana], damage[randomIndexDamage],cardType[randomIndexType]));
        }

        for (int i = 0; i < playerDeck.Count; i++)
        {
            cardsVisual[i].ChangeManaText(playerDeck[i].manaCost.ToString());
            cardsVisual[i].ChangeDamageText(playerDeck[i].damagePoints.ToString());
            cardsVisual[i].ChangeTypeText(playerDeck[i].typeCard);
        }
    }

    public void SelectCards(int card){

        manaCount = warriorMana;

        if (pressedCards.Contains(playerDeck[card]))
          {
              cardsVisual[card].HighlightCard(false);
              pressedCards.Remove(playerDeck[card]);
          }

        else
        {
             pressedCards.Add(playerDeck[card]);
             cardsVisual[card].HighlightCard(true);
        }

        for (int i = 0; i < pressedCards.Count; i++)
        {
            manaCount -= pressedCards[i].manaCost;
        }


        if(pressedCards.Count!= 0 && manaCount >= 0){
            playButton.enabled = true;
            warningText.text = "";

        }

        else if (manaCount < 0)
            {
               playButton.enabled = false;
               warningText.text = "Você não tem mana suficiente!";
            } 

      }

    public void PlayButton()
    {
        manaCount = 0;
        warningText.text = "";

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
                visualEnemy.enemy.enemyLife -= selectedCards[i].damagePoints;

                warriorMana -= selectedCards[i].manaCost;
             }

             else
            {
                warriorLife += selectedCards[i].damagePoints;

                warriorMana -= selectedCards[i].manaCost;
            }

            playerDeck.Remove(selectedCards[i]);

            StartCoroutine(WarriorAttackDelay(selectedCards[i].damagePoints));
         }

        warriorLife -= visualEnemy.enemy.enemyAttack;

        if (warriorLife > 15)
        { warriorLife = 15; }

        DisplayCardsDeck(selectedCards.Count);
        selectedCards.Clear();

        StartCoroutine(EnemyAttackDelay(selectedCards));
       
    }

    public void UpDateTexts(){
        warriorLifeText.text = warriorLife.ToString();
        warriorManaText.text = warriorMana.ToString();

    }

    public void ClearWarningTexts(){
        warningText.text = "";
        warningEnemyText.text = "";
        warningWarriorText.text = "";
    }

    public void GameOver(){

        if(visualEnemy.enemy.enemyLife <= 0){

            visualEnemy.DeadEnemySprite();
            warningText.text = "Game Over!";

            StartCoroutine(EnemyCreateDelay(4));

        }

        if(warriorLife <= 0){
            warningText.text = "Game Over!";
                            
        }
    }

    public void NewEnemy(){
        
        visualEnemy.CreateNewEnemy(5, 10);

        warriorLife = 15;
        warriorMana = 15;

        UpDateTexts();
        visualEnemy.EnemyTexts();


        playerDeck = new List<Card>();
        DisplayCardsDeck(5);

        warningText.text = "";

        newEnemyButton.SetActive(false);
    }
        
    IEnumerator EnemyCreateDelay(int seconds){
        yield return new WaitForSeconds(seconds);

        NewEnemy();
    }

    IEnumerator WarriorAttackDelay(int sum)
    {
        yield return new WaitForSeconds(1);

        warningEnemyText.text = "-" + sum.ToString();
        visualEnemy.EnemyTexts();

    }

    IEnumerator EnemyAttackDelay(List<Card> selectedCards)
    {
        yield return new WaitForSeconds(3);

        warningText.text = "Ataque do Inimigo!";

        warningWarriorText.text = "-" + visualEnemy.enemy.enemyAttack.ToString();
          
        UpDateTexts();
        GameOver();

        StartCoroutine(ClearTextsAfterSeconds());
    }

    IEnumerator ClearTextsAfterSeconds(){
        yield return new WaitForSeconds(3);

        ClearWarningTexts();
    }

}

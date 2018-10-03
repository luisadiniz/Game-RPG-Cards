using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Batalha : MonoBehaviour {

    List<string> cardType = new List<string>
    { "Ataque", "Defesa" };

    List<int> mana = new List<int>
    { 1, 2, 4, 5, 6 };

    List<int> damage = new List<int>
    { 2, 4, 8 };


    [SerializeField]
    private List<VisualCards> cardsVisual = new List<VisualCards>();

    private List<Card> playerDeck = new List<Card>();

    private List<Card> pressedCards = new List<Card>();

    private List<Card> fullDeck = new List<Card>();

    [SerializeField]
    private TextMeshProUGUI warriorLifeText;
    [SerializeField]
    private TextMeshProUGUI warriorManaText;

    [SerializeField]
    private TextMeshProUGUI warningText;
    [SerializeField]
    private TextMeshProUGUI deckText;

    private int warriorLife;
    private int warriorMana;
    private int manaCount;
    private int damageCount;
    private int protectionCount;
    private int deckCount;

    [SerializeField]
    private Button playButton;
    [SerializeField]
    private Button enemy1Button;
    [SerializeField]
    private Button enemy2Button;

    [SerializeField]
    VisualEnemy visualEnemy;


    private bool gameOverCheck;


    void Start () {

        CreateFullDeck();
        DisplayCardsDeck(5);

        playButton.enabled = false;
        enemy1Button.enabled = false;
        enemy2Button.enabled = false;

        warningText.text = "";

        visualEnemy.CreateNewEnemy(3, 5);

        warriorLife = 15;
        warriorMana = 15;

        UpDateTexts();
        visualEnemy.EnemyTexts();

	}

    public void CreateFullDeck(){

        for (int i = 0; i < cardType.Count; i++)
        {
            for (int j = 0; j < mana.Count; j++)
            {
                int randomIndexDamage = Random.Range(0, damage.Count);

                Card newCard = new Card(mana[j],damage[randomIndexDamage], cardType[i]);
                Card newCard2 = new Card(mana[j], damage[randomIndexDamage], cardType[i]);

                fullDeck.Add(newCard);
                fullDeck.Add(newCard2);
            }
        }
    }


    public void DisplayCardsDeck(int numberCards) {

        deckCount = fullDeck.Count;

        for (int i = 0; i < numberCards; i++)
        {
            int randomIndexMana = Random.Range(0, fullDeck.Count);

            playerDeck.Add(fullDeck[randomIndexMana]);
        }

        if (fullDeck.Count != 0)
        {
            for (int i = 0; i < playerDeck.Count; i++)
            {
                cardsVisual[i].ChangeManaText(playerDeck[i].manaCost.ToString());
                cardsVisual[i].ChangeDamageText(playerDeck[i].damagePoints.ToString());
                cardsVisual[i].ChangeTypeText(playerDeck[i].typeCard);
            }
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
        warningText.text = "Selecione o Inimigo";

        playButton.enabled = false;

        for (int i = 0; i < cardsVisual.Count; i++)
        {
            cardsVisual[i].GetComponent<Button>().enabled = true;
            cardsVisual[i].HighlightCard(false);
        }

        enemy1Button.enabled = true;
        enemy2Button.enabled = true;
    }
   

    public void UsingCards(List<Card> selectedCards, Enemy enemy){

        damageCount = 0;
        protectionCount = 0;

        for (int i = 0; i < selectedCards.Count; i++)
        {
            if (selectedCards[i].typeCard == cardType[0])
            {
                enemy.enemyLife -= selectedCards[i].damagePoints;

                warriorMana -= selectedCards[i].manaCost;

                damageCount += selectedCards[i].damagePoints;
             }

             else
            {
                warriorLife += selectedCards[i].damagePoints;

                warriorMana -= selectedCards[i].manaCost;

                protectionCount += selectedCards[i].damagePoints;
            }

            cardsVisual[i].EnableCards(false);

            playerDeck.Remove(selectedCards[i]);
            fullDeck.Remove(selectedCards[i]);

            StartCoroutine(WarriorAttackDelay(damageCount));

         }

        warriorLife -= (visualEnemy.enemy1.enemyAttack + visualEnemy.enemy2.enemyAttack);

        if (warriorLife > 15)
        { warriorLife = 15; }

        visualEnemy.EnemyLifeCondition();

        DisplayCardsDeck(selectedCards.Count);

        selectedCards.Clear();

        StartCoroutine(EnemyAttackDelay());
       
    }

    public void EnemySelection(string enemy)
    {
        if (enemy == "Inimigo 1") 
        { UsingCards(pressedCards, visualEnemy.enemy1); }

        else
        {
            UsingCards(pressedCards, visualEnemy.enemy2);
        }
    }

    public void UpDateTexts(){
        warriorLifeText.text = warriorLife.ToString();
        warriorManaText.text = warriorMana.ToString();

        deckText.text = "Cartas no Baralho: " + deckCount.ToString();

    }


    public void GameOver(){

        if(visualEnemy.enemy1.enemyLife <= 0 && visualEnemy.enemy2.enemyLife <= 0){

            gameOverCheck = true;

            warningText.text = "Game Over!";

            StartCoroutine(EnemyCreateDelay(4));
        }

        if(visualEnemy.enemy1.enemyLife <= 0)
        {
            visualEnemy.DeadEnemySprite(visualEnemy.enemyImage1, visualEnemy.randomEnemy1);
        }
        if(visualEnemy.enemy2.enemyLife <= 0)
        {
            visualEnemy.DeadEnemySprite(visualEnemy.enemyImage2, visualEnemy.randomEnemy2);
        }

        if(warriorLife <= 0)
        {
            warningText.text = "Game Over!";

            for (int i = 0; i < cardsVisual.Count; i++)
            {
                cardsVisual[i].GetComponent<Button>().enabled = false;
            }

            playButton.enabled = false;

        }

        if(fullDeck.Count == 0 && visualEnemy.enemy1.enemyLife > 0)
        {
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
    }
        

    IEnumerator WarriorAttackDelay(int sum)
    {
        yield return new WaitForSeconds(1);

        warningText.text = "Seu Ataque" + "\n" + "Dano: " + sum.ToString() + "\n" + "Defende: " + protectionCount.ToString();

        visualEnemy.EnemyTexts();
        deckText.text = "Cartas no Baralho: " + deckCount.ToString();

        GameOver();
    }

    IEnumerator EnemyAttackDelay()
    {
        yield return new WaitForSeconds(5);

        if (gameOverCheck == false)
        {
            int enemyAttack = (visualEnemy.enemy1.enemyAttack + visualEnemy.enemy2.enemyAttack) - protectionCount;

            warningText.text = "Ataque do Inimigo" + "\n" + "Dano: " + enemyAttack.ToString();
        }

        GameOver();

        UpDateTexts();

        StartCoroutine(ClearTextsAfterSeconds());
    }

    IEnumerator EnemyCreateDelay(int seconds)
    {
        yield return new WaitForSeconds(seconds);

        NewEnemy();
    }

    IEnumerator ClearTextsAfterSeconds()
    {

        yield return new WaitForSeconds(3);

        if (gameOverCheck == false)
        {
            warningText.text = "";

            warriorMana += 2;

            if (warriorMana > 15)
            { warriorMana = 15; }

            UpDateTexts();

            for (int i = 0; i < cardsVisual.Count; i++)
            {
                cardsVisual[i].EnableCards(true);
            }
        }
    }
}

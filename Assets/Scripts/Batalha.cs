using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Batalha : MonoBehaviour
{

    List<string> cardType = new List<string>
    { "Ataque", "Defesa" };

    List<int> mana = new List<int>
    { 1, 2, 4, 5, 6 };

    List<int> damage = new List<int>
    { 2, 4, 8 };


    private List<Card> playerDeck = new List<Card>();

    private List<Card> pressedCards = new List<Card>();

    private List<Card> fullDeck = new List<Card>();

    private List<Enemy> enemyList = new List<Enemy>();


    private int warriorLife;
    public int warriorMana;
    private int manaCount;
    private int damageCount;
    private int protectionCount;
    private int deckCount;
    private int attackCount;

    //properties
    public int WarriorLife
    {
        get { return warriorLife; }
    }
    public int AttackCount
    {
        get { return attackCount; }
    }
    public int DeckCount
    {
        get { return fullDeck.Count; }
    }
    public int ProtectionCount
    {
        get { return protectionCount; }
    }
    public List<Card> PressedCards 
    { get { return pressedCards; }}

    public bool GameOverCheck 
    { get { return gameOverCheck; } }


    private bool gameOverCheck;

    [SerializeField]
    private BatalhaVisual batalhaVisual;

    public Action OnWarriorLifeChange;


    public void Awake()
    {
        CreateFullDeck();

        warriorLife = 30;
        warriorMana = 20;

        DisplayCardsDeck(5);

    }

	public void Start()
	{
        EnemySpwaning();
	}

	public void EnemySpwaning()
    {
        int randomNumberEnemies = Random.Range(1, 4);

        for (int i = 0; i < randomNumberEnemies; i++)
        {
            Enemy enemy = new Enemy();
            enemy.enemyAttack = 10;
            enemy.enemyLife = 5;

            int randomEnemy = Random.Range(0, 3);
            enemy.enemyType = randomEnemy;

            enemyList.Add(enemy);
        }

        batalhaVisual.OnEnemiesSpawneds(enemyList);
    }

    public void CreateFullDeck()
    {

        for (int i = 0; i < cardType.Count; i++)
        {
            for (int j = 0; j < mana.Count; j++)
            {
                int randomIndexDamage = Random.Range(0, damage.Count);

                Card newCard = new Card(mana[j], damage[randomIndexDamage], cardType[i]);
                Card newCard2 = new Card(mana[j], damage[randomIndexDamage], cardType[i]);

                fullDeck.Add(newCard);
                fullDeck.Add(newCard2);
            }
        }
    }


    public void DisplayCardsDeck(int numberCards)
    {
        deckCount = fullDeck.Count;

        for (int i = 0; i < numberCards; i++)
        {
            int randomIndexDeck = Random.Range(0, fullDeck.Count);

             playerDeck.Add(fullDeck[randomIndexDeck]);
             fullDeck.Remove(fullDeck[randomIndexDeck]);
        }

        batalhaVisual.OnPlayerHandUpdate(playerDeck);
    }


    public void SelectCards(int card)
    {
        manaCount = warriorMana;

        if (pressedCards.Contains(playerDeck[card]))
        {
            pressedCards.Remove(playerDeck[card]);

            playerDeck[card].OnSelectedCard(false);
        }

        else
        {
            pressedCards.Add(playerDeck[card]);
            playerDeck[card].OnSelectedCard(true);

        }

        for (int i = 0; i < pressedCards.Count; i++)
        {
            manaCount -= pressedCards[i].manaCost;
        }

        batalhaVisual.UsingVisualCards(manaCount);

    }


    public void UsingCards(Enemy enemy)
    {
        damageCount = 0;
        protectionCount = 0;
        attackCount = 0;

        for (int i = 0; i < pressedCards.Count; i++)
        {
            if (pressedCards[i].typeCard == cardType[0])
            {
                enemy.enemyLife -= pressedCards[i].damagePoints;

                warriorMana -= pressedCards[i].manaCost;

                damageCount += pressedCards[i].damagePoints;
            }

            else
            {
                warriorMana -= pressedCards[i].manaCost;

                protectionCount += pressedCards[i].damagePoints;
            }

            batalhaVisual.UsingVisualCards(manaCount);

            playerDeck.Remove(pressedCards[i]);
        }

        StartCoroutine(batalhaVisual.WarriorAttackDelay(damageCount , enemy));

        EnemyAttack();


        if (fullDeck.Count > 0)
        {
            DisplayCardsDeck(pressedCards.Count);
        }

        else
        {
            for (int i = 0; i < pressedCards.Count; i++)
            {
                pressedCards[i].OnCardsUse();
            }

        }

        pressedCards.Clear();

        StartCoroutine(batalhaVisual.EnemyAttackDelay());

    }

    public void EnemyAttack()
    {
        for (int i = 0; i < enemyList.Count; i++)
        {
            if (enemyList[i].enemyLife > 0)
            {
                attackCount += enemyList[i].enemyAttack;
            }
        }

        if (attackCount >= protectionCount)
        {
            warriorLife -= (attackCount - protectionCount);
        }


        for (int i = 0; i < enemyList.Count; i++)
        {
            enemyList[i].EnemyLifeCondition();
        }
    }


    public void EnemySelection(int enemy)
    {
        manaCount = 0;

        UsingCards(enemyList[enemy]);

        batalhaVisual.EnemyButtonActivation(false);
    }

    public void GameOver()
    {
        int deadEnemies = 0;

        for (int i = 0; i < enemyList.Count; i++)
        {
            if (enemyList[i].enemyLife <= 0)
            {
                deadEnemies++;
            }

            if (deadEnemies == enemyList.Count)
            {
                gameOverCheck = true;

                batalhaVisual.GameOverVisual(gameOverCheck);

                NewEnemy();
            }
        }

        if (warriorLife <= 0 || playerDeck.Count == 0)
        {
            gameOverCheck = true;

            batalhaVisual.GameOverVisual(gameOverCheck);

            batalhaVisual.SetActiveCardsButton(false);

        }

    }

    public void NewEnemy()
    {
        enemyList.Clear();

        EnemySpwaning();

        batalhaVisual.SetActiveCardsButton(true);


        batalhaVisual.ButtonsInicialState();

        gameOverCheck = false;
    }

    public void NewCardsDeck()
    {
        playerDeck = new List<Card>();
        DisplayCardsDeck(5);  
    }
}

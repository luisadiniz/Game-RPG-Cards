using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class BatalhaVisual : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI warriorLifeText;
    [SerializeField]
    private TextMeshProUGUI warriorManaText;

    [SerializeField]
    private TextMeshProUGUI warningText;
    [SerializeField]
    private TextMeshProUGUI deckText;

    [SerializeField]
    private Button playButton;
    [SerializeField]
    private GameObject restartButton;
    [SerializeField]
    private GameObject playButtonGameObject;

    [SerializeField]
    private Batalha batalha;

    [SerializeField]
    private List<VisualCards> cardsVisual = new List<VisualCards>();

    [SerializeField]
    private List<VisualEnemy> enemiesVisual;


	public void Awake()
	{
        batalha.OnWarriorLifeChange += UpDateTexts;

        playButton.enabled = true;

        ButtonsInicialState();
	}

	public void Start()
    {
        UpDateTexts();

        SetActiveCardsButton(true);
    }

    public void OnEnemiesSpawneds(List<Enemy> list)
    {
        for (int i = 0; i < enemiesVisual.Count; i++)
        {
            if (i < list.Count)
            {
                enemiesVisual[i].enemyObject.SetActive(true);

                enemiesVisual[i].SetEnemy(list[i]);
            }
            else
            {
                enemiesVisual[i].enemyObject.SetActive(false);
            }
        }
    }

    public void OnPlayerHandUpdate(List<Card> listCards)
    {
        for (int i = 0; i < listCards.Count; i++)
        {
            cardsVisual[i].SetCard(listCards[i]);
        }
    }

    public void UsingVisualCards(int manaCount)
    {
        if (manaCount >= 0)
        {
            playButton.enabled = true;
            warningText.text = "";
        }

        else if (manaCount <= 0)
        {
            playButton.enabled = false;
            warningText.text = "Você não tem mana suficiente!";
        }
    }

    public void UpDateTexts()
    {
        batalha.OnWarriorLifeChange += UpDateTexts;

        warriorLifeText.text = batalha.WarriorLife.ToString();
        warriorManaText.text = batalha.warriorMana.ToString();

        deckText.text = "Cartas no Baralho: " + batalha.DeckCount.ToString();
    }

    public void EnemyButtonActivation(bool active)
    {
        for (int i = 0; i < enemiesVisual.Count; i++)
        {
            enemiesVisual[i].enemyButton.enabled = active;
        }
    }


    public void GameOverVisual(bool gameOver)
    {
        if (gameOver == true)
        {
            warningText.text = "Game Over!";

            playButton.enabled = false;
            EnemyButtonActivation(false);

            playButtonGameObject.SetActive(false);
            restartButton.SetActive(true);
        }

    }

    public void SelectCards(int card)
    {
        batalha.SelectCards(card);
    }


    public void DeselectPressedCards()
    {
        for (int i = 0; i < cardsVisual.Count; i++)
        {
            cardsVisual[i].GetComponent<Button>().enabled = true;
            cardsVisual[i].HighlightCard(false);
        }
    }

    public void EnabledVisualCardsSelected(bool enabled)
    {
        for (int i = 0; i < batalha.PressedCards.Count; i++)
        {
            cardsVisual[i].EnableCards(enabled);
        }
    }

    public void SetActiveCardsButton(bool active)
    {
        for (int i = 0; i < cardsVisual.Count; i++)
        {
            cardsVisual[i].GetComponent<Button>().enabled = active;
        }

    }

    public void PlayButton()
    {
        warningText.text = "Selecione o Inimigo";

        playButton.enabled = false;

        DeselectPressedCards();

        EnemyButtonActivation(true);

        if (batalha.PressedCards.Count == 0)
        {
            warningText.text = "";

            batalha.EnemyAttack();
            StartCoroutine(EnemyAttackDelay());
        }
    }

    public IEnumerator WarriorAttackDelay(int sum , Enemy enemyAttacked)
    {
        yield return new WaitForSeconds(1);

        warningText.text = "Seu Ataque" + "\n" + "Dano: " + sum.ToString() + "\n" + "Defende: " + batalha.ProtectionCount.ToString();

        enemyAttacked.OnAttackRecived();
        deckText.text = "Cartas no Baralho: " + batalha.DeckCount.ToString();
    }

    public IEnumerator EnemyAttackDelay()
    {
        yield return new WaitForSeconds(3);

        if (batalha.GameOverCheck == false)
        {
            warningText.text = "Ataque do Inimigo" + "\n" + "Dano: " + batalha.AttackCount.ToString();
        }

        batalha.GameOver();

        UpDateTexts();

        StartCoroutine(ClearTextsAfterSeconds());

    }

    public IEnumerator ClearTextsAfterSeconds()
    {
        yield return new WaitForSeconds(4);

        if (batalha.GameOverCheck == false)
        {
            warningText.text = "";

            batalha.warriorMana += 2;

            UpDateTexts();

            EnabledVisualCardsSelected(true);

            playButton.enabled = true;

        }
    }

    public void ButtonsInicialState()
    {
        restartButton.SetActive(false);

        playButtonGameObject.SetActive(true);
        playButton.enabled = true;

        warningText.text = "";
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(0);

        batalha.NewCardsDeck();
    }

}

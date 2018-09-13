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

    Card card1;

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




    void Start () {

        enemyLife = 20;
        warriorLife = 15;
        warriorMana = 15;
        enemyAttack = 2;

        DisplayCardsDeck();
	}
	
	void Update () {


		
	}


    public void DisplayCardsDeck() {

        int randomIndexMana = Random.Range(0, mana.Count);
        int randomIndexDamage = Random.Range(0, damage.Count);
        int randomIndexType = Random.Range(0, cardType.Count);

        card1 = new Card();
        card1.manaCost = mana[randomIndexMana];
        card1.damagePoints = damage[randomIndexDamage];
        card1.typeCard = cardType[randomIndexType];


        for (int i = 0; i < cardsVisual.Count; i++)
        {
            cardsVisual[i].ChangeManaText(mana[randomIndexMana].ToString());
            cardsVisual[i].ChangeDamageText(damage[randomIndexDamage].ToString());
            cardsVisual[i].ChangeTypeText(cardType[randomIndexType].ToString());
        }
     }

    public void UsingCards(string card){

        if(card1.typeCard == cardType[0])
        {
            enemyLife -= card1.damagePoints;

            warriorMana -= card1.manaCost;

            warriorLife -= enemyAttack;
        }

        else {

            warriorLife += card1.damagePoints;

            warriorMana -= card1.damagePoints;
        }

        UpDateTexts();
    }

    public void UpDateTexts(){
        warriorLifeText.text = warriorLife.ToString();
        warriorManaText.text = warriorMana.ToString();
        enemyLifeText.text = enemyLife.ToString();
    }

        


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VisualEnemy : MonoBehaviour {

    public List<Sprite> enemyAttackSprite;
    public List<Sprite> enemyDeadSprite;

    public Image enemyImage;
    public TextMeshProUGUI enemyLifeText;
    public TextMeshProUGUI enemyAttackText;

    public Button enemyButton;
    public string enemyStatus;

    public GameObject enemyObject;

    private Enemy enemyLogic;

    public void SetEnemy(Enemy targetEnemy)
    {
        enemyLogic = targetEnemy;
        WhenAlive();
        EnemyTexts();

        enemyLogic.OnAttackRecived += EnemyTexts;
    }

    public void EnemyTexts()
    {
        enemyLifeText.text = enemyLogic.enemyLife.ToString();
        enemyAttackText.text = enemyLogic.enemyAttack.ToString();

        if(enemyLogic.enemyLife == 0)
        {
            WhenDead();
        }
     }

    public void WhenAlive()
    {
        enemyImage.sprite = enemyAttackSprite[enemyLogic.enemyType];
    }

    public void WhenDead()
    {
        enemyImage.sprite = enemyDeadSprite[enemyLogic.enemyType];
    }


}

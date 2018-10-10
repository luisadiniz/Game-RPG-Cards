using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VisualEnemy : MonoBehaviour {

    public List<Sprite> enemyAttackSprites;
    public List<Sprite> enemyDeadSprites;

    public List<Image> enemyImage;
    public List<TextMeshProUGUI> enemyLifeText;
    public List<TextMeshProUGUI> enemyAttackText;

    public List<Button> enemyButton;

    public List<GameObject> enemyObject;

    public List<Enemy> enemyList = new List<Enemy>();


    public void CreateNewEnemy(int attack)
    {
        int randomNumberEnemies = Random.Range(1, 4);

        for (int i = 0; i < randomNumberEnemies; i++)
        {
            Enemy enemy = new Enemy();
            enemy.enemyAttack = attack;
            enemy.enemyLife = 5;

            enemyList.Add(enemy);
        }

        for (int i = 0; i < enemyList.Count; i++)
        {
            int randomEnemy = Random.Range(0, enemyAttackSprites.Count);
            enemyImage[i].sprite = enemyAttackSprites[randomEnemy];

            enemyAttackText[i].text = enemyList[i].enemyLife.ToString();

            enemyObject[i].SetActive(true);
            enemyList[i].enemyType = randomEnemy;
        }

    }

    public void EnemyTexts()
    {
        for (int i = 0; i < enemyList.Count; i++)
        {
            enemyLifeText[i].text = enemyList[i].enemyLife.ToString();
        }
     }


    public void DeadEnemySprite(){

        for (int i = 0; i < enemyList.Count; i++)
        {
            if (enemyList[i].enemyLife <= 0)
            {
              enemyImage[i].sprite = enemyDeadSprites[enemyList[i].enemyType];
            }
        }
    }

    public void EnemyLifeCondition()
    {
        for (int i = 0; i < enemyList.Count; i++)
        {
            if (enemyList[i].enemyLife < 0)
            { enemyList[i].enemyLife = 0; }
        }
    }

    public void EnemyButtonActivation(bool active)
    {
        for (int i = 0; i < enemyButton.Count; i++)
        {
            enemyButton[i].enabled = active;
        }
    }

    public void DesativateEnemyObject()
    {
        for (int i = 0; i < enemyObject.Count; i++)
        {
            enemyObject[i].SetActive(false);
        }
    }

    public void ClearEnemyList()
    {
        enemyList.Clear();
    }
}

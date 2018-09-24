using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VisualEnemy : MonoBehaviour {

    [SerializeField]
    private TextMeshProUGUI enemyLifeText;
    [SerializeField]
    private TextMeshProUGUI enemyAttackText;

    public List<Sprite> enemyAttackSprites;
    public List<Sprite> enemyDeadSprites;


    public Image enemyImage;
    public Enemy enemy;

    int randomEnemy;

    public void EnemyTexts(){
        enemyLifeText.text = enemy.enemyLife.ToString();
        enemyAttackText.text = enemy.enemyAttack.ToString();
    }

    public void CreateNewEnemy(int life, int attack)
    {
        enemy = new Enemy();
        enemy.enemyLife = life;
        enemy.enemyAttack = attack;

        randomEnemy = Random.Range(0, enemyAttackSprites.Count);
        enemyImage.sprite = enemyAttackSprites[randomEnemy];

    }

    public void DeadEnemySprite(){
        enemyImage.sprite = enemyDeadSprites[randomEnemy];

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VisualEnemy : MonoBehaviour {

    [SerializeField]
    private TextMeshProUGUI enemy1LifeText;
    [SerializeField]
    private TextMeshProUGUI enemy1AttackText;
    [SerializeField]
    private TextMeshProUGUI enemy2LifeText;
    [SerializeField]
    private TextMeshProUGUI enemy2AttackText;

    public List<Sprite> enemyAttackSprites;
    public List<Sprite> enemyDeadSprites;


    public Image enemyImage1;
    public Image enemyImage2;
    public Enemy enemy1;
    public Enemy enemy2;

    public int randomEnemy1;
    public int randomEnemy2;

    public void EnemyTexts(){
        enemy1LifeText.text = enemy1.enemyLife.ToString();
        enemy1AttackText.text = enemy1.enemyAttack.ToString();

        enemy2LifeText.text = enemy2.enemyLife.ToString();
        enemy2AttackText.text = enemy2.enemyAttack.ToString();
    }

    public void CreateNewEnemy(int life, int attack)
    {
        enemy1 = new Enemy();
        enemy1.enemyLife = life;
        enemy1.enemyAttack = attack;

        randomEnemy1 = Random.Range(0, enemyAttackSprites.Count);
        enemyImage1.sprite = enemyAttackSprites[randomEnemy1];

        enemy2 = new Enemy();
        enemy2.enemyLife = life;
        enemy2.enemyAttack = attack;

        randomEnemy2 = Random.Range(0, enemyAttackSprites.Count);
        enemyImage2.sprite = enemyAttackSprites[randomEnemy2];

    }

    public void DeadEnemySprite(Image image, int randomEnemy){
        image.sprite = enemyDeadSprites[randomEnemy];
    }

    public void EnemyLifeCondition(){
        if (enemy1.enemyLife < 0)
        { enemy1.enemyLife = 0; }

        if (enemy2.enemyLife < 0)
        { enemy2.enemyLife = 0; }
    }
}

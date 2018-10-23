using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Enemy {

    public int enemyLife;
    public int enemyAttack;

    public int enemyType;

    public Action OnAttackRecived;

    public void EnemyLifeCondition()
    {
        if (enemyLife < 0)
        { enemyLife = 0; }
    }

    public void AttackRecived()
    {
        if (OnAttackRecived != null)
        {
            OnAttackRecived();
        }
    }

}

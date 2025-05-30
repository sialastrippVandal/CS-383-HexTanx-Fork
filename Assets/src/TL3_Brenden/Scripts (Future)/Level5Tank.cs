using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level5Tank : TankType
{
    public override void Initialize()
    {
        enemyActionPoints = 5;
        health = 100;  // Set health specific to Level 4 Tank
        // Debug.Log("Level 5 Tank Spawned with " + health + " HP");
        damage = findDamageVal(30);
    }

    public int findDamageVal(int baseDamage)
    {
        bool bcModeOn = PlayerPrefs.GetInt("BCMode", 0) == 1;

        DamageEffect damageEffect;
        if (bcModeOn)
        {
            damageEffect = new BCEnemyDamage(baseDamage);
        }
        else
        {
            damageEffect = new DamageEffect(baseDamage);
        }
        return damageEffect.GetDamage();
    }
}
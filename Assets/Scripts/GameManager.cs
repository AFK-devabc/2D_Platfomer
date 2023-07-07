using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] protected EnemyStarts flyStart;
    [SerializeField] protected EnemyStarts Boss;
    [SerializeField] protected EnemyStarts RockBug;
    [SerializeField] protected EnemyStarts RockSlug;
    [SerializeField] protected EnemyStarts earthSlime;


    void Start()
    {
        DataPersistenceManager.instance.StartLoadGame();
        if(DataPersistenceManager.instance.gameMode == GameMode.Normal)
        {
            flyStart.maxHealth = 4;
            Boss.maxHealth = 10;
            RockBug.maxHealth = 5;
            RockSlug.maxHealth = 3;
            earthSlime.maxHealth = 3;

        }
        else if(DataPersistenceManager.instance.gameMode != GameMode.Hard)
        {
            flyStart.maxHealth = 6;
            Boss.maxHealth = 15;
            RockBug.maxHealth = 8;
            RockSlug.maxHealth = 5;
            earthSlime.maxHealth= 5;
        }

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int deathCount;
    public Vector3 playerPosition;
    public float playerHealth;

    //public SerializableDictionary<string, bool> coinsCollected;

    // the values defined in this constructor will be the default values
    // the game starts with when there's no data to load
    public GameData() 
    {
        this.deathCount = 0;
        playerPosition = new Vector3(-110,30,0);
        playerHealth = 5;
        //coinsCollected = new SerializableDictionary<string, bool>();
    }
}

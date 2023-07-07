using UnityEngine;

[System.Serializable]

public struct EnemyData
{
public    bool isDestroyed;
    public Vector3 position;
    EnemyData(bool isDestroyed, Vector3 position)
    {
        this.isDestroyed = isDestroyed;
        this.position = position;
    }
};

public enum GameMode
{
    Normal, 
    Hard
}

public class GameData
{
    public GameMode gameMode;
    public int deathCount;
    public Vector3 playerPosition;
    public float playerHealth;
    public bool canUseDash;
    public int extraJump;
    public SerializableDictionary<string, EnemyData> enemies;
    public SerializableDictionary<string, bool> abilityStones;

    // the values defined in this constructor will be the default values
    // the game starts with when there's no data to load
    public GameData(GameMode gameMode) 
    {
        this.deathCount = 0;
        playerPosition = new Vector3(-110,30,0);
        playerHealth = 5;
        canUseDash = false;
        extraJump = 0;
        this.gameMode = gameMode;
        enemies = new SerializableDictionary<string, EnemyData>();
        abilityStones = new SerializableDictionary<string, bool>(); 
    }
}

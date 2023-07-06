using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyHealth : Health, IDataPersistence
{
    [SerializeField] private string id;
    
    [ContextMenu("Generate ID")]
    private void GenerateID()
    {
        id = System.Guid.NewGuid().ToString();
    }

    [SerializeField] protected HealthbarBehavior healthbar;
    [SerializeField] private Material flashMaterial;

    private float flashSpeed = 0.2f;
    private List< MeshRenderer> mesh;
    private List< SkinnedMeshRenderer> skinmesh;

    private void Awake()
    {
        health = maxHealth;
        healthbar.SetHealth(health, maxHealth);

        mesh = GetComponentsInChildren<MeshRenderer>().ToList<MeshRenderer>();
        skinmesh = GetComponentsInChildren<SkinnedMeshRenderer>().ToList<SkinnedMeshRenderer>();
    }

    public override void TakeDamage(float damage, Transform hitPos = null)
    {
        health = health - damage;
        healthbar.SetHealth(health, maxHealth);
        StartCoroutine(Flash(flashSpeed));
        if (health <= 0)
        {
            Die();
        }
    }

    private IEnumerator Flash(float time)
    {
        List<Material> meshMaterial = new List<Material>();
        meshMaterial.Clear();
        for(int i = 0; i < mesh.Count; i++)
        {
            meshMaterial.Add(mesh[i].material);
            mesh[i].material = flashMaterial;
        }

        List<Material> skinMeshMaterial = new List<Material>();
        skinMeshMaterial.Clear();
        for (int i = 0; i < skinmesh.Count; i++)
        {
            skinMeshMaterial.Add(skinmesh[i].material);
            skinmesh[i].material = flashMaterial;
        }


        yield return new WaitForSeconds(flashSpeed);

        for (int i = 0; i < mesh.Count; i++)
        {
            mesh[i].material = meshMaterial[i];
        }
        for (int i = 0; i < skinmesh.Count; i++)
        {
            skinmesh[i].material = skinMeshMaterial[i];
        }

    }

    public void LoadData(GameData data)
    {
        EnemyData enemyData = new EnemyData();
        if (data.enemies.TryGetValue(id, out enemyData))
        {
            isDestroyed = enemyData.isDestroyed;
            transform.position = enemyData.position;
            gameObject.SetActive(!isDestroyed);
            health = maxHealth;
        }
    }

    public void SaveData(GameData data)
    {
        if(data.enemies.ContainsKey(id))
        {
            data.enemies.Remove(id);
        }
        EnemyData enemyData = new EnemyData();
        enemyData.isDestroyed = isDestroyed;
        enemyData.position = transform.position;
        data.enemies.Add(id, enemyData);
    }
    public void ReloadData(GameData data)
    {
        EnemyData enemyData = new EnemyData();
        if (data.enemies.TryGetValue(id, out enemyData))
        {
            isDestroyed = enemyData.isDestroyed;
            transform.position = enemyData.position;
            gameObject.SetActive(!isDestroyed);
            health = maxHealth;
        }
    }
}

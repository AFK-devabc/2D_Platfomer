using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyHealth : Health
{
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


}

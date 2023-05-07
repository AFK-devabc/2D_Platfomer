using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{

    private PlayerController playerController;

    [Header("----------Collision----------")]

    [SerializeField] private float collisionForce;
    [SerializeField] private float stopControllerTime;
    [SerializeField] private float invulnerableTime;


    private List<Material> mat;
    private Color[] colors = { Color.white, Color.red };
    private float flashSpeed = 0.1f;

    private void Start()
    {
        health = maxHealth;
        playerController = gameObject.GetComponent<PlayerController>();

        MeshRenderer[] mesh = GetComponentsInChildren<MeshRenderer>();
        SkinnedMeshRenderer[] skinmesh = GetComponentsInChildren<SkinnedMeshRenderer>();

        mat = new List<Material>();
        mat.Clear();
        foreach (MeshRenderer meshRenderer in mesh)
        {
            mat.Add( meshRenderer.material);
        }
        foreach (SkinnedMeshRenderer meshRenderer in skinmesh)
        {
            mat.Add(meshRenderer.material);
        }

    }

    public override void TakeDamage(float damage, Transform hitPos = null)
    {
        StartCoroutine(playerController.StopInputActions(stopControllerTime));
        if (hitPos)
        {
            Vector2 pushForce = new Vector2(hitPos.position.x - transform.position.x > 0 ?
                                            -collisionForce : collisionForce
                                            , collisionForce);
            playerController.SetVelocity(pushForce);
        }
            StartCoroutine(InVulnerableCount(invulnerableTime));
        StartCoroutine(Flash(invulnerableTime, flashSpeed));


        base.TakeDamage(damage);
    }
    private IEnumerator InVulnerableCount(float time)
    {
        Physics2D.IgnoreLayerCollision(gameObject.layer, 6);
        yield return new WaitForSeconds(time);

        Physics2D.IgnoreLayerCollision(gameObject.layer, 6, false);
    }
    private IEnumerator Flash(float time, float intervalTime)
    {
        float elapsedTime = 0f;
        int index = 0;
        List<Color> meshColor = new List<Color>();

        for(int i =0; i<mat.Count; i++)
        {
            meshColor.Add(mat[i].color);
        }
        while (elapsedTime < time)
        {
            foreach (Material mat in mat)
            {
                mat.color = colors[index % 2];
            }

            elapsedTime += intervalTime;
            index++;
            yield return new WaitForSeconds(intervalTime);
        }
        for (int i = 0; i < mat.Count; i++)
        {
            mat[i].color = meshColor[i];
        }
    }

}
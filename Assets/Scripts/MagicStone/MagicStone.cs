using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Animator))]


public class MagicStone : MonoBehaviour, IDataPersistence
{
    [SerializeField] private string id;

    [ContextMenu("Generate ID")]
    private void GenerateID()
    {
        id = System.Guid.NewGuid().ToString();
    }

    [SerializeField] private int ability;

    [SerializeField] private GameObject particle;

    [SerializeField] private GameObject unlockMessage;
    [SerializeField] private GameObject skillMessage;
    private bool canInteract = false;
    private bool isActivated = false;

    private Animator animator;

    private InteractWithStone interactActions;
    private Transform player;

    private void Awake()
    {
        interactActions = new InteractWithStone();
        interactActions.InteractMap.InteractItem.performed += InteracAction;
        animator = GetComponent<Animator>();
    }


    private void OnEnable()
    {
        interactActions.InteractMap.Enable();

    }

    private void OnDisable()
    {
        interactActions.InteractMap.Disable();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            if (!isActivated)
                unlockMessage.SetActive(true);
            canInteract = true;
            player = collision.transform;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {

            unlockMessage.SetActive(false);
            skillMessage.SetActive(false);
            canInteract = false;
          
        }
    }

    private void InteracAction(InputAction.CallbackContext context)
    {
        if (canInteract && !isActivated)
        {
            animator.SetBool("activate", true);
            isActivated = true;
            unlockMessage.SetActive(false);
            skillMessage.SetActive(true);
            particle.SetActive(true) ;
            player.GetComponent<PlayerController>().UnlockAbility(ability);


            DataPersistenceManager.instance.SaveGame();
        }
    }

    public void LoadData(GameData data)
    {
        if (data.abilityStones.TryGetValue(id, out isActivated))
        {
            animator.SetBool("activate", true);
        }
    }

    public void SaveData(GameData data)
    {
        if (data.abilityStones.ContainsKey(id))
        {
            data.abilityStones.Remove(id);
        }

        data.abilityStones.Add(id, isActivated);
    }
    public void ReloadData(GameData data)
    {
        if (data.abilityStones.TryGetValue(id, out isActivated))
        {
            animator.SetBool("activate", true);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Animator))]


public class MagicStone : MonoBehaviour
{

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
           player.GetComponent<PlayerController>().UnlockAbility(ability) ;
        }
    }
}

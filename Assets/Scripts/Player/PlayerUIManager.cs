using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
  [SerializeField]   private PlayerHealth playerHealth;
 
   [SerializeField] private GameObject heartImg;
    private List<Image> healthList;

   
    private void Start()
    {
        healthList = new List<Image>();
        healthList.Clear();
        playerHealth.OnHealthChange += UpdateHealthUI;
        playerHealth.OnTotalHealthChange += UpdateTotalHealthUI;

        for (int i = 0;i < playerHealth.MaxHealth; i++)
        {
            GameObject h = Instantiate(heartImg, this.transform);
            healthList.Add(h.transform.GetChild(0).GetComponent<Image>());
        }
    }

    private void UpdateHealthUI()
    {
         int  heartFill = (int)playerHealth.CurentHealth;
        foreach(Image image in healthList)
        {
            image.fillAmount = heartFill;
            heartFill -= 1;
        }
    }
    private void UpdateTotalHealthUI()
    {
        foreach(Image image in healthList)
        {
            Destroy(image.transform.parent.gameObject);
        }
        healthList.Clear();

        for (int i = 0; i < playerHealth.MaxHealth; i++)
        {
            GameObject h = Instantiate(heartImg, this.transform);
            healthList.Add(h.transform.GetChild(0).GetComponent<Image>());
            
        }
        UpdateHealthUI();
    }
}

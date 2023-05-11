using UnityEngine;
using UnityEngine.UI;

public class HealthbarBehavior : MonoBehaviour
{

    [SerializeField] private GameObject slider;

    //[SerializeField] private Color low;

    //[SerializeField] private Color high;

    //[SerializeField] private Vector3 offSet;

    [SerializeField] private Image healthBar;
    [SerializeField] private Image healthAni;

    public void SetHealth(float health, float maxHealth)
    {
        slider.SetActive(health < maxHealth);
        //slider.value = health;
        //slider.maxValue = maxHealth;
        healthBar.fillAmount = (float)health / maxHealth;  
        //slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(low, high, slider.normalizedValue);
    }

    private void FixedUpdate()
    {
        healthAni.fillAmount = Mathf.Lerp(healthAni.fillAmount, healthBar.fillAmount, Time.fixedDeltaTime );
    }
}

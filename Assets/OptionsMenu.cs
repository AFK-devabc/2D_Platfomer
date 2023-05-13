using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    private Button button;
    // Start is called before the first frame update
    void Start()
    {
        button = transform.GetChild(0).GetComponent<Button>();
        button.interactable = false;
    }

    public void SettingsButtonClick()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

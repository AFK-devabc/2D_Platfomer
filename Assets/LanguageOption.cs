using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LanguageOption : MonoBehaviour
{
    [SerializeField] List<string> languages;
    [SerializeField] TextMeshProUGUI text;
    int currentIndex = 0;

    public void Next()
    {
        if(currentIndex < languages.Count-1)
        {
            currentIndex++;
            text.text = languages[currentIndex];
        }
    }
        

    public void Previous()
    {
        if(currentIndex > 0)
        {
            currentIndex--;
            text.text = languages[currentIndex];
        }
    }
}

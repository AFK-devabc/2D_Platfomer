using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KetBindManager : MonoBehaviour
{
    private Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();
    [SerializeField] TextMeshProUGUI UP, DOWN, LEFT, RIGHT, ATTACK, SLASH, JUMP;

    private GameObject currentKey;
    // Start is called before the first frame update
    void Start()
    {
        keys.Add("UP", KeyCode.W);
        keys.Add("DOWN", KeyCode.S);
        keys.Add("LEFT", KeyCode.A);
        keys.Add("RIGHT", KeyCode.D);
        keys.Add("ATTACK", KeyCode.Z);
        keys.Add("SLASH", KeyCode.X);
        keys.Add("JUMP", KeyCode.Space);

        UP.text = keys["UP"].ToString();
        DOWN.text = keys["DOWN"].ToString();
        LEFT.text = keys["LEFT"].ToString();
        RIGHT.text = keys["RIGHT"].ToString();
        ATTACK.text = keys["ATTACK"].ToString();
        SLASH.text = keys["SLASH"].ToString();
        JUMP.text = keys["JUMP"].ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnGUI()
    {
        if(currentKey != null)
        {
            Event e = Event.current;
            if(e.isKey ) {
                keys[currentKey.name] = e.keyCode;
                currentKey.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = e.keyCode.ToString();
                currentKey.GetComponent<Outline>().enabled = false;
                currentKey = null;
            }
        }
    }

    public void keyChange(GameObject clicked)
    {
        if(currentKey! != null)
        {
            currentKey.GetComponent<Outline>().enabled = false;
            currentKey = null;
        }
        currentKey = clicked;  
        currentKey.GetComponent<Outline>().enabled = true;
    }
}

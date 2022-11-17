using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StateUIUpdater : MonoBehaviour
{
    public static StateUIUpdater Instance;
    [SerializeField] TMP_Text _currentStateText;
    [SerializeField] TMP_Text _nextStateText;
    string _currentT = "Standart";
    string _currentC = "Blue";
    [SerializeField] Image CurrentColor;
    private void Awake()
    {
        Instance = this;
    }
    public void setGunColor(string color)
    {
        if (color == "Blue")
            CurrentColor.color = Color.blue;
        else if (color == "Red")
            CurrentColor.color = Color.red;
        else if (color == "Green")
            CurrentColor.color = Color.green;

    }

    public void setTexts(string color, string type)
    {
        _currentStateText.text = "CurrentBall:" + _currentT + " CurrentColor:" + _currentC;
        _currentT = type;
        _currentC = color;
        _nextStateText.text = "NextBall:" + type + " NextColor" + color;
    }
}

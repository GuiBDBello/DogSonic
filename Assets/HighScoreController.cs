using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreController : MonoBehaviour
{
    public Text HighScoreLevel01;
    public Text HighScoreLevel02;
    public Text HighScoreLevel03;
    public Text HighScoreLevel04;

    private void Start()
    {
        HighScoreLevel01.text = "High Score: " + PlayerPrefs.GetFloat(Scenes.Level01);
        HighScoreLevel02.text = "High Score: " + PlayerPrefs.GetFloat(Scenes.Level02);
        HighScoreLevel03.text = "High Score: " + PlayerPrefs.GetFloat(Scenes.Level03);
        HighScoreLevel04.text = "High Score: " + PlayerPrefs.GetFloat(Scenes.Level04);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    public string scoreText;

    void Update()
    {
        GetComponent<Text>().text=PlayerPrefs.GetInt(scoreText)+"";
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public int score;
    public Text scoreText;
    // Start is called before the first frame update
    void Update()
    {
        scoreText.text = score.ToString();
    }
}

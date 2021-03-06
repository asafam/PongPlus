﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSpeedBoost : MonoBehaviour
{
    public GameObject GameSpeedText;
    

    void OnCollisionEnter(Collision other)
    {
        //If the double speed is on - turn it off.
        if (GameSpeedText.activeSelf) {
            GameSpeedText.SetActive(false);
            Time.timeScale = 1f;
            transform.localScale += new Vector3(0,0,3);
        } 
        else //If the double speed is off - turn it on.
        {
             GameSpeedText.SetActive(true);
             Time.timeScale = 1.5f;
             transform.localScale -= new Vector3(0,0,3);
        }
    }
}

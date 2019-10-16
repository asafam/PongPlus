﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public static int maxScore = 10;
    public int numPlayers = 2;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Creating game...");
        // create players
        numPlayers = Math.Max(1, numPlayers);

        // assign players to pedals and borders
        GameObject[] playerGameObjects = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < playerGameObjects.Length; i++)
        {
            GameObject playerGameObject = GameObject.Find("Player" + i);

            Player player = playerGameObject.GetComponent<Player>();
            player.playerName = player.playerName != null ? player.playerName : "Player" + i;
            player.GetComponent<Health>().OnHealthPctChanged += HandleHealthPctChanged;

            GameObject[] pedalGameObjects = GameObject.FindGameObjectsWithTag("Paddle");
            GameObject pedalGameObject = Array.Find(pedalGameObjects, pgo => pgo.GetComponent<Pedal>().player.playerName == player.playerName);

            if (i < numPlayers)
            {
                player.mode = PlayerMode.Human;
                pedalGameObject.AddComponent<PedalMovement>();
                PedalMovement pedalMovement = pedalGameObject.GetComponent<PedalMovement>();
                // pedalMovement.keyboardInputKey = player.keyboardInputKey;
                Debug.Log("Assign human player #" + i.ToString());
            }
            else
            {
                player.mode = PlayerMode.Computer;
                pedalGameObject.AddComponent<PedalMovementComputer>();
                Debug.Log("Assign computer player #" + i.ToString());
            }
        }
    }

    public void OnPlayerBorderCollisionEnter(PlayerBorder border, Ball ball)
    {
        ball.transform.position = new Vector3(0f, 1f, 0f); //move the ball to the middle
        ball.GetComponent<Rigidbody>().velocity = Vector3.zero; //stopping the ball
        ball.speed = 10; //resetting the requested speed to normal
        Vector2 randomValues = UnityEngine.Random.insideUnitCircle; //generetes random X Y
        while (randomValues.y < 0.3)
        {
            Debug.Log("rolled " + randomValues.y + " for ball's new z - rerolling");
            randomValues = UnityEngine.Random.insideUnitCircle; //prevent the ball from moving too vertical and not towards the players
        }
        Vector3 startVel = new Vector3(randomValues.x, 0, randomValues.y); //creates a vector3 from the random values (x<-x, y<-0, z<-y)
        ball.GetComponent<Rigidbody>().AddForce(startVel.normalized * ball.speed, ForceMode.Impulse);
        // IncrementScore(ball.hitter, ball.points);
    }

    public void HandleHealthPctChanged(Player player, float pct)
    {

    }

    public void IncrementScore(Player player, int points)
    {
        // if (!player)
        // {
        //     Debug.LogError("Player object is missing...");
        //     return;
        // }
        
        // player.GetComponent<HpBar>().ReduceHP(points);
        // Debug.Log("Player " + player.playerName + " scored " + points.ToString() + " points!");
        // GameObject[] scoresGO = GameObject.FindGameObjectsWithTag("Score");
        
        // if (scoresGO == null || scoresGO.Length == 0) {
        //     Debug.LogError("Score GameObject is missing...");
        //     return;
        // }

        // GameObject scoreGO = Array.Find(scoresGO, sgo => sgo.GetComponent<Score>() && sgo.GetComponent<Score>().player.playerName == player.playerName);
        // Score score = scoreGO.GetComponent<Score>();
        // score.score += points;

        if (player.GetComponent<HpBar>().getFill() <= 0)
        {
            EndGame(player);
        }
    }

    public static void EndGame(Player player)
    {

        if (player.playerName == "Red") {
            SceneManager.LoadScene("RedWins");
        } 
        else if (player.playerName == "Blue")
        {
            SceneManager.LoadScene("BlueWins");
        }
        else 
        {
            Debug.LogError("Winner not detected!");
        }
    }
}

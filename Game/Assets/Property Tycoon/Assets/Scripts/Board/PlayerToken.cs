﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerToken : MonoBehaviour 
{
    [Tooltip("The tile the PlayerToken will start on.")]
    public Tile startingTile;
    public DiceManager diceManager;
    public Text valueText;

    Tile currentTile;

    Tile[] moveQueue;
    int moveQueueIndex;

    Vector3 targetPosition;
    Vector3 velocity;

    Tile finalTile;
	
    void Awake()
    {
        finalTile = startingTile;
        targetPosition = this.transform.position;
    }

	
	void Update () 
    {
        if (Vector3.Distance(this.transform.position, targetPosition) > 0.03f)
        {
            this.transform.position = Vector3.SmoothDamp(this.transform.position, targetPosition, ref velocity, 0.2f);
        }
        else
        {
            if (moveQueue != null && moveQueueIndex < moveQueue.Length)
            {
                Tile nextTile = moveQueue[moveQueueIndex];
                SetNewTargetPosition(nextTile.transform.position);
                moveQueueIndex++;
            }
        }
	}

    public void MovePlayerToken()
    {
        int spacesToMove = diceManager.totalValue;
        valueText.text = "Value: " + spacesToMove.ToString();

        if (spacesToMove == 0)
        {
            return;
        }

        moveQueue = new Tile[spacesToMove];

        for (int i = 0; i < spacesToMove; i++)
        {
            finalTile = finalTile.nextTile;
            moveQueue[i] = finalTile;
        }
        moveQueueIndex = 0;
    }


    void SetNewTargetPosition(Vector3 pos)
    {
        targetPosition = pos;
        velocity = Vector3.zero;
    }

  
   
}
﻿using UnityEngine;
using System.Collections;

public class Player {

    private int playerNumber;
    public int PlayerNumber { get { return playerNumber; } }

    private Color playerColor;
    public Color PlayerColor { get { return playerColor; } }

    private int score;
    public int Score { get { return score; } }

    private int[] roundScores = new int[4];
    public int[] RoundScores
    {
        get { return roundScores; }
    }

    public Player(int controller)
    {
        playerNumber = controller;
        score = 0;
    }

    public void SetColor(Color color)
    {
        playerColor = color;
    }

    public void ModifyScore(int val)
    {
        score += val;
    }
}

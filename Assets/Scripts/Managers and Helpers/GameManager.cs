﻿using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager instance;
    public int caughtScore = 10;
    public int passedScore = 1;
    private Team team1 = new Team();
    private Team team2 = new Team();

    private Timer timer;
    private Score score;
    private int round;

    void Awake()
    {
        instance = this;
    }

	// Use this for initialization
	void Start()
    {
        timer = GetComponent<Timer>();
        score = GetComponent<Score>();
        round = 0;

        int firstTeam = Random.Range(1,2);

        if( firstTeam == 1 )
            team1.AreBears = true;
        else
            team2.AreBears = true;

        StartCoroutine("roundStartCoroutine");
    }
	
    private IEnumerator roundStartCoroutine()
    {
        yield return new WaitForSeconds(1);
        StartRound();
    }

	// Update is called once per frame
	void Update()
    {
	
	}

    public void StartRound()
    {
        round++;

        timer.ResetTimer();
        timer.StartTimer();

		ObjectPool.instance.GetObject("Fish", true);
    }

    public void EndRound()
    {
        timer.StopTimer();

        //Check for a winner
        Score.AnimalTeam animalWinner = score.Winner();

        Debug.Log(animalWinner);

        if( animalWinner == Score.AnimalTeam.BOTH )
        {
            team1.Wins++;
            team2.Wins++;
        }

        if( animalWinner == Score.AnimalTeam.BEAR && team1.AreBears ||
            animalWinner == Score.AnimalTeam.SALMON && team2.AreBears )
        {
            team1.Wins++;
        }
        else
        {
            team2.Wins++;
        }
    }

    public void SalmonCaught()
    {
        score.ModifyScore(Score.AnimalTeam.BEAR, 8);
        if( score.GoalReached() )
        {
            EndRound();
        }

        if( timer.RemainingTime > 0 )
            ObjectPool.instance.GetObject("Fish");
    }

    public void SalmonFlee()
    {
        score.ModifyScore(Score.AnimalTeam.SALMON, 1);
        if( score.GoalReached() )
        {
            EndRound();
        }
        if( timer.RemainingTime > 0 )
            ObjectPool.instance.GetObject("Fish");
    }

    public void SalmonCrash()
    {
        score.ModifyScore(Score.AnimalTeam.BEAR, 4);
        if( score.GoalReached() )
        {
            EndRound();
        }

        if( timer.RemainingTime > 0 )
            ObjectPool.instance.GetObject("Fish");
    }
}

using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameScoreManager : MonoBehaviour
{
    DatabaseReference mDatabase;
    string UserId;
    int score = 0;

    int i = 0;

    public float timeRemaining = 10;
    public bool timerIsRunning = false;
    public TMP_Text countdownTimer;
    public GameObject clickableObj;
    public GameObject scorePanel;

    public List<TextMeshProUGUI> scoreList;

    void Start()
    {
        mDatabase = FirebaseDatabase.DefaultInstance.RootReference;
        UserId = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
        GetUserScore();
    }
    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                countdownTimer.text = (timeRemaining.ToString() + " seconds");
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
                Destroy(clickableObj);
                countdownTimer.text = ("Time has run out!");
                WriteNewScore(score);
                GetUsersHighestScores();
                scorePanel.SetActive(true);
            }
        }

    }
    public void ClickBomb()
    {
        score++;
        Debug.Log(score);
    }
    public void WriteNewScore(int bombScore)
    {
        mDatabase.Child("users").Child(UserId).Child("score").SetValueAsync(bombScore);
    }
    public void GetUserScore()
    {
        FirebaseDatabase.DefaultInstance
            .GetReference("users/" + UserId + "/score")
            .GetValueAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.Log(task.Exception);
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    Debug.Log("Score: " + snapshot.Value);
                    score = (int)snapshot.Value;
                    GameObject.Find("LabelScore").GetComponent<TMPro.TMP_Text>().text = "Score: " + score;
                }
            });
    }
    public void GetUsersHighestScores()
    {
        FirebaseDatabase.DefaultInstance
            .GetReference("users").OrderByChild("score").LimitToLast(5)
            .GetValueAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.Log(task.Exception);
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                foreach (var userDoc in (Dictionary<string, object>)snapshot.Value)
                    {
                        Debug.Log($"{userDoc}");
                        var userObject = (Dictionary<string, object>)userDoc.Value;
                        scoreList[i++].text = userObject["username"] + " : " + userObject["score"];
                        Debug.Log(userObject["username"] + " : " + userObject["score"]);
                    }
                }
            });
    }
}
public class UserData 
{
    public int score;
    public string username;
}

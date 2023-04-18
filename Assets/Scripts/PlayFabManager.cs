using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;

public class PlayFabManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Login();
    }

    void Login()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnSuccess,OnError);
    }

    void OnSuccess(LoginResult result)
    {
        Debug.Log("Successful login/ account creation");
    }

    static void OnError(PlayFabError error)
    {
        Debug.Log("Error while logging in/ create account");
        Debug.Log(error.GenerateErrorReport());
    }

    public static void SendLeaderboard( int score)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = "HighScore",
                    Value = score
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
    }

     static void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Successful Leaderboard sent!!!");
    }

     public void GetLeaderboard()
     {
         var request = new GetLeaderboardRequest
         {
             StatisticName = "HighScore",
             StartPosition = 0,
             MaxResultsCount = 10

         };
         PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);
     }

     void OnLeaderboardGet(GetLeaderboardResult result)
     {
         foreach (var item in result.Leaderboard)
         {
             Debug.Log(item.Position + " " + item.PlayFabId + " " + item.StatValue);
         }
         {
             
         }
     }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Dan.Main;

public class Leaderboard : MonoBehaviour
{
    [SerializeField]
    private List<TMP_Text> names;
    [SerializeField]
    private List<TMP_Text> scores;
    [SerializeField]
    private TMP_Text CTS;
    [SerializeField]
    private TMP_InputField inputname;

    public GameObject inputButton;
    public GameObject namefield;
    public GameObject endText;

    private string public_key = "5b2b74343fd0690cdeefffbb6d01ff12e2d8a04c1029e4f4febb48a319f85b46";
    public void get_leaderboard()
    {
        LeaderboardCreator.GetLeaderboard(public_key, ((msg) =>
        {
            int L = (msg.Length < names.Count) ? msg.Length : names.Count;
            for (int i = 0; i < L; ++i)
            {
                names[i].text = msg[i].Username;
                scores[i].text = msg[i].Score.ToString();
            }
        }));
    }

    public void set_leaderboard_entry(string username, int score)
    {
        LeaderboardCreator.UploadNewEntry(public_key, username, score, ((msg) =>
        {
            get_leaderboard();
        }));
    }

    public void submit_score()
    {
        set_leaderboard_entry(inputname.text, int.Parse(CTS.text));
    }

    public void wake_up()
    {
        inputButton.SetActive(true);
        namefield.SetActive(true);
        endText.SetActive(true);

       // for (int i = 0; i < names.Count; ++i)
        //{
          //  names[i].enabled = true;
           // scores[i].enabled = true;
        //}
        get_leaderboard();
    }

    
}

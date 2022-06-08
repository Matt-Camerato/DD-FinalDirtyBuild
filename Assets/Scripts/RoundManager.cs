using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RoundManager : MonoBehaviour
{
    static public int roundsPlayed = 0;
    static public int roundsWon = 0;
    static public int roundsLost = 0;

    public Text roundText;
    public Text winsText;
    public Text lossesText;

    static public void EndRound()
    {
        if (roundsPlayed >= 5 || roundsWon >= 3 || roundsLost >= 3)
        {
            SceneManager.LoadScene(0); //return to daycare if won 3 times, lost 3 times, or played 5 times

            //reset static variables
            roundsWon = 0;
            roundsLost = 0;
            roundsPlayed = 0;
        }
        else SceneManager.LoadScene(1); //else go back to comp setup screen
    }

    public void Update()
    {
        roundText.text = "ROUND " + (roundsPlayed + 1).ToString();
        winsText.text = "wins: " + roundsWon.ToString();
        lossesText.text = "losses: " + roundsLost.ToString();
    }
}

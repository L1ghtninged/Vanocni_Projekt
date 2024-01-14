using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManagerScript : MonoBehaviour
{

    public void ClearPlayers()
    {
        LogicScript.players.Clear();
    }
    public void LoadTwoPlayers()
    {
        ClearPlayers();
        LogicScript.players.Add(new Player("Player_1"));
        LogicScript.players.Add(new Player("Player_2"));
        SceneManager.LoadScene("SampleScene");
    }
    public void LoadThreePlayers()
    {
        ClearPlayers();
        LogicScript.players.Add(new Player("Player_1"));
        LogicScript.players.Add(new Player("Player_2"));
        LogicScript.players.Add(new Player("Player_3"));
        SceneManager.LoadScene("SampleScene");
    }
    public void LoadFourPlayers()
    {
        ClearPlayers();
        LogicScript.players.Add(new Player("Player_1"));
        LogicScript.players.Add(new Player("Player_2"));
        LogicScript.players.Add(new Player("Player_3"));
        LogicScript.players.Add(new Player("Player_4"));
        SceneManager.LoadScene("SampleScene");
    }


}

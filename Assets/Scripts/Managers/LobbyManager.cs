using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;


public class LobbyManager : MonoBehaviour
{
    public TMP_InputField player1Name;
    public TMP_InputField player2Name;
    public GameObject startButton;
    Canvas hintScreen;

    bool player1NameIn = false;
    bool player2NameIn = false;
    // Talk to GameData and pass in the player names.
    // Only enable start game button when both players have text input

    // Start is called before the first frame update
    void Start()
    {
        startButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (player1NameIn && player2NameIn)
        {
            startButton.SetActive(true);
        }
    }

    public void SetPlayer1Name()
    {
        GameManager.GMInstance.UpdatePlayerOneName(player1Name.text);
        player1NameIn = true;
    }
    public void SetPlayer2Name()
    {
        GameManager.GMInstance.UpdatePlayerTwoName(player2Name.text);
        //GameData.playerOneNameData = player1Name.text;
        player2NameIn = true;
    }

    public void NextButton() // unused
    {
        hintScreen.enabled = true;
    }
    public void StartNewGame()
    {
        MySceneManager.MSMInstance.LoadNewScene(2); // Level 1
    }
    public void ExitToMainButton()  // back button
    {
        PhotonNetwork.Disconnect();
        MySceneManager.MSMInstance.LoadNewScene(0);
    }
}

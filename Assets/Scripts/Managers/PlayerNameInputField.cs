using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class PlayerNameInputField : MonoBehaviourPunCallbacks
{
    const string playerNamePrefKey = "PlayerName";

    TMP_InputField inputField;

    // Start is called before the first frame update
    void Start()
    {
        string defaultName = string.Empty;
        if (inputField != null)
        {
            inputField = GetComponent<TMP_InputField>();
            if (PlayerPrefs.HasKey(playerNamePrefKey))
            {
                defaultName = PlayerPrefs.GetString(playerNamePrefKey);
                inputField.text = defaultName;
            }
            
        }
        PhotonNetwork.NickName = defaultName;
    }
    
    public void SetPlayerName(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            Debug.LogError("Player Name is null or empty");
            return;
        }
        PhotonNetwork.NickName = value;

        PlayerPrefs.SetString(playerNamePrefKey, value);
    }
    
}

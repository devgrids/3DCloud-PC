using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
//using TMPro;

public class LoginManager : MonoBehaviourPunCallbacks
{
    //public TMP_InputField PlayerName_InputField;
    public InputField PlayerName_InputField;

    #region UNITY Methods

    void Start()
    {
        //PhotonNetwork.ConnectUsingSettings();
    }

    void Update()
    {
    }

    #endregion

    #region UI Callback Methods

    public void ConnectAnonymously()
    {
        PhotonNetwork.GameVersion = "0.1";
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Se va a conectar al servidor master");
    }

    public void ConnectToPhotonServer()
    {

        PhotonNetwork.NickName = "Leonidas";
        PhotonNetwork.ConnectUsingSettings();

    }
    #endregion

    #region Photon Callback Methods
    public override void OnConnected()
    {
        Debug.Log("Se ha llamado al servidor");
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        Debug.Log("Se ha conectado al servidor master");
        PhotonNetwork.LoadLevel("Home");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Problema al conectar al servidor");
    }

    #endregion
}

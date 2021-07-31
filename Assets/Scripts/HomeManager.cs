using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeManager : MonoBehaviourPunCallbacks
{
    // Como pueden haber varios jugadores creamos un gameobject serializable
    [SerializeField]
    GameObject genericPlayer;

    [SerializeField]
    Vector3 spawnPlayer;

    void Start()
    {
        if (PhotonNetwork.IsConnected)
        {
            if (genericPlayer != null)
            {
                // Instanciar al jugador para todos los jugadores en la sala en una posici�n al azar al inicio de la escena
                PhotonNetwork.Instantiate(genericPlayer.name, spawnPlayer, Quaternion.identity);
            }
        }
    }

    void Update()
    {

    }

    public override void OnJoinedRoom()
    {
        
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        //PhotonNetwork.PlayerList;
    }

    public void debugGUI()
    {
        Debug.Log("Evento Debug Activado");
    }

}

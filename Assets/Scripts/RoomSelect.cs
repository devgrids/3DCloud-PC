using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomSelect : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Text textNombre;
    [SerializeField]
    private Text textCapacidad;
    [SerializeField]
    private Image imagen;

    private string roomName;
    private int roomSize;
    private int playerCount;
    private int indexImagen;

    // Método enlazado con el botón que une al jugador a una sala
    public void JoinRoomOnClick()
    {
        //PhotonNetwork.JoinRoom(roomName);
        CreateRoom();
        //StartGameOnClick();

        Debug.Log("ROOM NAME: " + roomName);
        Debug.Log("ROOM SIZE: " + roomSize);
        Debug.Log("PLAYER COUNT: " + playerCount);
        Debug.Log("INDEX IMAGEN: " + indexImagen);
    }

    // Se llama por el controlador de lobbys para cada nueva sala que se añade a la lista
    public void SetRoom(string nombreIngresado, int capacidad, int cantidad, int indexImagen)
    {
        this.roomName = nombreIngresado;
        roomSize = capacidad;
        playerCount = cantidad;
        this.indexImagen = indexImagen;
        textNombre.text = nombreIngresado;
        textCapacidad.text = cantidad + "/" + capacidad;
        imagen.sprite = LobbyManager.sharedInstance.imageList[indexImagen];
    }

    public void StartGameOnClick()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            //PhotonNetwork.CurrentRoom.IsOpen = false;
            // Como se ha puesto la sincronización automática de escenas, el master 
            // notifica automáticamente a los demás jugadores antes de carga la escena
            PhotonNetwork.LoadLevel(indexImagen + 1);
            Debug.Log("Eres cliente master");
        }
    }

    public void CreateRoom()
    {
        Debug.Log("Creando nueva sala: " + roomName);
        RoomOptions roomOptions = new RoomOptions()
        {
            IsVisible = true,
            IsOpen = true,
            MaxPlayers = byte.Parse(roomSize.ToString())
        };
        PhotonNetwork.CreateRoom(roomName, roomOptions); // Creación de una nueva sala
    }

    public override void OnJoinedRoom()
    {
        //PhotonNetwork.LeaveLobby();
        // 4
        Debug.Log("Te uniste a la Sala: " + PhotonNetwork.CurrentRoom.Name);
        Debug.Log("La sala cuenta con: " + PhotonNetwork.CurrentRoom.PlayerCount + " jugador(es).");
        //PhotonNetwork.LoadLevel(this.indexImagen + 1);
        StartGameOnClick();

    }
}

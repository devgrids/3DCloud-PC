using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomSelect : MonoBehaviour
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

    // M�todo enlazado con el bot�n que une al jugador a una sala
    public void JoinRoomOnClick()
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    // Se llama por el controlador de lobbys para cada nueva sala que se a�ade a la lista
    public void SetRoom(string nombreIngresado, int capacidad, int cantidad)
    {
        this.roomName = nombreIngresado;
        roomSize = capacidad;
        playerCount = cantidad;
        textNombre.text = nombreIngresado;
        textCapacidad.text = cantidad + "/" + capacidad;
    }
}

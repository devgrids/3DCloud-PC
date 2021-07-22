using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public static LobbyManager sharedInstance;

    private List<RoomInfo> roomLista;
    private Transform contenedorRoom; // contenedor para las salas disponibles
    public GameObject prefabRoomList; //Prefab para mostrar cada sala en el lobby

    #region UNITY Methods
    private void Awake()
    {
        sharedInstance = this;
    }
    #endregion

    #region Photon Callback Methods

    // Si la conexión fue establecida
    public override void OnConnectedToMaster()
    {
        // Esto indica que todos los jugadores de la sala usarán la misma el jugador master (el que crea la sala) o Master Client
        PhotonNetwork.AutomaticallySyncScene = true;
        roomLista = new List<RoomInfo>();
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Curso creado correctamente");
    }

    public override void OnCreateRoomFailed(short returnCode, string message) //si la sala existe
    {
        Debug.Log("Fallo en crear una nueva sala, seguramente ya existe una sala con ese nombre.");
    }

    // Cada vez que se actualiza la lista de salas se llama a este método con la lista de salas
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        //BorrarSalasdeLista();
        // int tempIndex;
        // Loop por cada sala en la lista de salas
        foreach (RoomInfo room in roomList)
        {
            /*
            if (salasenLista != null)
            {
                //Busca el índice de la sala en la lista de salas
                tempIndex = salasenLista.FindIndex(ByName(room.Name)); 
            }
            else
            {
                tempIndex = -1;
            }
            // Quitar de la lista porque ha sido cerrada la sala
            // Si no quedan jugadores en sala se borra la sala
            
            if (tempIndex != -1) 
            {
                salasenLista.RemoveAt(tempIndex);
                Destroy(contenedordeSalas.GetChild(tempIndex).gameObject);
            }
            */
            // Agregar lista de salas porque ya tiene un jugador
            //if (room.PlayerCount > 0) 
            //{
            roomLista.Add(room);
            //ListRoom(room);
            //}
        }
    }

    #endregion

    #region UI Callback Methods

    // Predicate, es un método C# que contiene un set de criterios de búsqueda y devuelve un boolean
    // Está definido en el System namespace 
    // Método que buscan una sala en la lista de salas
    static System.Predicate<RoomInfo> ByName(string name)
    {
        return delegate (RoomInfo room)
        {
            return room.Name == name;
        };
    }

    void BorrarSalasdeLista()
    {
        for (int i = contenedorRoom.childCount - 1; i >= 0; i--)
        {
            Destroy(contenedorRoom.GetChild(i).gameObject);
        }
    }

    void ListRoom(RoomInfo room) //Muestra la nueva lista de salas para la sala actual
    {
        if (room.IsOpen && room.IsVisible)
        {
            //GameObject tempListing = Instantiate(prefabRoomList, contenedorRoom);
            //RoomSelect tempButton = tempListing.GetComponent<RoomSelect>();
            //int capacidad = int.Parse(maxSala.text);
            //tempButton.SetRoom(room.Name, capacidad, room.PlayerCount);
        }
    }

    

    #endregion


}

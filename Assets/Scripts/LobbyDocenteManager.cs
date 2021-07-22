using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class LobbyDocenteManager : MonoBehaviourPunCallbacks
{
    public static LobbyDocenteManager sharedInstance;

    [SerializeField] Text textNombreDocente;
    [SerializeField] InputField inputNombreRoom;
    [SerializeField] InputField inputCapacidadRoom;

    [SerializeField]
    private Transform contenedorRoom; // contenedor para las salas disponibles

    #region UNITY Methods

    private void Awake()
    {
        sharedInstance = this;
    }

    void Start()
    {

    }

    void Update()
    {

    }
    #endregion

    #region UI Callback Methods

    public void SetNombreDocente()
    {
        textNombreDocente.text = dalCuenta.sharedInstance.cuenta.nombres + " " + dalCuenta.sharedInstance.cuenta.apellidos;
    }

    public void CreateRoom()
    {
        Debug.Log("Creando nueva sala: " + inputNombreRoom.text);
        RoomOptions roomOptions = new RoomOptions()
        {
            IsVisible = true,
            IsOpen = true,
            MaxPlayers = byte.Parse(inputCapacidadRoom.text)
        };
        PhotonNetwork.CreateRoom(inputNombreRoom.text, roomOptions); // Creación de una nueva sala
    }

    public void addListaGUI()
    {
        GameObject card = Instantiate(LobbyManager.sharedInstance.prefabRoomList, contenedorRoom);
        RoomSelect scriptRoomSelect = card.GetComponent<RoomSelect>();
        scriptRoomSelect.SetRoom(inputNombreRoom.text, int.Parse(inputCapacidadRoom.text), 1);
        //GameObject tempListing = Instantiate(prefabRoomList, contenedorRoom);
        //RoomSelect tempButton = tempListing.GetComponent<RoomSelect>();
        //int capacidad = int.Parse(maxSala.text);
        //tempButton.SetRoom(room.Name, capacidad, room.PlayerCount);
    }

    #endregion

    #region IEnumerator Callback Methods

    #endregion

    #region Photon Callback Methods


    #endregion





}

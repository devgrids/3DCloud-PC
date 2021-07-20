using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private RoomManager roomManager;

    void Start()
    {
        roomManager = new RoomManager();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        roomManager.OnEnterRoomButtonClicked_Aula_1();
    }



}

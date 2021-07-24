using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviourPunCallbacks
{
    public static PlayerController sharedInstance;

    public PhotonView PV;
    public FirstPersonController scriptPersonController;

    [SerializeField]
    Camera cameraPlayer;

    private void Awake()
    {
        sharedInstance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        scriptPersonController = GetComponent<FirstPersonController>();
        PV = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PV.IsMine)
        {
            scriptPersonController.enabled = true;
            cameraPlayer.enabled = true;
        }
        else
        {
            scriptPersonController.enabled = false;
            cameraPlayer.enabled = false;
        }
    }
}

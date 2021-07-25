using Photon.Pun;
using Photon.Voice.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviourPunCallbacks
{
    public static PlayerController sharedInstance;

    public PhotonView PV;
    public Recorder VoiceRecorder;

    public FirstPersonController scriptPersonController;
    [SerializeField] Camera cameraPlayer;

    private void Awake()
    {
        sharedInstance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        scriptPersonController = GetComponent<FirstPersonController>();
        PV = GetComponent<PhotonView>();
        VoiceRecorder.TransmitEnabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (PV.IsMine)
        {
            scriptPersonController.enabled = true;
            cameraPlayer.enabled = true;
            if(Input.GetKeyDown(KeyCode.P))
            {
                VoiceRecorder.TransmitEnabled = true;
            }
        }
        else
        {
            scriptPersonController.enabled = false;
            cameraPlayer.enabled = false;
            if (Input.GetKeyUp(KeyCode.P))
            {
                VoiceRecorder.TransmitEnabled = false;
            }
            
        }
    }
}

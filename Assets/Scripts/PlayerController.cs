using Photon.Pun;
using Photon.Voice.Unity;
using UnityEngine;


public class PlayerController : MonoBehaviourPunCallbacks
{
    public static PlayerController sharedInstance;

    public PhotonView PV;
    public Recorder VoiceRecorder;

    public FirstPersonController scriptPersonController;
    [SerializeField] Camera cameraPlayer;

    public FirstPersonController player;
    public Animator animator;

    Vector3 inputAxis;

    private void Awake()
    {
        sharedInstance = this;
    }

    void Start()
    {
        scriptPersonController = GetComponent<FirstPersonController>();
        PV = GetComponent<PhotonView>();
        VoiceRecorder.TransmitEnabled = false;
    }

    void Update()
    {
        if (PV.IsMine)
        {
            scriptPersonController.enabled = true;
            cameraPlayer.enabled = true;

            inputAxis = player.inputAxis;

            animator.SetFloat("velocityX", inputAxis.x);
            animator.SetFloat("velocityY", inputAxis.z);

            if (Input.GetKeyDown(KeyCode.P))
            {
                VoiceRecorder.TransmitEnabled = true;
            }

            if (PhotonNetwork.IsMasterClient)
            {
                //Debug.Log("Eres el profesor");
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

    private void FixedUpdate()
    {
        
    }

    public void NextContent()
    {
        if (!PhotonNetwork.IsMasterClient) return;

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        for (int i = 0; i < players.Length; i++)
        {
            players[i].GetComponent<PhotonView>().RPC("NextContentPun", RpcTarget.AllBufferedViaServer);
        }
    }

    [PunRPC]
    public void NextContentPun()
    {
         ClassContent.sharedInstance.NextContent();
    }


    //void OnGUI()
    //{
    //    Debug.Log("gui evento");
    //    GUI.Label(new Rect(new Vector2(20, 20), new Vector2(2,2)), "Game Over");
    //}

}

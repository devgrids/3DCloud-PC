using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyEstudianteManager : MonoBehaviour
{
    public static LobbyEstudianteManager sharedInstance;

    [SerializeField] Text textNombreEstudiante;

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

    public void SetNombreEstudiante()
    {
        textNombreEstudiante.text = dalCuenta.sharedInstance.cuenta.nombres + " " + dalCuenta.sharedInstance.cuenta.apellidos;
        //Debug.Log(LoginManager.sharedInstance.cuenta.nombres);
    }

    #endregion

    #region IEnumerator Callback Methods





    #endregion

    #region Photon Callback Methods


    #endregion



}

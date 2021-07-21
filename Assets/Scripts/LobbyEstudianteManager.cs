using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyEstudianteManager : MonoBehaviour
{
    public static LobbyEstudianteManager sharedInstance;

    [SerializeField] Text textNombreEstudiante;

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

    public void SetNombreEstudiante()
    {
        textNombreEstudiante.text = LoginManager.sharedInstance.cuenta.nombres + " " + LoginManager.sharedInstance.cuenta.apellidos;
        //Debug.Log(LoginManager.sharedInstance.cuenta.nombres);
    }
}

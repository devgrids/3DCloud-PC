using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyDocenteManager : MonoBehaviour
{
    public static LobbyDocenteManager sharedInstance;

    [SerializeField] Text textNombreDocente;

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

    public void SetNombreDocente()
    {
        textNombreDocente.text = LoginManager.sharedInstance.cuenta.nombres + " " + LoginManager.sharedInstance.cuenta.apellidos;
    }
}

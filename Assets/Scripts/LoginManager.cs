using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{
    public InputField inputUser;
    public InputField inputPassword;

    public static LoginManager sharedInstance;
    public Cuenta cuenta;

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

    public void guardarCuenta()
    {
        Cuenta cuenta = new Cuenta();
        cuenta.nickname = "JoseMV";
        cuenta.nombres = "Piero Jose";
        cuenta.password = "cloud";
        cuenta.telefono = "987456123";
        cuenta.tipoCuenta = 0;
        cuenta.apellidos = "Moreno Vásquez";
        cuenta.domicilio = "Barcelona";
        cuenta.email = "piero.jmv.2001@gmail.com";

        string json = JsonUtility.ToJson(cuenta);
        StartCoroutine(IE_guardarCuenta(json));
    }

    public void obtenerCuenta()
    {
        StartCoroutine(IE_obtenerCuenta());
    }

    #endregion

    #region IEnumerator Callback Methods

    IEnumerator IE_guardarCuenta(string jsonCuenta)
    {
        WWWForm form = new WWWForm();
        form.AddField("cuenta", jsonCuenta);

        UnityWebRequest www = UnityWebRequest.Post("http://localhost/3dcloud/controllers/cuenta/guardarCuenta.php", form);
        yield return www.SendWebRequest();

        string res = debugNetwork(www);
        if (res != "Error")
        {
            Debug.Log(res);
        }
    }

    public IEnumerator IE_obtenerCuenta()
    {
        WWWForm form = new WWWForm();
        form.AddField("email", inputUser.text);
        form.AddField("password", inputPassword.text);

        UnityWebRequest www = UnityWebRequest.Post("http://localhost/3dcloud/controllers/cuenta/obtenerCuentaPorEmailAndPassword.php", form);
        yield return www.SendWebRequest();

        string res = debugNetwork(www);
        
        if (res != "Error")
        {
            this.cuenta = JsonUtility.FromJson<Cuenta>(res);
            if (this.cuenta.idCuenta > 0)
            {
                if (this.cuenta.tipoCuenta == 0)
                {
                    GameManager.sharedInstance.SetGameState(GameState.lobbyDocente);
                    LobbyDocenteManager.sharedInstance.SetNombreDocente();
                }
                else if (this.cuenta.tipoCuenta == 1)
                {
                    GameManager.sharedInstance.SetGameState(GameState.lobbyEstudiante);
                    LobbyEstudianteManager.sharedInstance.SetNombreEstudiante();
                }
            }
            else
            {
                inputPassword.text = "";
            }
            
        }
    }

    private string debugNetwork(UnityWebRequest www)
    {
        if (!www.isNetworkError && !www.isHttpError)
            return www.downloadHandler.text;
        return "Error";
    }

    #endregion

    #region Photon Callback Methods


    #endregion

}

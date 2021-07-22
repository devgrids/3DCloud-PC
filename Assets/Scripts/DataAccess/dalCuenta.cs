using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class dalCuenta : MonoBehaviour
{
    public static dalCuenta sharedInstance;
    public Cuenta cuenta;
    
    public InputField inputUser;
    public InputField inputPassword;

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
        Cuenta obj = new Cuenta();
        obj.nickname = "JoseMV";
        obj.nombres = "Piero Jose";
        obj.password = "cloud";
        obj.telefono = "987456123";
        obj.tipoCuenta = 0;
        obj.apellidos = "Moreno Vásquez";
        obj.domicilio = "Barcelona";
        obj.email = "piero.jmv.2001@gmail.com";

        string json = JsonUtility.ToJson(obj);
        StartCoroutine(IE_guardarCuenta(json));
    }

    public void obtenerCuenta()
    {
        StartCoroutine(IE_obtenerCuenta(inputUser.text, inputPassword.text));
    }

    #endregion

    #region IEnumerator Callback Methods

    IEnumerator IE_guardarCuenta(string jsonCuenta)
    {
        WWWForm form = new WWWForm();
        form.AddField("cuenta", jsonCuenta);

        UnityWebRequest www = UnityWebRequest.Post("http://localhost/3dcloud/controllers/cuenta/guardarCuenta.php", form);
        yield return www.SendWebRequest();

        string res = Util.debugNetwork(www);
        if (res != "Error")
        {
            Debug.Log(res);
        }
    }

    public IEnumerator IE_obtenerCuenta(string user, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("email", user);
        form.AddField("password", password);

        UnityWebRequest www = UnityWebRequest.Post("http://localhost/3dcloud/controllers/cuenta/obtenerCuentaPorEmailAndPassword.php", form);
        yield return www.SendWebRequest();

        string res = Util.debugNetwork(www);

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
        }
    }

    #endregion
}

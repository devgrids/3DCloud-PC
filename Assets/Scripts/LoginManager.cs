using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{
    public InputField inputUser;
    public InputField inputPassword;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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

        //WWWForm form = new WWWForm();
        //form.AddField("cuenta", json);

        //UnityWebRequest www = UnityWebRequest.Post("http://localhost/3dcloud/controllers/cuenta/guardarCuenta.php", form);

        //cuenta = JsonUtility.FromJson<Cuenta>(json);

        //UnityWebRequest www = UnityWebRequest.Post(url, formData);

        //www.chunkedTransfer = false;////ADD THIS LINE

        //yield return www.SendWebRequest();
    }

    IEnumerator IE_guardarCuenta(string cuenta)
    {
        WWWForm form = new WWWForm();
        form.AddField("cuenta", cuenta);

        UnityWebRequest www = UnityWebRequest.Post("http://localhost/3dcloud/controllers/cuenta/guardarCuenta.php", form);
        yield return www.SendWebRequest();

        if (!www.isNetworkError && !www.isHttpError)
        {
            // Get text content like this:
            Debug.Log(www.downloadHandler.text);
        }

    }

}

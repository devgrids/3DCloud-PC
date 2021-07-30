using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class dalCurso : MonoBehaviour
{
    public static dalCurso sharedInstance;

    public Curso curso;
    public List<Curso> listaDeCursos;

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

    public void obtenerListaDeCursos()
    {
        StartCoroutine(IE_obtenerListaDeCursos());
    }

    #endregion

    #region IEnumerator Callback Methods



    public IEnumerator IE_obtenerListaDeCursos()
    {
        UnityWebRequest www = UnityWebRequest.Get(Util.BaseUrl + "/3dcloud/controllers/curso/obtenerListaCursos.php");
        yield return www.SendWebRequest();

        String res = Util.debugNetwork(www);

        if (res != Util.Error)
        {
            this.listaDeCursos = Util.getJsonList<Curso>(res);
            foreach(var curso in listaDeCursos)
            {
                LobbyManager.sharedInstance.AddContenedorListaCurso(curso.nombre, curso.capacidad, curso.imagen);
            }    
        }
    }

    #endregion
}

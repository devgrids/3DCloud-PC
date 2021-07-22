using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Util : MonoBehaviour
{
    public static string debugNetwork(UnityWebRequest www)
    {
        if (!www.isNetworkError && !www.isHttpError)
            return www.downloadHandler.text;
        return "Error";
    }
}

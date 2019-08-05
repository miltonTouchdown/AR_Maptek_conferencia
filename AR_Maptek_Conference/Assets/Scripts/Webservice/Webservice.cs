using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Webservice : MonoBehaviour
{
    private static Webservice _instace;
    public static Webservice Instance
    {
        get
        {
            return _instace;
        }
        set
        {
            if (_instace == null)
            {
                _instace = value;
            }
        }
    }

    void Awake()
    {
        if (_instace == null)
        {
            _instace = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        
    }

    public void registerUser(string email, OnResponseCallback response = null)
    {
        response(true, "registro correcto");
        FillUserData();
    }

    public void getUserData(string email, OnResponseCallback response = null)
    {
        response(true, "Datos correcto");
        FillUserData();
    }

    public void setUserData(User user, OnResponseCallback response = null)
    {
        response(true, "Datos correcto");
    }

    public void deleteLike(int idExpo, OnResponseCallback response = null)
    {
        response(false, "Datos correcto");
    }

    public void addLike(int idExpo, OnResponseCallback response = null)
    {
        response(false, "Datos correcto");
    }

    public void getConferenceData(OnResponseCallback response = null)
    {     
        FillConferenceData((s,m) =>
        {
            if (s)
            {
                response(true, "Datos conferencia correcto");
            }
        });
    }

    public Texture2D getTextureExpositor(OnResponseCallback response = null)
    {
        response(true, "Dato");

        return new Texture2D(2, 2, TextureFormat.ARGB32, false);
    }

    /// <summary>
    /// Llenar datos del usuario
    /// </summary>
    private void FillUserData()
    {
        // LLenar datos usuario en Appmanager

        // Obtener usuario
        var userJson = Resources.Load<TextAsset>("User/User");

        var n = JSON.Parse(userJson.ToString());

        User u = new User();

        List<int> likes = new List<int>();

        for (int i = 0; i < n["message"]["like_user"].Count; i++)
        {
            likes.Add(n["message"]["like_user"][i]["id"].AsInt);
        }

        u.id = n["message"]["id"].AsInt;
        u.email = n["message"]["email"].Value;
        u.idLikeExpositions = likes;

        AppManager.Instance.currUser = u;
    }

    private void FillConferenceData(OnResponseCallback response = null)
    {
        // Llenar datos conferencia en Conference Control

        // Obtener conferencias
        var expoJson = Resources.Load<TextAsset>("Conference/Expositions");

        var n = JSON.Parse(expoJson.ToString());

        List<Exposition> arrExpo = new List<Exposition>();

        for (int i = 0; i < n["message"].Count; i++)
        {
            Exposition e = new Exposition();

            e.id = n["message"][i]["id"].AsInt;
            e.day = DateTime.Parse(n["message"][i]["day"].Value);
            e.name_exposition = n["message"][i]["name_exposition"].Value;
            e.info_exposition = n["message"][i]["info_exposition"].Value;
            e.hour = n["message"][i]["hour"].Value;
            e.room = n["message"][i]["room"].Value;
            e.name_expositor = n["message"][i]["name_expositor"].Value;
            e.url_photo_expositor = n["message"][i]["photo_expositor"].Value;
            e.info_expositor = n["message"][i]["info_expositor"].Value;

            arrExpo.Add(e);
        }

        ConferenceControl.Instance.FillExpositionsInformation(arrExpo);

        if (response != null)
            response(true, "Datos cargado correctamente");
    }

    public delegate void OnResponseCallback(bool status, string message);
    public static event OnResponseCallback onResponse;
}

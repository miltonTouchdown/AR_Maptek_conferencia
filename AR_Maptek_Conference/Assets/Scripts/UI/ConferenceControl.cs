using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ConferenceControl : MonoBehaviour
{
    public bool isLoadConference = false;

    // Informacion de exposiciones
    public List<Exposition> arrExposition;

    public Exposition currExposition = null;

    private static ConferenceControl _instace;
    public static ConferenceControl Instance
    {
        get
        {
            return _instace;
        }
        set
        {
            if(_instace == null)
            {
                _instace = value;
            }
        }
    }

    void Awake()
    {
        if(_instace == null)
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
        // Obtener Likes de usuario

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

        FillExpositionsInformation(arrExpo);
    }

    /// <summary>
    /// Llenar arreglo de exposiciones
    /// </summary>
    public void FillExpositionsInformation(List<Exposition> expositions)
    {
        // LLenar arreglo obtenido desde webservice
        arrExposition = expositions;

        isLoadConference = true;

        //Test
        FindObjectOfType<UIMainMenu>().initMainMenu();
    }

    /// <summary>
    /// Obtener charlas de un dia en especifico
    /// </summary>
    /// <param name="day">entero que indica dia de la charla</param>
    /// <returns>Retorna un arreglo ordena por fechas de las charlas del dia especifico</returns>
    public Exposition[] GetExpositionsByDay(int day)
    {
        List<Exposition> e = arrExposition.Where((exp) => (int)exp.day.Day == day).ToList();

        return e.OrderByDescending( (d) => d.day).Reverse().ToArray();
    }

    public void setLikeExposition()
    {
        Exposition expo = arrExposition.Single((ex) => ex.id == currExposition.id);

        expo.isLiked = !expo.isLiked;

        // TODO Subir like a webservice
    }

    public Texture GetTextureExpositor()
    {
        // Cargar textura desde webservice

        Texture2D t = new Texture2D(2, 2, TextureFormat.ARGB32, false);

        Exposition expo = arrExposition.Single((ex) => ex.id == currExposition.id);

        expo.photo_expositor = t;

        return t;
    }
}
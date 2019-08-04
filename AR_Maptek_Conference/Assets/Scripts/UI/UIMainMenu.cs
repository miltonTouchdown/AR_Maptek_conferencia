using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    [Header("General")]
    public Color colorMaptek;

    [Header("Main Menu")]
    public GameObject prefCharlaGloblInfo;
    public Transform contentInfo;
    public TextMeshProUGUI title;

    public Button bttnDayOne;
    public Button bttnDayTwo;
    public Button bttnDayThree;

    [Header("Charla")]
    public WindowMovement viewCharla;
    public TextMeshProUGUI titleCharla;
    public TextMeshProUGUI aboutCharla;

    public Button bttnLike;
    public Image imgLike;

    [Header("Expositor")]
    public WindowMovement viewExpositor;

    void Start ()
    {
        //initMainMenu();
    }

    public void initMainMenu()
    {
        // Cambiar color del boton del dia y mostrar charlas

        int today = DateTime.Now.Day;

        Button bttnToday = null;

        if (today <= 20)
        {
            bttnToday = bttnDayOne;
        }
        if(today == 21)
        {
            bttnToday = bttnDayTwo;
        }
        if (today >= 23)
        {
            bttnToday = bttnDayThree;
        }

        bttnToday.GetComponent<Image>().color = colorMaptek;
        bttnToday.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
        bttnToday.onClick.Invoke();
    }

    public void ShowCharlasByDay(int day)
    {
        Exposition[] expos = ConferenceControl.Instance.GetExpositionsByDay(day);

        // Eliminar informacion que se muestra
        for(int i = contentInfo.childCount - 1; i >= 0; i--)
        {
            Destroy(contentInfo.GetChild(i).gameObject);
        }

        // Crear botones con informacion
        foreach(Exposition e in expos)
        {
            Button bttn = Instantiate(prefCharlaGloblInfo, contentInfo).GetComponent<Button>();

            // TODO Agregar listener a botones para cambiar de vista
            bttn.onClick.AddListener(() => 
            {
                ConferenceControl.Instance.currExposition = e;

                ShowCharlaInformation();
            });

            // Cambiar texto informacion
            bttn.GetComponentInChildren<TextMeshProUGUI>().text = getFormatStringInfo(e);
        }
    }

    public void ShowCharlaInformation()
    {
        viewCharla.setActiveWindow(true);

        imgLike.color = (ConferenceControl.Instance.currExposition.isLiked) ? colorMaptek : Color.white;

        titleCharla.text = getFormatStringInfo(ConferenceControl.Instance.currExposition);
        aboutCharla.text = ConferenceControl.Instance.currExposition.info_exposition;
    }

    public void setLikeCharla()
    {
        ConferenceControl.Instance.setLikeExposition();

        imgLike.color = (ConferenceControl.Instance.currExposition.isLiked) ? colorMaptek : Color.white;

        // TODO: Si es true entonces mostrar mensaje para enviar mail
    }

    public void ShowExpositorInformation()
    {
        viewExpositor.setActiveWindow(true);
    }

    public void BackToMainMenu()
    {
        ConferenceControl.Instance.currExposition = null;
    }

    private string getFormatStringInfo(Exposition expo)
    {
        string textFormat = "<size=80%>@NameExposition\n\n<size=60%>@Hour\n<size=60%><color=#3fcf4e>Room @Room</color>\n<size=100%><b>@NameExpositor";

        textFormat = textFormat.Replace("@NameExposition", expo.name_exposition);
        textFormat = textFormat.Replace("@Hour", expo.hour);
        textFormat = textFormat.Replace("@Room", expo.room);
        textFormat = textFormat.Replace("@NameExpositor", expo.name_expositor);

        return textFormat;
    }
}

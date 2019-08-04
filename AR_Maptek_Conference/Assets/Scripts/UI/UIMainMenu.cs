using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    [Header("Main Menu")]
    public GameObject prefCharlaGloblInfo;
    public Transform contentInfo;
    public TextMeshProUGUI title;

    public Button bttnDayOne;
    public Button bttnDayTwo;
    public Button bttnDayThree;

    void Start ()
    {
        initMenu();
    }

    public void initMenu()
    {
        // Seleccionar dia 
        Debug.Log(DateTime.Now.Day);

        //bttnDayOne.onClick.AddListener(()=> { bttnDayOne.GetComponentInChildren<TextMeshProUGUI>().color = Color.white; });
      
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

            // Agregar listener a botones

            // Cambiar texto informacion
            bttn.GetComponentInChildren<TextMeshProUGUI>().text = getFormatStringInfo(e);
        }
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

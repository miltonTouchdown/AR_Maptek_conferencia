using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionRegister : MonoBehaviour
{
    public bool deletePlayerPref = false;

    void Start()
    {
#if UNITY_EDITOR
        if (deletePlayerPref)
            PlayerPrefs.DeleteAll();
#endif

        // Cargar datos del usuario si es que ya esta registrado
        if (PlayerPrefs.HasKey("Email"))
        {
           LoadUserData(PlayerPrefs.GetString("Email"));
        }
    }

    public void RegisterUser(string email, Webservice.OnResponseCallback onFinish = null)
    {
        Webservice.Instance.registerUser(email, (r,m)=>
        {
            if (r)
            {
                // Guardar datos en playerpref

                // Guardar datos del usuario en appmanager
            }

            if (onFinish != null)
                onFinish.Invoke(r, m);
        });
    }

    public void LoadUserData(string email)
    {
        Webservice.Instance.registerUser(email, (r, m) =>
        {
            if (r)
            {
                // Guardar datos del usuario en appmanager
            }
        });
    }
}

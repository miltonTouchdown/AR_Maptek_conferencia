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
        response.Invoke(true, "registro correcto");
    }

    public void getUserData(OnResponseCallback response = null)
    {
        response.Invoke(true, "Datos correcto");
    }

    public void getConferenceData(OnResponseCallback response = null)
    {
        response.Invoke(true, "Datos conferencia correcto");
    }

    public delegate void OnResponseCallback(bool status, string message);
    public static event OnResponseCallback onResponse;
}

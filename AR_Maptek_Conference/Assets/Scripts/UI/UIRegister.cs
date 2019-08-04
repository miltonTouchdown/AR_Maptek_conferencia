using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIRegister : MonoBehaviour
{
    public TextMeshProUGUI feedback;
    public InputField inputField;

    private SessionRegister _sessionRegister;

    void Start()
    {
        _sessionRegister = FindObjectOfType<SessionRegister>();
    }

    public void OnRegister()
    {
        string email = inputField.text;

        // Revisar formato de email
        bool isMailValid = true;//new EmailAddressAttribute().IsValid(email);

        if (!isMailValid)
        {
            feedback.text = "Correo con formato incorrecto";

            return;
        }

        // Activar pantalla de carga "registrando"

        // registrar
        _sessionRegister.RegisterUser(email, (s,m)=> 
        {
            // Desactivar pantalla de carga "registrando"

            if (!s)
            {
                // Mostrar feedback al usuario
                feedback.text = m;
            }
        });
    }
}

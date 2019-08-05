using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class User
{
    public int id;
    public string email;
    public List<int> idLikeExpositions; // Los "me gusta" guardados del usuario
}

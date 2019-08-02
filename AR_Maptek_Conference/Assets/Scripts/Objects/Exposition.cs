using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Exposition
{
    public int date; // dia de la charla
    public string name_exposition;
    public string hour; // Horario de la charla. Formato ejemplo "08:00 am - 10:00 am"
    public int room;
    public string name_expositor;
    public Texture photo_expositor;
    public string info_expositor;
}

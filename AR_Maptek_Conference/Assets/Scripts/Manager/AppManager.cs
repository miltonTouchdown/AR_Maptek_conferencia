using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppManager : MonoBehaviour
{
    public GameObject loadingScreen;

    public User currUser = null;

    private static AppManager _instace;
    public static AppManager Instance
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
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        
    }

    public void DownloadDataConference()
    {

    }

    public void DownloadDataUser()
    {

    }

    public delegate void OnFinishCallback();
    public static event OnFinishCallback onFinish;
}
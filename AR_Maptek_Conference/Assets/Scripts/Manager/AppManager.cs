using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AppManager : MonoBehaviour
{
    [Header("Global UI")]
    public GameObject loadingScreen;

    [Header("User")]
    public User currUser = null;

    [Header("Email data")]
    public string emailTo;
    public string subject;
    public string body;

    private UIMainMenu _UIMainMenu = null;
    private SessionRegister _sessionRegister = null;
    private PopUp _popUp = null;

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
        _sessionRegister = FindObjectOfType<SessionRegister>();
        _popUp = GetComponentInChildren<PopUp>();

        if (!_sessionRegister.LoadUserData())
        {
            loadingScreen.SetActive(false);
        }
    }

    public void DownloadDataConference()
    {

    }

    public void DownloadDataUser()
    {

    }

    public void LoadMainMenu()
    {
        loadingScreen.SetActive(true);

        // Cargar escena main menu
        LoadScene(1, () =>
        {
            _UIMainMenu = FindObjectOfType<UIMainMenu>();

            if (ConferenceControl.Instance.isLoadConference)
            {
                // Cargar charlas
                _UIMainMenu.initMainMenu();

                // Cargar charla en caso de estar iniciada
                if (ConferenceControl.Instance.currExposition.isOpen)
                    _UIMainMenu.ShowCharlaInformation();

                LeanTween.delayedCall(.3f, ()=>
                {
                    loadingScreen.SetActive(false);
                }); 
            }
            else
            {
                // Descargar conferencias si es que no estan descargadas

                Webservice.Instance.getConferenceData((s, m) =>
                {
                    if (s)
                    {
                        ConferenceControl.Instance.SetLikesExposition(currUser.idLikeExpositions);

                        _UIMainMenu.initMainMenu();

                        LeanTween.delayedCall(.3f, () =>
                        {
                            loadingScreen.SetActive(false);
                        });
                    }                  
                });
            }
        });
    }

    public void LoadRegisterMenu()
    {
        LoadScene(0, ()=>
        {
            _sessionRegister = FindObjectOfType<SessionRegister>();

            if (!_sessionRegister.LoadUserData())
            {
                loadingScreen.SetActive(false);
            }
        });
    }

    public void LoadScene(int index, OnFinishCallback onFinish = null)
    {
        //loadingScreen.SetActive(true);

        StartCoroutine(LoadAsyncScene(index, onFinish));
    }

    IEnumerator LoadAsyncScene(int index, OnFinishCallback onFinish = null)
    {

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(index);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        //loadingScreen.SetActive(false);

        if (onFinish != null)
            onFinish();
    }

    public void SendEmail()
    {
        body = body.Replace("@NameExposition","'"+ConferenceControl.Instance.currExposition.name_exposition+ "'");

        SenderEmail.SendEmail(emailTo, subject, body);
    }

    public delegate void OnFinishCallback();
    public static event OnFinishCallback onFinish;
}
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Namer : MonoBehaviour
{
    public static Namer Instance { get; private set; }
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    public TMP_InputField userNAME;
    public string userName = "";

    void Update()
    {
        if(userNAME!= null)
            userName = userNAME.text;
    }
    public void StartGame()
    {
        SceneManager.LoadSceneAsync(1);
    }
}

using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public GameObject diePanel;
    [SerializeField] private TextMeshProUGUI sender;

    private void Awake()
    {
        Instance = this;
        diePanel.SetActive(false);
    }

    public void DiePanel()
    {
        diePanel.SetActive(true);
        StartCoroutine(Wait());
    }
    public void UpdateVisual(string userName, string side)
    {
        sender.text = userName + " Helping " + side;
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(10f);
        SceneManager.LoadSceneAsync(1);
    }

}

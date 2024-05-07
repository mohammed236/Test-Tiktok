using TMPro;
using UnityEngine;

public class Health : MonoBehaviour
{
    private float currenthealth;
    [SerializeField] private float health = 1160f;
    [SerializeField] private Transform fill = null;
    [SerializeField] private TextMeshProUGUI value_txt;


    private void Start()
    {
        currenthealth = health;
        float currentHp = currenthealth / health;
        fill.localScale = new Vector3(1, currentHp, 1);
        value_txt.text = currenthealth.ToString();
    }
    public float Damage(float damage)
    {
        if(currenthealth > 0)
        {
            currenthealth -= damage;
        }
        else
        {
            UIManager.Instance.DiePanel();
        }
        float currentHp = currenthealth / health;
        fill.localScale = new Vector3(1,currentHp,1);
        value_txt.text = currenthealth.ToString();

        return currenthealth;
    }
}

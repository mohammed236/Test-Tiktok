using Arab.Baking;
using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public static Gun Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] private Transform fireStartPoint_P;
    [SerializeField] private Transform fireStartPoint_I;
    [SerializeField] private Transform Palestine;
    [SerializeField] private Transform Isreal;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float bulletSpeed = 2f;
    public string str = "";
    public string user = "";
    public string gft = "";
    private bool isShooting = false;
    private Transform currentOrigine;
    public int damageMultiplier = 1;

    private void Shoot(Transform origin,float speed,Transform start)
    {
        SoundManager.instance.PlaySound(SoundManager.Sound.Shoot);
        currentOrigine = origin;
        StartCoroutine(Wait());
        GameObject b = Instantiate(bullet,start.position,Quaternion.identity, origin);
        b.GetComponent<Rigidbody2D>().AddForce(origin.right * speed);
        Destroy(b,5f);

    }
    private void Update()
    {
        if(currentOrigine!= null)
        currentOrigine.GetComponentInChildren<Animator>().SetBool("IsShooting", isShooting);
        switch (gft)
        {
            case "Rose":// 1 Palestine
                damageMultiplier = 5;
                Shoot(Palestine, bulletSpeed, fireStartPoint_P);
                UIManager.Instance.UpdateVisual(user, "Palestine");

                gft = "";
                user = "";

                break;
            case "Cap"://99 Palestine
                damageMultiplier = 25;
                Shoot(Palestine, bulletSpeed, fireStartPoint_P);
                UIManager.Instance.UpdateVisual(user, "Palestine");

                gft = "";
                user = "";

                break;
            case "Money Gun"://500 Palestine
                damageMultiplier = 100;
                Shoot(Palestine, bulletSpeed, fireStartPoint_P);
                UIManager.Instance.UpdateVisual(user, "Palestine");

                gft = "";
                user = "";

                break;
            case "Diamond"://1000 Palestine
                damageMultiplier = 500;
                Shoot(Palestine, bulletSpeed, fireStartPoint_P);
                UIManager.Instance.UpdateVisual(user, "Palestine");

                gft = "";
                user = "";

                break;
            case "GG":// 1 Isreil
                damageMultiplier = 5;
                Shoot(Isreal, bulletSpeed, fireStartPoint_I);
                UIManager.Instance.UpdateVisual(user, "Israel");

                gft = "";
                user = "";

                break;
            case "Hat and Mustache":// 99 Isreil
                damageMultiplier = 25;
                Shoot(Isreal, bulletSpeed, fireStartPoint_I);
                UIManager.Instance.UpdateVisual(user, "Israel");

                gft = "";
                user = "";

                break;
            case "Gem Gun":// 500 Isreil
                damageMultiplier = 100;
                Shoot(Isreal, bulletSpeed, fireStartPoint_I);
                UIManager.Instance.UpdateVisual(user, "Israel");

                gft = "";
                user = "";

                break;
            case "Champion":// 1000 Isreil
                damageMultiplier = 500;
                Shoot(Isreal, bulletSpeed, fireStartPoint_I);
                UIManager.Instance.UpdateVisual(user, "Israel");

                gft = "";
                user = "";

                break;
            default:
                //UIManager.Instance.UpdateVisual("", "");

                break;
        }
        switch (str)
        {
            case "1":
                damageMultiplier = 1;
                Shoot(Palestine, bulletSpeed, fireStartPoint_P);
                UIManager.Instance.UpdateVisual(user,"Palestine");
                str = "";
                user = "";

                break;
            case "2":
                damageMultiplier = 1;
                Shoot(Isreal, bulletSpeed, fireStartPoint_I);
                UIManager.Instance.UpdateVisual(user, "Israel");

                str = "";
                user = "";

                break;
            default:
               //UIManager.Instance.UpdateVisual("", "");

                break;
        }

    }
    
    /*IEnumerator ShootCourotine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.2f);
            switch (str)
            {
                case "1":
                    damageMultiplier = 1;
                    Shoot(Palestine, bulletSpeed, fireStartPoint_P);
                    break;
                case "2":
                    damageMultiplier = 1;
                    Shoot(Isreal, bulletSpeed, fireStartPoint_I);
                    break;
                case "Rose":// 1 Palestine
                    damageMultiplier = 5;
                    Shoot(Palestine, bulletSpeed, fireStartPoint_P);
                    break;
                case "Cap"://99 Palestine
                    damageMultiplier = 10;
                    Shoot(Palestine, bulletSpeed, fireStartPoint_P);
                    break;
                case "You're Amazing"://500 Palestine
                    damageMultiplier = 25;
                    Shoot(Palestine, bulletSpeed, fireStartPoint_P);
                    break;
                case "Email Message"://1000 Palestine
                    damageMultiplier = 50;
                    Shoot(Palestine, bulletSpeed, fireStartPoint_P);
                    break;
                case "GG":// 1 Isreil
                    damageMultiplier = 5;
                    Shoot(Isreal, bulletSpeed, fireStartPoint_I);
                    break;
                case "Hat and Mustache":// 99 Isreil
                    damageMultiplier = 10;
                    Shoot(Isreal, bulletSpeed, fireStartPoint_I);
                    break;
                case "Money Gun":// 500 Isreil
                    damageMultiplier = 25;
                    Shoot(Isreal, bulletSpeed, fireStartPoint_I);
                    break;
                case "Mirror Bloom":// 1000 Isreil
                    damageMultiplier = 50;
                    Shoot(Isreal, bulletSpeed, fireStartPoint_I);
                    break;
                default:
                    break;
            }
            str = "";
        }
    }*/

    IEnumerator Wait()
    {
        isShooting = true;
        yield return new WaitForSeconds(.3f);
        isShooting = false;
    }
}

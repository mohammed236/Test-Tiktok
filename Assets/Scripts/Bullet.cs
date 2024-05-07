using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hit " + collision.transform.name);
            collision.gameObject.GetComponent<Health>().Damage(Gun.Instance.damageMultiplier);
        }
        Destroy(gameObject);
    }
}

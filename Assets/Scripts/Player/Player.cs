using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] int hp = 50;
    public Transform cameraPosition;

    public void TakeDamage(int damage)
    {
        hp -= Mathf.Max(damage, 1);

        if (hp < 0)
        {
            Destroy(gameObject);
        }
    }
}
using System.Collections;
using UnityEngine;

public class AttackZone : MonoBehaviour
{
    int damage = 25;
    [SerializeField] float attackCD = 2f;
    bool isReadyToAttack = true;
    public BoxCollider attackZone;
    public Animator hand;
    public AudioSource attackSound;
    public AudioClip[] clips;

    private void Start()
    {
        attackZone = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !attackZone.enabled && isReadyToAttack)
        {
            int i = Random.Range(0, clips.Length);
            attackSound.clip = clips[i];
            attackSound.Play();
            attackZone.enabled = true;
            hand.SetBool("isAttack", true);
            int rand = Random.Range(0, 2);
            hand.SetInteger("AttackInt", rand);
            StartCoroutine(HideAttackZone());
        }
    }

    public void ReadyAttack()
    {
        isReadyToAttack = true;
        hand.SetBool("isAttack", false);
    }

    IEnumerator HideAttackZone()
    {
        yield return new WaitForSeconds(0.1f);
        attackZone.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            Partygoer partygoer = other.GetComponent<Partygoer>();
            if (partygoer != null)
            {
                partygoer.TakeDamage(damage);
                
                Debug.Log($"Partygoer took damage {damage}, hp = {partygoer.HP}");
            }

        }
    }
}

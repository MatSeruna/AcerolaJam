using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hands : MonoBehaviour
{
    public AttackZone AttackZone;
    public void Ready()
    {
        AttackZone.ReadyAttack();
    }
}

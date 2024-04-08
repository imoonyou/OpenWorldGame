using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCaculate : MonoBehaviour
{
    public BoxCollider attackCollider;


    private void Awake()
    {
        attackCollider = GetComponentInParent<BoxCollider>();
    }
}

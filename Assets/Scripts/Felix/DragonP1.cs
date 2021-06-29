using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonP1 : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;

    public Transform shootTransform;
    public GameObject fireballPrefab;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        
    }
}

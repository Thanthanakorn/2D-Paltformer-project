using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCOnfiner : MonoBehaviour
{
    private BoxCollider2D _box;
    void Start()
    {
        _box = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}

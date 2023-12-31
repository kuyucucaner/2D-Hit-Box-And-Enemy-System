using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    Animator animator;
    public float Health
    {
        set
        {
            health = value;
            if (health <= 0)
            {
                Defeated();
            }
        }
        get
        {
            return health;
        }
    }
    public float health = 1;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Defeated()
    {
        Debug.Log("Animation is successful!");
        Destroy(gameObject);
    }

}

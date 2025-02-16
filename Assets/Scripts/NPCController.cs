using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NPCController : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator animator;
    public UnityEvent onInteract;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void LookAtPlayer(Vector3 playerPosition)
    {
        Vector3 direction = playerPosition - transform.position;
        direction.Normalize();

        animator.SetFloat("MoveX", direction.x);
        animator.SetFloat("MoveY", direction.y);
    }

    public void greeting()
    {

    }
}

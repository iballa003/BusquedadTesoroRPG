using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed = 20f;
    private bool isMoving;
    private Vector2 input;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isMoving){
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            if(input != Vector2.zero){
                var targetPos = transform.position;
                targetPos.x = input.x;
                targetPos.y = input.y;
            }
        }
    }

    IEnumerator Move(Vector3 targetPos){
        while((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon){
            transform.position = Vector3.MoveTowards(transform.position,targetPos,moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;
    }
}

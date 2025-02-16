using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed;
    private bool isMoving;
    private Vector2 input;

    private Animator animator;

    public LayerMask InteractiveLayer;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isMoving){
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            

            if (input != Vector2.zero){
                input = input.normalized;

                animator.SetFloat("MoveX", input.x);
                animator.SetFloat("MoveY", input.y);

                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;

                if(isWalkable(targetPos)){
                    StartCoroutine(Move(targetPos));
                }
            }
        }
        animator.SetBool("IsMoving", isMoving);
        if (Input.GetKeyDown(KeyCode.Space)) // Usa la tecla Espacio para probar
        {
            StartCoroutine(MoveTileUp());
        }
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Z))
        {
            Interact();
        }
    }

    private IEnumerator MoveTileUp()
    {
        animator.SetFloat("MoveX", 0);   // No hay movimiento horizontal
        animator.SetFloat("MoveY", 1);   // Movimiento hacia arriba (Y positivo)
        animator.SetBool("IsMoving", true); // Activar la animación de movimiento
        Vector3 targetPos1 = transform.position + Vector3.up; // Primera casilla

        // Mover a la primera casilla
        if (isWalkable(targetPos1))
        {
            yield return StartCoroutine(Move(targetPos1));
        }
        else
        {
            yield break; // Detener si la primera casilla no es transitable
        }
        animator.SetBool("IsMoving", false); // Detener la animación al finalizar
    }

    IEnumerator Move(Vector3 targetPos){
        isMoving = true;
        while((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon){
            transform.position = Vector3.MoveTowards(transform.position,targetPos,moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;
        isMoving = false;
    }

    private bool isWalkable(Vector3 targetPos){
        if(Physics2D.OverlapCircle(targetPos, 0.2f, InteractiveLayer) != null){
            return false;
        }
        return true;
    }

    private void Interact()
    {
        var facingDirection = new Vector3(animator.GetFloat("MoveX"), animator.GetFloat("MoveY"));
        var InteractPos = transform.position + facingDirection;
        var collider = Physics2D.OverlapCircle(InteractPos, 0.2f, InteractiveLayer);

        if (collider != null) {
            var npc = collider.GetComponent<NPCController>();
            if (npc != null)
            {
                npc.LookAtPlayer(transform.position);
                Debug.Log("Hola. Soy un NPC");
            }
        }
    }
}

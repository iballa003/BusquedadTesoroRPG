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
       
    }

    public void action()
    {
        if (onInteract != null)
        {
            onInteract.Invoke(); // Llama a los eventos asignados en el Inspector
        }
        else
        {
            Debug.Log("No hay acción asignada para este NPC.");
        }
    }
}

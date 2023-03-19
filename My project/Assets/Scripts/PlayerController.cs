using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    new Rigidbody rigidbody;
    Animator animator;
    string JumpParameter = "Jump";

    float JumpSeeed = 7f;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rigidbody = GetComponentInChildren<Rigidbody>();
        GameController.instance.gameInitialized += Initialize;
    }

    private void Initialize()
    {
        transform.localPosition = Vector3.zero;
    }

    public void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Jump();
        }
    }

    public void Jump()
    {
        if (GroundCheck()) { 
            animator.SetTrigger(JumpParameter);
            rigidbody.velocity = Vector3.up* JumpSeeed;
        }
    }
    public bool GroundCheck()
    {
        return Physics.Raycast(transform.position, -Vector3.up, 0.2f);
    }
}

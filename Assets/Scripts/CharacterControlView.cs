using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterControlView : MonoBehaviour
{
    InputMaster controls;
    void Awake()
    {
        print("test");
        controls = new InputMaster();
        //controls.Map1.Move.started += ctx => OnMovement(ctx.ReadValue<Vector2>());
        //controls.Map1.Move.performed += ctx => OnMovement(ctx.ReadValue<Vector2>());
    }
    public void OnEnable()
    {
        controls.Enable();
    }

    public void OnDisable()
    {
        controls.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        CharacterMove();
        CharacterAnimation();
    }

    public Animator playerMovement;
    public float speed;
    public GameObject player;
    Vector3 input;
    void CheckInput()
    {
        OnMovement(controls.Map1.Move.ReadValue<Vector2>());
    }
    public void OnMovement(Vector2 _input)
    {
        print(_input.x);
        input = _input;
        //input.x = Input.GetAxisRaw("Horizontal");
        //input.y = Input.GetAxisRaw("Vertical");
    }

    void CharacterMove()
    {
        player.GetComponent<Rigidbody2D>().MovePosition(player.transform.position + input * Time.deltaTime * speed);
    }

    void CharacterAnimation()
    {
        playerMovement.SetFloat("MoveX", input.x);
        playerMovement.SetFloat("MoveY", input.y);
        playerMovement.SetBool("Moving", (new Vector2(input.x, input.y).magnitude > 0f));
    }
}

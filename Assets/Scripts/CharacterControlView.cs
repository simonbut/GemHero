using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterControlView : MonoBehaviour
{
    public float lengthLimit = 10;

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
        input = _input;
    }

    void CharacterMove()
    {
        Vector3 _prePos = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        Vector3 _offset = input * Time.deltaTime * speed;
        player.GetComponent<Rigidbody2D>().MovePosition(player.transform.position + _offset);
        if (Rope.instance.GetRopeLength(_offset) > lengthLimit)
        {
            player.GetComponent<Rigidbody2D>().MovePosition(player.transform.position);
        }
    }

    void CharacterAnimation()
    {
        playerMovement.SetFloat("MoveX", input.x);
        playerMovement.SetFloat("MoveY", input.y);
        playerMovement.SetBool("Moving", (new Vector2(input.x, input.y).magnitude > 0f));
    }
}

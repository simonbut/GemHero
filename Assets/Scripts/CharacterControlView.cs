using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControlView : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
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

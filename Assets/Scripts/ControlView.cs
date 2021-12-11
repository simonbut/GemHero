using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using ClassHelper;

public class ControlView : MonoBehaviour
{
    #region instance
    private static ControlView m_instance;

    public static ControlView Instance
    {
        get
        {
            return m_instance;
        }
    }

    void Awake()
    {
        if (ControlView.Instance == null)
        {
            m_instance = this;
            controls = new InputMaster();
        }
    }
    #endregion


    public float lengthLimit = 10;
    public GameObject cameraParent;

    public InputMaster controls;

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

    void UpdateCameraPosition(Vector3 _offset)
    {
        if (!UIManager.Instance.IsNoUI())
        {
            return;
        }
        Vector3 _newVector = player.transform.position + _offset - cameraParent.transform.position;
        Vector3 result = cameraParent.transform.position;
        if (Mathf.Abs(_newVector.y) > 2)
        {
            result.y += _offset.y;
        }
        if (Mathf.Abs(_newVector.x) > 1)
        {
            result.x += _offset.x;
        }
        cameraParent.transform.position = result;
    }

    public Animator playerMovement;
    public float speed;
    public GameObject player;
    Vector3 input;
    void CheckInput()
    {
        OnMovement(controls.Map1.Move.ReadValue<Vector2>());
        OnReact(controls.Map1.React.triggered);
        OnOpenMenu(controls.Map1.AssetKey.triggered);
    }

    public void OnOpenMenu(bool isTrue)
    {
        if (!UIManager.Instance.IsNoUI())
        {
            return;
        }
        if (isTrue)
        {
            MainGameView.Instance.inGameMainMenuUI.AddUI();
        }
    }

    public void OnMovement(Vector2 _input)
    {
        input = _input;
    }

    public void OnReact(bool isTrue)
    {
        if (isTrue)
        {
            MainGameView.Instance.CharacterReact();
        }
    }

    void CharacterMove()
    {
        if (!UIManager.Instance.IsNoUI())
        {
            return;
        }
        Vector3 _prePos = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        Vector3 _offset = input * Time.deltaTime * speed;
        player.GetComponent<Rigidbody2D>().MovePosition(player.transform.position + _offset);
        if (Rope.instance.GetRopeLength(_offset) > lengthLimit)
        {
            player.GetComponent<Rigidbody2D>().MovePosition(player.transform.position);
        }

        Vector3 tempScale = player.transform.localScale;
        if (_offset.x > 0)
        {
            tempScale.x = -Mathf.Abs(tempScale.x);
        }
        if (_offset.x < 0)
        {
            tempScale.x = Mathf.Abs(tempScale.x);
        }
        player.transform.localScale = tempScale;

        UpdateCameraPosition(_offset);
    }

    void CharacterAnimation()
    {
        if (!UIManager.Instance.IsNoUI())
        {
            return;
        }
        playerMovement.SetFloat("MoveX", input.x);
        playerMovement.SetFloat("MoveY", input.y);
        playerMovement.SetBool("Moving", (new Vector2(input.x, input.y).magnitude > 0f));
    }
}

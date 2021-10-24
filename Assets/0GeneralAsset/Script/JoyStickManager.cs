using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.DualShock;

public class JoyStickManager : MonoBehaviour
{
    #region instance
    private static JoyStickManager m_instance;

    public static JoyStickManager Instance
    {
        get
        {
            return m_instance;
        }
    }

    void Awake()
    {
        m_instance = this;
    }
    #endregion


    public float rsx;
    public float rsy;
    public float PS4rsx;
    public float PS4rsy;

    //public bool isJoyStickUsing = false;
    //private bool isJoyStickSetuped = false;
    //JoyStickControls joyStickControls;

    // Start is called before the first frame update
    void Start()
    {
        //joyStickControls = new JoyStickControls();

    }

    public Vector2 GetJoyStickRotate()
    {
        if (IsJoyStickEnable())
        {
            if (Gamepad.current is DualShockGamepad)
            {
                return new Vector2(PS4rsx, PS4rsy);
            }
            else
            {
                return new Vector2(rsx, rsy);
            }
        }
        return new Vector2(0,0);
    }

    // Update is called once per frame
    void Update()
    {
        //if (JoyStickManager.Instance.IsInputDown("Cross"))
        //{
        //    print("shoot");
        //}

        //if (IsJoyStickEnable())
        //{
        //    print("have joystick");
        //}
        //else
        //{
        //    print("no joystick");
        //}

        //if (!isJoyStickSetuped)
        //{
        //    if (IsJoyStickEnable())
        //    {
        //        print("setup joystick");
        //        //JoyStickSetup();
        //        isJoyStickSetuped = true;
        //    }
        //}

        //if (isJoyStickUsing)
        //{
        //    print("usingjoyStick");
        //    //JoyStickMove();
        //}

        if (CheckAnyGamepadButtonPressed())
        {
            print("isJoyStickEnable");
            isJoyStickEnable = true;
        }
        if (CheckAnyKeyboardAndMousePressed())
        {
            isJoyStickEnable = false;
        }


        if (IsJoyStickEnable())
        {
            float x = Input.GetAxis("DPad X");
            float y = Input.GetAxis("DPad Y");
            float PS4x = Input.GetAxis("PS4 DPad X");
            float PS4y = Input.GetAxis("PS4 DPad Y");
            float x2 = Input.GetAxis("Xaxis");
            float y2 = Input.GetAxis("Yaxis");
            float l2 = Input.GetAxis("L2");
            float rw = Input.GetAxis("R2");
            rsx = Input.GetAxis("RightStickX");
            rsy = Input.GetAxis("RightStickY");
            PS4rsx = Input.GetAxis("PS4 RightStickX");
            PS4rsy = Input.GetAxis("PS4 RightStickY");
            isUpPrevious = isUp;
            isDownPrevious = isDown;
            isLeftPrevious = isLeft;
            isRightPrevious = isRight;
            isRightStickUpPrevious = isRightStickUp;
            isRightStickDownPrevious = isRightStickDown;
            isPS4UpPrevious = isPS4Up;
            isPS4DownPrevious = isPS4Down;
            isPS4LeftPrevious = isPS4Left;
            isPS4RightPrevious = isPS4Right;
            isPS4RightStickUpPrevious = isPS4RightStickUp;
            isPS4RightStickDownPrevious = isPS4RightStickDown;
            //isRightStickRightPrevious = isRightStickRight;
            //isRightStickLeftPrevious = isRightStickLeft;
            isRightStickUpPrevious = isRightStickUp;
            isRightStickDownPrevious = isRightStickDown;
            isL2Previous = isL2;
            isR2Previous = isR2;
            isPS4Left = (x2 < -0.25f || PS4x < -0.25f);
            isPS4Right = (x2 > 0.25f || PS4x > 0.25f);
            isPS4Up = (y2 < -0.25f || PS4y > 0.25f);
            isPS4Down = (y2 > 0.25f || PS4y < -0.25f);
            isLeft = (x < -0.25f || x2 < -0.25f);
            isRight = (x > 0.25f || x2 > 0.25f);
            isUp = (y < -0.25f || y2 < -0.25f);
            isDown = (y > 0.25f || y2 > 0.25f);
            isL2 = (l2 > 0.25f);
            isR2 = (rw > 0.25f);
            //isRightStickRight = (rsx > 0.25f);
            //isRightStickLeft = (rsx < -0.25f);
            isRightStickUp = (rsy < -0.25f);
            isRightStickDown = (rsy > 0.25f);
            isPS4RightStickUp = (PS4rsy < -0.25f);
            isPS4RightStickDown = (PS4rsy > 0.25f);
            //print("isRight " + isRight);
            //print("isRightPrevious " + isRightPrevious);
        }

        if (IsJoyStickEnable())
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    //bool isRightStickRight = false;
    //bool isRightStickLeft = false;
    bool isRightStickUp = false;
    bool isRightStickDown = false;
    bool isUp = false;
    bool isDown = false;
    bool isLeft = false;
    bool isRight = false;
    //bool isPS4RightStickRight = false;
    //bool isPS4RightStickLeft = false;
    bool isPS4RightStickUp = false;
    bool isPS4RightStickDown = false;
    bool isPS4Up = false;
    bool isPS4Down = false;
    bool isPS4Left = false;
    bool isPS4Right = false;
    //bool isRightStickRightPrevious = false;
    //bool isRightStickLeftPrevious = false;
    bool isRightStickUpPrevious = false;
    bool isRightStickDownPrevious = false;
    bool isUpPrevious = false;
    bool isDownPrevious = false;
    bool isLeftPrevious = false;
    bool isRightPrevious = false;
    //bool isPS4RightStickRightPrevious = false;
    //bool isPS4RightStickLeftPrevious = false;
    bool isPS4RightStickUpPrevious = false;
    bool isPS4RightStickDownPrevious = false;
    bool isPS4UpPrevious = false;
    bool isPS4DownPrevious = false;
    bool isPS4LeftPrevious = false;
    bool isPS4RightPrevious = false;
    bool isL2 = false;
    bool isR2 = false;
    bool isL2Previous = false;
    bool isR2Previous = false;

    bool isJoyStickEnable = false;

    public bool IsJoyStickEnable()
    {
        return isJoyStickEnable;
    }

    //public bool IsJoyStickEnable()
    //{
    //    if (Gamepad.current != null)
    //    {
    //        return true;
    //    }
    //    return false;
    //}

    public bool IsInput(string button)
    {
        if (Gamepad.current != null && (Gamepad.current is DualShockGamepad))
        {
            button = PS4ButtonToXboxButton(button);
        }
        switch (button)
        {
            case "PS4RightStickUp":
                if (IsJoyStickEnable())
                {
                    return (isPS4RightStickUp);
                }
                break;
            case "PS4RightStickDown":
                if (IsJoyStickEnable())
                {
                    return (isPS4RightStickDown);
                }
                break;
            case "PS4Up":
                if (IsJoyStickEnable())
                {
                    return (isPS4Up);
                }
                break;
            case "PS4Down":
                if (IsJoyStickEnable())
                {
                    return (isPS4Down);
                }
                break;
            case "PS4Left":
                if (IsJoyStickEnable())
                {
                    return (isPS4Left);
                }
                break;
            case "PS4Right":
                if (IsJoyStickEnable())
                {
                    return (isPS4Right);
                }
                break;
            case "RightStickUp":
                if (IsJoyStickEnable())
                {
                    return (isRightStickUp);
                }
                break;
            case "RightStickDown":
                if (IsJoyStickEnable())
                {
                    return (isRightStickDown);
                }
                break;
            case "Up":
                if (IsJoyStickEnable())
                {
                    return (isUp);
                }
                break;
            case "Down":
                if (IsJoyStickEnable())
                {
                    return (isDown);
                }
                break;
            case "Left":
                if (IsJoyStickEnable())
                {
                    return (isLeft);
                }
                break;
            case "Right":
                if (IsJoyStickEnable())
                {
                    return (isRight);
                }
                break;
            case "L2":
                if (IsJoyStickEnable())
                {
                    return (isL2);
                }
                break;
            case "R2":
                if (IsJoyStickEnable())
                {
                    return (isR2);
                }
                break;
            case "Circle":
                if (IsJoyStickEnable())
                {
                    if (Input.GetKey(KeyCode.Mouse0))
                    {
                        return false;
                    }
                }
                break;
            case "Cross":
                if (IsJoyStickEnable())
                {
                    if (Input.GetKey(KeyCode.Mouse1))
                    {
                        return false;
                    }
                }
                break;
        }
        return Input.GetButton(button);
    }

    public bool IsInputDown(string button)
    {
        if (Gamepad.current != null && (Gamepad.current is DualShockGamepad))
        {
            button = PS4ButtonToXboxButton(button);
        }
        switch (button)
        {
            case "PS4RightStickUp":
                if (IsJoyStickEnable())
                {
                    return (isPS4RightStickUp && !isPS4RightStickUpPrevious);
                }
                break;
            case "PS4RightStickDown":
                if (IsJoyStickEnable())
                {
                    return (isPS4RightStickDown && !isPS4RightStickDownPrevious);
                }
                break;
            case "PS4Up":
                if (IsJoyStickEnable())
                {
                    return (isPS4Up && !isPS4UpPrevious);
                }
                break;
            case "PS4Down":
                if (IsJoyStickEnable())
                {
                    return (isPS4Down && !isPS4DownPrevious);
                }
                break;
            case "PS4Left":
                if (IsJoyStickEnable())
                {
                    return (isPS4Left && !isPS4LeftPrevious);
                }
                break;
            case "PS4Right":
                if (IsJoyStickEnable())
                {
                    return (isPS4Right && !isPS4RightPrevious);
                }
                break;
            case "RightStickUp":
                if (IsJoyStickEnable())
                {
                    return (isRightStickUp && !isRightStickUpPrevious);
                }
                break;
            case "RightStickDown":
                if (IsJoyStickEnable())
                {
                    return (isRightStickDown && !isRightStickDownPrevious);
                }
                break;
            case "Up":
                if (IsJoyStickEnable())
                {
                    //if (isUp && !isUpPrevious)
                    //{
                    //    print("Up");
                    //}
                    return (isUp && !isUpPrevious);
                }
                break;
            case "Down":
                if (IsJoyStickEnable())
                {
                    //if (isDown && !isDownPrevious)
                    //{
                    //    print("Down");
                    //}
                    return (isDown && !isDownPrevious);
                }
                break;
            case "Left":
                if (IsJoyStickEnable())
                {
                    return (isLeft && !isLeftPrevious);
                }
                break;
            case "Right":
                if (IsJoyStickEnable())
                {
                    return (isRight && !isRightPrevious);
                }
                break;
            case "L2":
                if (IsJoyStickEnable())
                {
                    return (isL2 && !isL2Previous);
                }
                break;
            case "R2":
                if (IsJoyStickEnable())
                {
                    return (isR2 && !isR2Previous);
                }
                break;
            case "Circle":
                if (IsJoyStickEnable())
                {
                    //if (Input.GetButtonDown("Circle"))
                    //{
                    //    print("Circle");
                    //}
                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        return false;
                    }
                }
                break;
            case "Cross":
                if (IsJoyStickEnable())
                {
                    //if (Input.GetButtonDown("Cross"))
                    //{
                    //    print("Cross");
                    //}
                    if (Input.GetKeyDown(KeyCode.Mouse1))
                    {
                        return false;
                    }
                }
                break;
        }
        return Input.GetButtonDown(button);
    }

    public bool IsInputUp(string button)
    {
        if (Gamepad.current != null && (Gamepad.current is DualShockGamepad))
        {
            button = PS4ButtonToXboxButton(button);
        }
        switch (button)
        {
            case "PS4RightStickUp":
                if (IsJoyStickEnable())
                {
                    return (!isPS4RightStickUp && isPS4RightStickUpPrevious);
                }
                break;
            case "PS4RightStickDown":
                if (IsJoyStickEnable())
                {
                    return (!isPS4RightStickDown && isPS4RightStickDownPrevious);
                }
                break;
            case "PS4Up":
                if (IsJoyStickEnable())
                {
                    //if (isUp && !isUpPrevious)
                    //{
                    //    print("Up");
                    //}
                    return (!isPS4Up && isPS4UpPrevious);
                }
                break;
            case "PS4Down":
                if (IsJoyStickEnable())
                {
                    //if (isDown && !isDownPrevious)
                    //{
                    //    print("Down");
                    //}
                    return (!isPS4Down && isPS4DownPrevious);
                }
                break;
            case "PS4Left":
                if (IsJoyStickEnable())
                {
                    return (!isPS4Left && isPS4LeftPrevious);
                }
                break;
            case "PS4Right":
                if (IsJoyStickEnable())
                {
                    return (!isPS4Right && isPS4RightPrevious);
                }
                break;
            case "RightStickUp":
                if (IsJoyStickEnable())
                {
                    return (!isRightStickUp && isRightStickUpPrevious);
                }
                break;
            case "RightStickDown":
                if (IsJoyStickEnable())
                {
                    return (!isRightStickDown && isRightStickDownPrevious);
                }
                break;
            case "Up":
                if (IsJoyStickEnable())
                {
                    //if (isUp && !isUpPrevious)
                    //{
                    //    print("Up");
                    //}
                    return (!isUp && isUpPrevious);
                }
                break;
            case "Down":
                if (IsJoyStickEnable())
                {
                    //if (isDown && !isDownPrevious)
                    //{
                    //    print("Down");
                    //}
                    return (!isDown && isDownPrevious);
                }
                break;
            case "Left":
                if (IsJoyStickEnable())
                {
                    return (!isLeft && isLeftPrevious);
                }
                break;
            case "Right":
                if (IsJoyStickEnable())
                {
                    return (!isRight && isRightPrevious);
                }
                break;
            case "L2":
                if (IsJoyStickEnable())
                {
                    return (!isL2 && isL2Previous);
                }
                break;
            case "R2":
                if (IsJoyStickEnable())
                {
                    return (!isR2 && isR2Previous);
                }
                break;
            case "Circle":
                if (IsJoyStickEnable())
                {
                    //if (Input.GetButtonDown("Circle"))
                    //{
                    //    print("Circle");
                    //}
                    if (Input.GetKeyUp(KeyCode.Mouse0))
                    {
                        return false;
                    }
                }
                break;
            case "Cross":
                if (IsJoyStickEnable())
                {
                    //if (Input.GetButtonDown("Cross"))
                    //{
                    //    print("Cross");
                    //}
                    if (Input.GetKeyUp(KeyCode.Mouse1))
                    {
                        return false;
                    }
                }
                break;
        }
        return Input.GetButtonUp(button);
    }

    //private void OnEnable()
    //{
    //    if (IsJoyStickEnable())
    //    {
    //        joyStickControls.JoyStick.Enable();
    //    }
    //}

    //private void OnDisable()
    //{
    //    if (IsJoyStickEnable())
    //    {
    //        joyStickControls.JoyStick.Disable();
    //    }
    //}

    public string PS4ButtonToXboxButton(string _button)
    {
        switch (_button)
        {
            case "Up":
                return "PS4Up";
                break;
            case "Down":
                return "PS4Down";
                break;
            case "Left":
                return "PS4Left";
                break;
            case "Right":
                return "PS4Right";
                break;
            case "RightStickUp":
                return "PS4RightStickUp";
                break;
            case "RightStickDown":
                return "PS4RightStickDown";
                break;
            //case "RightStickLeft":

            //    break;
            //case "RightStickRight":

            //    break;
            case "Circle":
                return "Square";
                break;
            case "Cross":
                return "Circle";
                break;
            case "Triangle":
                return "Triangle";
                break;
            case "Square":
                return "Cross";
                break;
            case "L1":
                return "L1";
                break;
            case "R1":
                return "R1";
                break;
            case "L2":
                return "Select";
                break;
            case "R2":
                return "Escape";
                break;
            case "Escape":
                return "L2";
                break;
        }

        return _button;
    }

    bool CheckAnyGamepadButtonPressed()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton0))
        {
            return true;
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton1))
        {
            return true;
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton2))
        {
            return true;
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton3))
        {
            return true;
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton4))
        {
            return true;
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton5))
        {
            return true;
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton6))
        {
            return true;
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton7))
        {
            return true;
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton8))
        {
            return true;
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton9))
        {
            return true;
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton10))
        {
            return true;
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton11))
        {
            return true;
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton12))
        {
            return true;
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton13))
        {
            return true;
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton14))
        {
            return true;
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton15))
        {
            return true;
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton16))
        {
            return true;
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton17))
        {
            return true;
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton18))
        {
            return true;
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton19))
        {
            return true;
        }
        if (Mathf.Abs(Input.GetAxis("DPad X")) > 0.5f)
        {
            return true;
        }
        if (Mathf.Abs(Input.GetAxis("DPad Y")) > 0.5f)
        {
            return true;
        }
        if (Mathf.Abs(Input.GetAxis("PS4 DPad X")) > 0.5f)
        {
            return true;
        }
        if (Mathf.Abs(Input.GetAxis("PS4 DPad Y")) > 0.5f)
        {
            return true;
        }
        if (Mathf.Abs(Input.GetAxis("Xaxis")) > 0.5f)
        {
            return true;
        }
        if (Mathf.Abs(Input.GetAxis("Yaxis")) > 0.5f)
        {
            return true;
        }

        return false;
    }

    bool CheckAnyKeyboardAndMousePressed()
    {
        if (Input.GetMouseButton(0))
        {
            return true;
        }
        if (Input.GetMouseButton(1))
        {
            return true;
        }
        if (Input.GetMouseButton(2))
        {
            return true;
        }
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            return true;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            return true;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            return true;
        }
        if (Input.GetKeyDown(KeyCode.Backslash))
        {
            return true;
        }
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            return true;
        }

        return false;
    }
}

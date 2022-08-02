using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PointerOverride : MonoBehaviour
{
    // Start is called before the first frame update
    public bool UseJoystick = false;
    public float AimDist = 5.0f;
    public float AimSpeed = 300.0f;
    public Vector3 Offset;
    public Vector2 bottomLeft, topRight;
    public static PointerOverride Player1Pointer, Player2Pointer;

    bool Faster = false;

    public Player playerInput;
    public int PlayerIndex = 0;
    private void Awake()
    {
        if (PlayerIndex == 0)
            Player1Pointer = this;
        else
            Player2Pointer = this;
    }

    void Start()
    {
        if (PlayerIndex == 1 && PlayerController.Player2 == null)
            gameObject.SetActive(false);
        playerInput = new Player();
        playerInput.Enable();

      
    }
    
    public void FasterBtn(InputAction.CallbackContext ctx)
    {
        Faster = ctx.ReadValueAsButton();
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.visible = false;

        if (PlayerIndex == 1
            || (PlayerIndex == 0 && PlayerController.Player2 == null))
        {
            Vector3 dir = playerInput.Controls.JoypadAim.ReadValue<Vector2>().normalized;
#if UNITY_WEBGL
            dir.y *= -1;
#endif
            if (dir.magnitude > 0.5f)
            {
                transform.position += dir * ((Faster) ? AimSpeed*1.5f:AimSpeed) * Time.deltaTime;
            }
        }
    }
    Vector3 joyPadDir = Vector3.zero;
    

    public void CursorPos(InputAction.CallbackContext ctx)
    {
        if (PlayerIndex == 0 && PlayerController.Player2 == null)
        {
            
        }
        transform.position = ctx.ReadValue<Vector2>();
    }
}

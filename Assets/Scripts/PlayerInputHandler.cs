using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerInputHandler : MonoBehaviour
{
    public bool InvertYAxis;//是否视角翻转
    public float ViewMultical;//视角灵敏度

    PlayerCharaterController m_playerCharaterController;

    void Start() {

        UnityEngine.Cursor.visible = false;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
    }

    /// <summary>
    /// 鼠标处于界面内且Player没死
    /// </summary>
    /// <returns></returns>
    bool MouseInsideGameAndPlayerNoDead()
    {
        //TODO:: Player no dead 要加进判断
        return UnityEngine.Cursor.lockState == CursorLockMode.Locked;
    }

    /// <summary>
    /// 得到鼠标/手柄移动值
    /// </summary>
    /// <param name="k_AxisName"></param>
    /// <param name="k_AxisNameJoyStick"></param>
    /// <returns></returns>
    public float GetMouseOrStickLookAxis(string k_AxisName,string k_AxisNameJoyStick)
    {
        if (MouseInsideGameAndPlayerNoDead())
        {
            
            bool isGamepad = (Input.GetAxis(k_AxisNameJoyStick) != 0f);
            float i = isGamepad ? Input.GetAxis(k_AxisNameJoyStick) : Input.GetAxisRaw(k_AxisName);

            if (InvertYAxis)
                i *= -1f;

            i *= ViewMultical;

            if (isGamepad)
            {
                i *= Time.deltaTime;
            }
            else
            {
                i *= 0.01f;
            }

            return i;
        }

        return 0f;
    }

    public bool SprintInput()
    {
        if(MouseInsideGameAndPlayerNoDead())
         return Input.GetButton(GameConstants.k_ButtonNameSprint);

        return false;
    }
}

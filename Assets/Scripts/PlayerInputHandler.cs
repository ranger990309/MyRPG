using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerInputHandler : MonoBehaviour
{
    public bool InvertYAxis;//�Ƿ��ӽǷ�ת
    public float ViewMultical;//�ӽ�������

    PlayerCharaterController m_playerCharaterController;

    void Start() {

        UnityEngine.Cursor.visible = false;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
    }

    /// <summary>
    /// ��괦�ڽ�������Playerû��
    /// </summary>
    /// <returns></returns>
    bool MouseInsideGameAndPlayerNoDead()
    {
        //TODO:: Player no dead Ҫ�ӽ��ж�
        return UnityEngine.Cursor.lockState == CursorLockMode.Locked;
    }

    /// <summary>
    /// �õ����/�ֱ��ƶ�ֵ
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

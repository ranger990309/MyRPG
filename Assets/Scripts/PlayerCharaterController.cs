using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharaterController : MonoBehaviour
{
    public float rotationSpeed = 200;//移动相机时的旋转速度
    public float viewViticalAngle;
    float TargetCharacterHeight;

    public bool isCrouching { get; private set; }

    public Camera playerCamera;
    PlayerInputHandler playerInputHandler;

    void Start()
    {
        playerInputHandler = GetComponent<PlayerInputHandler>();
    }


    void Update()
    {
        HandleCharaterMovement();
    }

    public void HandleCharaterMovement(){
        {//TODO: 摄像机的移动功能应该放在LateUpdate里,记得改
            // (视角左右动)旋转角色Y轴
            transform.Rotate(new Vector3(0f, playerInputHandler.GetMouseOrStickLookAxis(GameConstants.k_AxisNameMouseHorizontal,GameConstants.k_AxisNameJoyStickLookHorizontal)* rotationSpeed, 0f),Space.Self);
        }
        {
            // (视角上下动)摄像机垂直旋转
            viewViticalAngle += playerInputHandler.GetMouseOrStickLookAxis(GameConstants.k_AxisNameMouseVertical, GameConstants.k_AxisNameJoyStickLookVertical)* rotationSpeed;

            viewViticalAngle = Mathf.Clamp(viewViticalAngle, -89f, 89f);

            playerCamera.transform.localEulerAngles = new Vector3( viewViticalAngle, 0f,0f);
        }

        //人物移动
        //是否按下冲刺按键
        bool isSprinting = playerInputHandler.SprintInput();
        
        if (isSprinting)
        {   //冲刺就要取消下蹲状态
            isSprinting = SetCrouchState(false, false);
        }
        //冲刺还要改变速度倍增器的速度(这东西待会要乘在速度上加速的)
        float SpeedModifier = isSprinting ? 2f : 1f;
        
        // 拿到移动输入拿到的Vector3值(Local Space)->World Space的Vector3值(这里就要去Input脚本写移动输入了)


        bool SetCrouchState(bool WangtToCrouch,bool ignoreObstructions)
        {
            //下蹲 改变身高
            if (WangtToCrouch)
                TargetCharacterHeight = 0.9f;
            else//站立
            {
                if (!ignoreObstructions)
                {
                    //TODO: 站起来需要检测障碍物再高度变化
                    //若障碍物挡到头
                    return false;
                }
                TargetCharacterHeight = 1.8f;
            }

            //TODO: 触发站立事件
            isCrouching= WangtToCrouch;
            return true;
        }

    }
}

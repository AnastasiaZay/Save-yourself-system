using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PlayerMove : MonoBehaviour
{

    //Переменные для тачпада и заднего курка
    private SteamVR_Action_Vector2 touchpad = null;
    private SteamVR_Action_Boolean m_Boolean = null;

    private CharacterController controller = null;

    //Скорость
    public float playerSpeed = 1f;

    //Переменная для проверки, нужно ли перемещать игрока
    private bool checkWalk = false;


    private void Awake()
    {
        touchpad = SteamVR_Actions._default.Touchpad;
        m_Boolean = SteamVR_Actions._default.TouchClick;
        controller = GetComponent<CharacterController>();



    }


    // Update is called once per frame
    private void Update()
    {
        if (touchpad.axis.magnitude > 0.1f)
        {
            Vector3 direction = Player.instance.hmdTransform.TransformDirection(new Vector3(touchpad.axis.x, 0, touchpad.axis.y));
            controller.Move(playerSpeed * Time.deltaTime * Vector3.ProjectOnPlane(direction, Vector3.up) - new Vector3(0, 9.81f, 0) * Time.deltaTime);
        }

    }

    // Start is called before the first frame update
    private void Start()
    {
        
    }

   
}

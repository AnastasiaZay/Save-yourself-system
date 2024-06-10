using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PlayerMove : MonoBehaviour
{

    //Переменные для тачпада и заднего курка
    private SteamVR_Action_Vector2 touchpad = null;
    private SteamVR_Action_Boolean m_Boolean = null;

    private CharacterController controller;

    public LayerMask groundLayer;

    //Скорость
    public float playerSpeed = 1f;

    //Переменная для проверки, нужно ли перемещать игрока
    private bool checkWalk = false;

    public float gravity = -9.81f;
    private float fallSpeed;

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


        //Gravity
        bool isGrounded = CheckIsGround();
        if (isGrounded)
            fallSpeed = 0;
        else
            fallSpeed += gravity * Time.fixedDeltaTime;

        controller.Move(Vector3.up * fallSpeed * Time.fixedDeltaTime);

    }

    // Start is called before the first frame update
    private void Start()
    {
        ///Для спавна
        SpawnPoint.GetRespawn(gameObject);
    }

    bool CheckIsGround()
    {
        Vector3 rayStart = transform.TransformPoint(controller.center);
        float rayLength = controller.center.y + 0.01f;
        bool hasHit = Physics.SphereCast(rayStart,
            controller.radius,
            Vector3.down,
            out RaycastHit hitInfo,
            rayLength,
            groundLayer);
        return hasHit;
    }
}

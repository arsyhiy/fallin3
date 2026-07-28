/* PlayerInputHandler.cs: получаем инпут и отправляем в PlayerMovement

проще говоря файл принимает все input'ы от игрока и обрабатывает их и передает в PlayerMovement 

*/

using Unity.Game;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Unity.Player
{
    public class PlayerInputHandler : MonoBehaviour
    {

        [Tooltip("Sensitivity multiplier for moving the camera around")]
        public float LookSensitivity = 2f; // mouse sensitivity

        public float speed = 5f; // player's movement speed

        public float gravity = -9.81f;

        GameFlowManager m_GameFlowManager; // используется для логирования состояние GameFlowManager через DebugUtility 

        PlayerMovement m_PlayerCharacterController; // это класс PlayerMovement, используется для формирования лого через DebugUtility

        [Tooltip("Used to flip the vertical input axis")]
        public bool InvertYAxis = false;

        [Tooltip("Used to flip the horizontal input axis")]
        public bool InvertXAxis = false;

        private InputAction m_MoveAction;
        private InputAction m_LookAction;
        private InputAction m_JumpAction;
        private InputAction m_SprintAction;
        private InputAction m_CrouchAction;

        void Start()
        {
            m_PlayerCharacterController = GetComponent<PlayerMovement>();

            DebugUtility.HandleErrorIfNullGetComponent<PlayerMovement, PlayerInputHandler>(
                m_PlayerCharacterController, this, gameObject);

            m_GameFlowManager = FindAnyObjectByType<GameFlowManager>();

            DebugUtility.HandleErrorIfNullFindObject<GameFlowManager, PlayerInputHandler>(m_GameFlowManager, this);

            Cursor.lockState = CursorLockMode.Locked; // фиксируем курсор на месте
            Cursor.visible = false; // скрываем курсор с экрана игрока

            m_MoveAction = InputSystem.actions.FindAction("Player/Move");
            m_LookAction = InputSystem.actions.FindAction("Player/Look");
            m_JumpAction = InputSystem.actions.FindAction("Player/Jump");
            m_SprintAction = InputSystem.actions.FindAction("Player/Sprint");
            m_CrouchAction = InputSystem.actions.FindAction("Player/Crouch");
            
            m_MoveAction.Enable();
            m_LookAction.Enable();
            m_JumpAction.Enable();
            m_SprintAction.Enable();
            m_CrouchAction.Enable();
        }

        public bool CanProcessInput()
        {
            return Cursor.lockState == CursorLockMode.Locked;
        }

        public Vector3 GetMoveInput()
        {
            if (CanProcessInput())
            {
                var input = m_MoveAction.ReadValue<Vector2>();
                Vector3 move = new Vector3(input.x, 0f, input.y);

                // constrain move input to a maximum magnitude of 1, otherwise diagonal movement might exceed the max move speed defined
                move = Vector3.ClampMagnitude(move, 1);

                return move;
            }

            return Vector3.zero;
        }

        public float GetLookInputsHorizontal()
        {
            if (!CanProcessInput())
                return 0.0f;
            
            float input = m_LookAction.ReadValue<Vector2>().x;

            if (InvertXAxis)
                input *= -1;

            input *= LookSensitivity;
            
            return input;
        }

        public float GetLookInputsVertical()
        {
            if (!CanProcessInput())
                return 0.0f;
            
            float input = m_LookAction.ReadValue<Vector2>().y;

            if (InvertYAxis)
                input *= -1;

            input *= LookSensitivity;
            
            return input;
        }

        public bool GetJumpInputDown()
        {
            if (CanProcessInput())
            {
                return m_JumpAction.WasPressedThisFrame();
            }

            return false;
        }

        public bool GetJumpInputHeld()
        {
            if (CanProcessInput())
            {
                return m_JumpAction.IsPressed();
            }

            return false;
        }

        public bool GetSprintInputHeld()
        {
            if (CanProcessInput())
            {
                return m_SprintAction.IsPressed();
            }

            return false;
        }

        // наверное оставлю как закоментированный если захочу переписать приседание
        // public bool GetCrouchInputDown()
        // {
        //     if (CanProcessInput())
        //     {
        //         return m_CrouchAction.WasPressedThisFrame();
        //     }
        // 
        //     return false;
        // }

        public bool GetCrouchInput()
        {
            if (CanProcessInput())
            {
                return m_CrouchAction.IsPressed();
            }

            return false;
        }

    }
}

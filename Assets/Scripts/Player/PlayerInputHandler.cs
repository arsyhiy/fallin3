/* PlayerInputHandler.cs: получаем инпут и отправляем дальше (конец переписать) 

проще говоря файл принимает inputs и и отправляет ответы в более чистый ответ 
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


        GameFlowManager m_GameFlowManager;

        PlayerMovement m_PlayerCharacterController; // рядом файл который ..... 
        bool m_FireInputWasHeld; // как понимаю зажата кнопка выстрела



        [Tooltip("Used to flip the vertical input axis")]
        public bool InvertYAxis = false;

        [Tooltip("Used to flip the horizontal input axis")]
        public bool InvertXAxis = false;



        private InputAction m_MoveAction;
        private InputAction m_LookAction;
        private InputAction m_JumpAction;
        private InputAction m_FireAction;
        private InputAction m_AimAction;
        private InputAction m_SprintAction;
        private InputAction m_CrouchAction;
        private InputAction m_ReloadAction;
        private InputAction m_NextWeaponAction;

        void Start()
        {
            m_PlayerCharacterController = GetComponent<PlayerMovement>();

            // DebugUtility.HandleErrorIfNullGetComponent<PlayerMovement, PlayerInputHandler>(
            //     m_PlayerCharacterController, this, gameObject);

            m_GameFlowManager = FindAnyObjectByType<GameFlowManager>();

            // DebugUtility.HandleErrorIfNullFindObject<GameFlowManager, PlayerInputHandler>(m_GameFlowManager, this);

            Cursor.lockState = CursorLockMode.Locked; // зафексировали курсор
            Cursor.visible = false; // теперь мы его точно не увидем

            // проще говоря этот кусок кода до пустой строки ожидает или ищет  произведение action и иммено конкретные
            m_MoveAction = InputSystem.actions.FindAction("Player/Move");
            m_LookAction = InputSystem.actions.FindAction("Player/Look");
            m_JumpAction = InputSystem.actions.FindAction("Player/Jump");
            m_FireAction = InputSystem.actions.FindAction("Player/Fire");
            m_AimAction = InputSystem.actions.FindAction("Player/Aim");
            m_SprintAction = InputSystem.actions.FindAction("Player/Sprint");
            m_CrouchAction = InputSystem.actions.FindAction("Player/Crouch");
            m_ReloadAction = InputSystem.actions.FindAction("Player/Reload");
            m_NextWeaponAction = InputSystem.actions.FindAction("Player/NextWeapon");
            
            m_MoveAction.Enable();
            m_LookAction.Enable();
            m_JumpAction.Enable();
            m_FireAction.Enable();
            m_AimAction.Enable();
            m_SprintAction.Enable();
            m_CrouchAction.Enable();
            m_ReloadAction.Enable();
            m_NextWeaponAction.Enable();
        }

        public bool CanProcessInput()
        {
            return Cursor.lockState == CursorLockMode.Locked;
        }

        public Vector3 GetMoveInput()
        // из прочитаного понял что только на движение игрока но не камеры
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
        // получаем кординаты камера а точнее горизонтальное значение
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
        // как верхние но вертикаль
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
        // проверям нажатие jump
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


        public bool GetAimInputHeld()
        {
            if (CanProcessInput())
            {
                return m_AimAction.IsPressed();
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

        public bool GetCrouchInputDown()
        {
            if (CanProcessInput())
            {
                return m_CrouchAction.WasPressedThisFrame();
            }

            return false;
        }

    }
}

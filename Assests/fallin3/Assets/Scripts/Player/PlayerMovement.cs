// /* PlayerMovement.cs: производим действия по нажатиям игрока
// 
// проще говоря получаем инпут от InputPlayerHandler и делаем здесь
// 
// */

using UnityEngine;

namespace Unity.Player
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(PlayerInputHandler))]

    public class PlayerMovement : MonoBehaviour
    {
        public CharacterController controller;
        public Transform playerCamera;

        [Header("Movement")]
        public float moveSpeed = 5f;
        public float jumpHeight = 2f;
        public float gravity = -20f;

        [Header("Look")]
        public float rotationSpeed = 200f; 

        private PlayerInputHandler inputHandler;

        private Vector3 velocity;
        private float cameraPitch;
        
        [Header("Crouch")]
        public float normalHeight = 2f;
        public float crouchHeight = 1f;
        public float crouchSpeed = 2f;

        void Start()
        {
            controller = GetComponent<CharacterController>();
            inputHandler = GetComponent<PlayerInputHandler>();

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        void Update()
        {
            Look();
            Move();
            Crouch();
        }

        // utility functions
        
        void DebugInput()
        {
            if (inputHandler.GetJumpInputDown())
            {
                Debug.Log("Jump");
            }

            Vector3 move = inputHandler.GetMoveInput();

            if (move != Vector3.zero)
            {
                Debug.Log("Move: " + move);
            }

            float mouseX = inputHandler.GetLookInputsHorizontal();
            float mouseY = inputHandler.GetLookInputsVertical();

            if (mouseX != 0 || mouseY != 0)
            {
                Debug.Log($"Mouse: {mouseX} {mouseY}");
            }
        }
        

        void Look()
        {
            float mouseX = inputHandler.GetLookInputsHorizontal();
            float mouseY = inputHandler.GetLookInputsVertical();


            // вращение тела игрока
            transform.Rotate(
                Vector3.up *
                mouseX *
                rotationSpeed *
                Time.deltaTime
            );


            // вращение камеры

            cameraPitch -=
                mouseY *
                rotationSpeed *
                Time.deltaTime;


            cameraPitch = Mathf.Clamp(
                cameraPitch,
                -90f,
                90f
            );


            playerCamera.localRotation =
                Quaternion.Euler(
                    cameraPitch,
                    0f,
                    0f
                );
        }


        void Move()
        {
            // движение вперед/назад/влево/вправо

            Vector3 input =
                inputHandler.GetMoveInput();


            Vector3 move =
                transform.TransformDirection(input);


            Vector3 finalMove =
                move * moveSpeed;

            // проверка земли

            if (controller.isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            // прыжок

            if (
                inputHandler.GetJumpInputDown()
                &&
                controller.isGrounded
            )
            {
                velocity.y =
                    Mathf.Sqrt(
                        jumpHeight *
                        -2f *
                        gravity
                    );
            }

            // гравитация

            velocity.y += gravity * Time.deltaTime;

            // объединяем горизонтальное и вертикальное движение

            finalMove.y = velocity.y;

            controller.Move(
                finalMove * Time.deltaTime
            );
        }

        void Crouch()
        {
            if(inputHandler.GetCrouchInput())
            {
                controller.height = crouchHeight;
                moveSpeed = crouchSpeed;
            }
            else
            {
                controller.height = normalHeight;
                moveSpeed = 5f;
            }
        }
    }
}

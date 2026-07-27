/* PlayerMovement.cs: производим действия по нажатиям игрока

проще говоря получаем инпут от InputPlayerHandler и делаем здесь

*/


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

        private float cameraPitch = 0f;


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
        }

        void Look()
        {

            float mouseX =
                inputHandler.GetLookInputsHorizontal();

            float mouseY =
                inputHandler.GetLookInputsVertical();

            // вращаем тело игрока вправо/влево
            transform.Rotate(
                Vector3.up *
                mouseX *
                rotationSpeed *
                Time.deltaTime
            );

            // вращаем камеру вверх/вниз

            cameraPitch -= mouseY * rotationSpeed * Time.deltaTime;

            cameraPitch =
                Mathf.Clamp(
                    cameraPitch,
                    -90f,
                    90f
                );

            playerCamera.localEulerAngles =
                new Vector3(
                    cameraPitch,
                    0f,
                    0f
                );
        }

        void Move()
        {

            Vector3 input =
                inputHandler.GetMoveInput();

            Vector3 move =
                transform.TransformDirection(input);

            controller.Move(
                move *
                moveSpeed *
                Time.deltaTime
            );


            // проверка земли

            if(controller.isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            // прыжок

            if(
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

            velocity.y +=
                gravity *
                Time.deltaTime;

            controller.Move(
                velocity *
                Time.deltaTime
            );
        }
    }
}

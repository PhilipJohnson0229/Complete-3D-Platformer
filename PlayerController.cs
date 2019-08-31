using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DillonsDream
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController instance;

        public float moveSpeed;
        public float jumpForce;
        public float gravityScale = 5f;
        public float rotateSpeed;

        private Camera theCam;

        public GameObject playerModel;

        public Animator anim;

        public bool isKnocking;
        public float knockBackLength = 0.5f;
        private float knockBackCounter;
        public Vector2 knockBackPower;

        public CharacterController charController;
        private Vector3 moveDirection;
        public GameObject[] playerPeices;
        // Start is called before the first frame update
        void Awake()
        {
            instance = this;
            charController = this.transform.GetComponent<CharacterController>();
            theCam = Camera.main;
        }

        // Update is called once per frame
        void Update()
        {
            if (!isKnocking)
            {
                float yStore = moveDirection.y;
                // moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
                moveDirection = (transform.forward * Input.GetAxisRaw("Vertical")) + (transform.right * Input.GetAxisRaw("Horizontal"));
                moveDirection.Normalize();
                moveDirection = moveDirection * moveSpeed;
                moveDirection.y = yStore;

                if (charController.isGrounded)
                {
                    moveDirection.y = 0f;

                    if (Input.GetButtonDown("Jump"))
                    {
                        moveDirection.y = jumpForce;
                    }

                }

                moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale;
                charController.Move(moveDirection * Time.deltaTime);

                if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
                {
                    //quaternion.eurlor converts quaternion to vector3
                    transform.rotation = Quaternion.Euler(0, theCam.transform.rotation.eulerAngles.y, 0);
                    Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
                    //playerModel.transform.rotation = newRotation;
                    playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
                }
            }

            if (isKnocking)
            {
                knockBackCounter -= Time.deltaTime;

                float yStore = moveDirection.y;
                moveDirection = playerModel.transform.forward * -knockBackPower.x;
                moveDirection.y = yStore;

                if (charController.isGrounded)
                {
                    moveDirection.y = 0f;

                }

                moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale;

                charController.Move(moveDirection * Time.deltaTime);

                if (knockBackCounter <= 0)
                {
                    isKnocking = false;
                }
            }
           

            anim.SetFloat("Speed", Mathf.Abs(moveDirection.x) + Mathf.Abs(moveDirection.z));
            anim.SetBool("Grounded", charController.isGrounded);
        }

        public void KnockBack()
        {
            isKnocking = true;
            knockBackCounter = knockBackLength;
            Debug.Log("Knocked Back!");
            moveDirection.y = knockBackPower.y;
            charController.Move(moveDirection * Time.deltaTime);

        }
    }

}

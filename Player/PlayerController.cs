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
        public Vector3 moveDirSnapshot;

        public float bounceForce = 8f;

        private Camera theCam;

        public GameObject playerModel;

        public Animator anim;

        public bool isKnocking;
        public float knockBackLength = 0.5f;
        private float knockBackCounter;
        public Vector2 knockBackPower;

        public CharacterController charController;
        [SerializeField]
        private Vector3 moveDirection;
        public GameObject[] playerPeices;

        public GameObject hurtBox;

        public bool stopMove;

        public bool grounded;
        //trying to cereate ledge grab mechanic
        public bool wallFound;
        public bool ledgeFound;
        public bool ignoringLedge;
        public bool climbUp;
        public Transform wallDetector;
        public Transform ledgeDetector;
        public Vector3 calculatedClimbPoint;

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
            grounded = charController.isGrounded;

            if (!isKnocking && !stopMove)
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
                    hurtBox.SetActive(false);
                    if (Input.GetButtonDown("Jump"))
                    {
                        moveDirection.y = jumpForce;
                        hurtBox.SetActive(true);
                       
                    }
                    //ledge grab
                    wallDetector.gameObject.SetActive(false);
                    ledgeDetector.gameObject.SetActive(false);

                    wallFound = false;
                    moveDirSnapshot.x = moveDirection.x;
                    moveDirSnapshot.z = moveDirection.z;
                }

                //ledge grab
                if (!charController.isGrounded && !isKnocking)
                {
                   // moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale;
                    moveDirection.x = moveDirSnapshot.x;
                    moveDirection.z = moveDirSnapshot.z;
                    //ledge grab
                    wallDetector.gameObject.SetActive(true);

                    Debug.DrawRay(wallDetector.position, wallDetector.forward * 0.7f, Color.yellow);

                    RaycastHit hit;

                    if (Physics.Raycast(wallDetector.position, wallDetector.forward, out hit, 0.7f))
                    {
                        if (hit.transform.gameObject.tag == "Wall")
                        {
                            wallFound = true;
                        }
                       
                        
                    }
                    Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, moveDirSnapshot.y, moveDirection.z));
                    if (moveDirSnapshot == Vector3.zero)
                    {
                        playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, playerModel.transform.rotation, 0);
                    }
                    else
                    {
                        playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
                    }
                   
                   
                    
                }

                if (wallFound)
                {
                   
                    //Debug
                   /* if (UIManager.instance.wallCheckText.text != null)
                    {
                        UIManager.instance.wallCheckText.text = "Wall Found";
                    }*/

                    if (!ignoringLedge)
                    {
                        ledgeDetector.gameObject.SetActive(true);

                        Debug.DrawRay(ledgeDetector.position, -ledgeDetector.up * 0.7f, Color.yellow);

                        RaycastHit hit;

                        if (Physics.Raycast(ledgeDetector.position, -ledgeDetector.up, out hit, 0.7f))
                        {
                            Debug.Log(hit.normal);
                            if (hit.normal.x == 0)
                            {
                                ledgeFound = true;
                                calculatedClimbPoint = hit.point;
                            }
                        }
                    }   
                }
                else
                {
                    //Debug
                   /* if (UIManager.instance.wallCheckText.text != null)
                    {
                        UIManager.instance.wallCheckText.text = "No Wall Present";
                    }
                    else { return; }*/
                }

                if (ledgeFound)
                {
                    stopMove = true;
                    anim.SetBool("Hanging", true);
                    //UIManager.instance.LedgeCheckText.text = "Ledge Found";
                }
                else
                {
                    anim.SetBool("Hanging", false);
                   // UIManager.instance.LedgeCheckText.text = "No Ledge Present";
                }

               
                moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale;
                charController.Move(moveDirection * Time.deltaTime);

                if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
                {
                   
                    //quaternion.eurlor converts quaternion to vector3
                    transform.rotation = Quaternion.Euler(0, theCam.transform.rotation.eulerAngles.y, 0);
                    wallDetector.rotation = Quaternion.Euler(0, playerModel.transform.rotation.eulerAngles.y, 0);
                    Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));

                    //playerModel.transform.rotation = newRotation;
                    if (charController.isGrounded)
                    {
                        playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
                    }
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

            if (stopMove)
            {
                moveDirection = Vector3.zero;

                if (!wallFound)
                {
                    moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale;
                }
               
                charController.Move(moveDirection);
            }

            anim.SetFloat("Speed", Mathf.Abs(moveDirection.x) + Mathf.Abs(moveDirection.z));
            anim.SetBool("Grounded", charController.isGrounded);
            climbUp = anim.GetBool("ClimbUp");
        }

        void FixedUpdate()
        {

            if (climbUp)
            {

                gameObject.transform.position = calculatedClimbPoint;
                StartCoroutine(Stall());

            }
        }

        public void KnockBack()
        {
            isKnocking = true;
            knockBackCounter = knockBackLength;
            Debug.Log("Knocked Back!");
            moveDirection.y = knockBackPower.y;
            charController.Move(moveDirection * Time.deltaTime);
            hurtBox.SetActive(false);
        }

        public void Bounce()
        {
            moveDirection.y = bounceForce;
            charController.Move(moveDirection * Time.deltaTime);
            hurtBox.SetActive(false);
        }

        public void FallFromLedge()
        {
            ledgeFound = false;

            anim.SetBool("Hanging", false);

            stopMove = false;

            StartCoroutine(IgnoreLedge());
        }
        public IEnumerator IgnoreLedge()
        {
            ignoringLedge = true;

            yield return new WaitForSeconds(.5f);

            ignoringLedge = false;
        }

        public IEnumerator Stall()
        {
            yield return new WaitForSeconds(.2f);
            wallFound = false;
            ledgeFound = false;
            anim.SetBool("ClimbUp", false);
            yield return new WaitForSeconds(.2f);
            stopMove = false;
        }

        //Wall jumping
        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (!charController.isGrounded && hit.transform.gameObject.tag == "Wall" && hit.normal.y < 0.1)
            {
                if (Input.GetButtonDown("Jump"))
                {

                    //  moveDirection.y = jumpForce;
                    //  hurtBox.SetActive(true);
                    Debug.DrawRay(hit.point, hit.normal, Color.red, 1.25f);
                    Debug.Log(hit.normal);
                    moveDirection.y = jumpForce;
                    moveDirSnapshot.x = hit.normal.x * 5;
                    moveDirSnapshot.z = hit.normal.z * 5;
                    moveDirSnapshot.y = 0;
                   // Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirSnapshot.x, 0f, moveDirSnapshot.z));
                   
                }
               
            }
            
        }
    }

}

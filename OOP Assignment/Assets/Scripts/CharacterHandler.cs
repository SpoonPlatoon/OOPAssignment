using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterHandler
{
    public class CharacterHandler : MonoBehaviour
    {

        #region Variables
        [Header("CHARACTER HANDLER")] //Header in the inspector of unity to classify these variables from the others
        [Space(3)]
        [Header("Movement Connection")] //Header in the inspector of unity to classify these variables from the others
        #region Movement Connection
        public Vector3 moveDir = Vector3.zero;//vector3 called moveDirection we will use this to apply movement in worldspace
        public CharacterController charC;//CharacterController 
        #endregion
        [Space(3)]
        [Header("Movement Variables")] //Header in the inspector of unity to classify these variables from the others
        #region Movement Variables
        //public float variables with presets jumpSpeed 8, speed 6, walkSpeed 6, crouchSpeed 2, sprintSpeed 14, gravity 20
        public float jumpSpeed = 8.0f; //The value of the Y vector 'speed' when the Jump button is pressed
        public float speed = 6.0f;
        public float walk = 6, //The value of standard movement 'speed' when the WASD keys are pressed
                     crouch = 2, //The value of movement 'speed' when the LeftControl and WASD keys are pressed
                     sprint = 14;//The value of  movement 'speed' when the LeftShift and WASD keys are pressed
        public float gravity = 20.0f; //The value of gravity
        #endregion 
        #endregion

        void Start()
        {

        }
        void Update()
        {
            Movement();
            PickUp();
        }
        void Movement()
        {
            if (charC.isGrounded)//IF the character is touching the ground
            {
                //moveDir is equal to a new vector3 that is affected by Input.Get Axis.. Horizontal, 0, Vertical
                moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")); //Sets moveDir to the Horizontal and Vertical inputs. ie WASD/arrow keys (default).

                //moveDir is transformed in the direction of our moveDir
                moveDir = transform.TransformDirection(moveDir); //Transforms direction from local space to world space.

                //our moveDir is then multiplied by our speed
                moveDir *= speed;
                //meaning we are able to move in game


                //we can also jump if we are grounded so
                //in the input button for jump is pressed then our moveDir.y is equal to our jump speed
                if (Input.GetKey(KeyCode.Space)) //Bound the imput key for jump to Space
                {
                    moveDir.y = jumpSpeed; //When Space is pressed the movement direction in the y is equal to the "jumpSpeed"
                }
                if (Input.GetKey(KeyCode.LeftShift)) //Bound the imput key for sprint to LeftShift
                {
                    speed = sprint; //When LeftShift is pressed the movement speed is equal to "sprint"
                }
                else
                {
                    speed = walk; //When LeftShift is released the movement speed is equal to "walk"
                }
                if (Input.GetKey(KeyCode.LeftControl)) //Bound the imput key for crouch to LeftControl
                {
                    speed = crouch; //When LeftControl is pressed the movement speed is equal to "crouch"
                }

            }

            //grounded or not the players moveDir.y is always affected by gravity multiplied by time.deltaTime to become normalised
            moveDir.y -= gravity * Time.deltaTime;
            //Tell the character Controller that it is moving in a direction multiplied by Time.deltaTime
            charC.Move(moveDir * Time.deltaTime);

        }
        void PickUp()
        {
            #region PickUp
            //if our interact key is pressed
            if (Input.GetKey(KeyCode.E))
            {
                Ray interact; //create a ray
                interact = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2)); //this ray is shooting out from the main cameras center point
                RaycastHit hitinfo; //if this physics raycast hits something within 10 units

                if (Physics.Raycast(interact, out hitinfo, 2))
                {
                    Debug.Log("Hit something"); // Debug to state that the player has interacted with something

                    #region NPC tag 
                    //and that hits info is tagged NPC
                    if (hitinfo.collider.CompareTag("NPC"))
                    {
                        //Debug that we hit a NPC
                        Debug.Log("Hit the NPC");
                    }
                    #endregion //If you have NPC in the scene it will debug to say that it has hit it
                    #region Item
                    //and that hits info is tagged Item
                    if (hitinfo.collider.CompareTag("Item"))
                    {
                        //Debug that we hit an Item
                        Debug.Log("Hit an Item");
                    }
                    if (hitinfo.collider.tag == "Item")
                    {
                        Destroy(hitinfo.collider.gameObject); //will destroy item that the raycast has hit
                        gravity = gravity - 2; //on interaction will lower the gravity by 2
                        sprint++; //on interaction will increase the sprint variable by 1
                    }
                    #endregion //If you have Item in the scene it will debug to say that it has hit it and destroy said item (can be used for collection of items or powerups etc)
                }
            }
            #endregion
        }
    }
}

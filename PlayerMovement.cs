using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool onQuest = false;
    private int i;
    private string targetJob;
    private Weapon reward;
 
    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    private float range = 2f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    public float pushPower = 2.0F;

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        // no rigidbody
        if (body == null || body.isKinematic)
            return;

        // We dont want to push objects below us
        if (hit.moveDirection.y < -0.3f)
            return;

        // Calculate push direction from move direction,
        // we only push objects to the sides never up and down
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        // If you know how fast your character is trying to move,
        // then you can also multiply the push velocity by that.

        // Apply the push
        body.velocity = pushDir * pushPower;
    }


    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        //Using .Normalize ensures that the speed doesn't increase while moving diagonally
        Vector3 move = Vector3.Normalize(transform.right * x + transform.forward * z);

        controller.Move(move * speed * Time.deltaTime);
        //checks if character is grounded and no changing y values
        if (isGrounded && velocity.y < 0)
        {
            //slope limit ensures that player doesn't jitter in mid air
            controller.slopeLimit = 45.0f;
            velocity.y = -2f;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            controller.slopeLimit = 100.0f;
            //uses the equation v = square root of 2gh to calculate upward velocity
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        //uses the value for gravity to control how fast the player falls
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        RaycastHit hit;

        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, range))
        {
            
            if(Input.GetKeyDown(KeyCode.F))
            {
                NPC npc = hit.collider.GetComponent<NPC>();
                reward = GameObject.Find("Weapon").GetComponent<Weapon>();
                if(npc != null)
                {
                    if(!onQuest)
                    {
                        GiveQuest(npc);
                        onQuest = true;
                    }
                    else
                    {
                        if(npc.title == targetJob)
                        {
                            Debug.Log($"Quest Complete, you found the {targetJob}");
                            Debug.Log(reward.GiveWeapon(npc));
                            onQuest = false;
                        }
                        else
                        {
                            Debug.Log($"You are already on a quest, go find the {targetJob}");
                        }
                    }
                }
            }
        }
    }
    public void GiveQuest(NPC npc)
    {
        i = Random.Range(0,5);
        targetJob = GameObject.Find("NPCContainer").GetComponent<NPCJobs>().jobTitle[i];
        if(npc.title == targetJob)
        {
            if(i == 4)
            {
                i -= 1;
                targetJob = GameObject.Find("NPCContainer").GetComponent<NPCJobs>().jobTitle[i];
            }
            i += 1;
            targetJob = GameObject.Find("NPCContainer").GetComponent<NPCJobs>().jobTitle[i];
        }
        Debug.Log($"Go and find the {targetJob}");
    }
}


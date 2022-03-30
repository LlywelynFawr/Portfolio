using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public bool onQuest = false;
    private int i;
    private string targetJob;
    private Weapon reward;
    public Text text;
    public GameObject textbox;
    public Text questList;
 
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

    private void Start() {
        textbox.SetActive(false);
        questList.text = "No Current Quest";
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
        closeCanvas();

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
                        textbox.SetActive(true);
                        text.text = $"Go and find the {targetJob}";
                        questList.text = $"Talk to the {targetJob}";
                        pauseGame();
                    }
                    else
                    {
                        if(npc.title == targetJob)
                        {
                            textbox.SetActive(true);
                            text.text = $"Quest Complete, you found the {targetJob},\n{reward.GiveWeapon(npc)}";
                            questList.text = "No Current Quest";
                            onQuest = false;
                            pauseGame();
                        }
                        else
                        {
                           textbox.SetActive(true);
                           text.text = $"You are already on a quest, go find the {targetJob}";
                           pauseGame();
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
    }

    void closeCanvas(){
        if(textbox.activeSelf == true)
        {
            if(Input.GetKeyDown(KeyCode.Return))
            {
                textbox.SetActive(false);
                resumeGame();
            }
        }
    }

    void resumeGame()
    {
        Time.timeScale = 1;
        Cursor.visible = false;
    }

    void pauseGame()
    {
        Time.timeScale = 0;
        Cursor.visible = false;
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignJobs : MonoBehaviour
{
    private GameObject[] npcs;
    private int i;
    private List<int> iValues = new List<int>();
    public string title;
    public Color color;
    public string weapon;
    public GameObject npcContainer;

    private void Start() 
    {
        npcContainer = GameObject.Find("NPCContainer");
        npcs = GameObject.FindGameObjectsWithTag("NPC");
        foreach (GameObject npc in npcs)
        {
            bool flag = false;
            while(flag == false)
            {
                i = Random.Range(0,5);
                if(!iValues.Contains(i))
                {
                    iValues.Add(i);
                    flag = true;
                }
            }
                NPC thisNPC = npc.GetComponent<NPC>();
                thisNPC.title = npcContainer.GetComponent<NPCJobs>().jobTitle[i];
                thisNPC.color = npcContainer.GetComponent<NPCJobs>().colours[i];
                thisNPC.weapon = npcContainer.GetComponent<NPCJobs>().weaponPref[i];
                npc.GetComponent<Renderer>().material.color = thisNPC.color;
        }
    }
}

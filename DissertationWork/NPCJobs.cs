using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCJobs : MonoBehaviour
{
    public List<string> jobTitle = new List<string> {"Blacksmith", "Fletcher", "Butcher", "Farmer", "Wizard"};
    public List<Color> colours = new List<Color> {new Color(0f, 0f, 0f), new Color(1f, 0.92f, 0.016f), new Color(1f, 0f, 0f), new Color(0f, 1f, 0f), new Color(1f, 0f, 1f)};
    public List<string> weaponPref = new List<string> {"Sword", "Bow", "Axe", "Scythe", "Staff"};



    public void NPC(int title, int colour, int weapon)
    {
    }
}

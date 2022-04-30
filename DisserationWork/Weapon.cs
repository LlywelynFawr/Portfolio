using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private string rarity;
    private float modifier;
    private int j;
    private string weaponName;
    private float atk;
    private float critChance;
    private float range;
    private string elemental;
    private string rewardedWeapon;

    public string GiveWeapon(NPC npc)
    {
        j = Random.Range(0,101);
        if(j > 94)
        {
            rarity = "Legendary";
            modifier = 1.5f;
        }
        if(j <= 94 && j > 74)
        {
            rarity = "Rare";
            modifier = 1.25f;
        }
        if(j <= 74 && j > 44)
        {
            rarity = "Uncommon";
            modifier = 1f;
        }
        if(j <= 44)
        {
            rarity = "Common";
            modifier = 0.75f;
        }

        weaponName = npc.weapon;
        if(weaponName == "Bow" || weaponName == "Staff")
        {
            range = Random.Range(5,9);
        }
        else
        {
            range = Random.Range(1,3);
        }

        atk = Random.Range(100, 501) * modifier;
        critChance = Random.Range(0,51) * modifier;
        j = Random.Range(0,101);
        if(j > 90)
        {
            elemental = "Earth";
        }
        if(j > 80 && j <= 90)
        {
            elemental = "Water";
        }
        if(j > 70 && j <= 80)
        {
            elemental = "Fire";
        }
        if(j > 60 && j <= 70)
        {
            elemental = "Air";
        }
        if(j <= 60)
        {
            elemental = "None";
        }
        if(elemental == "None")
        {
            rewardedWeapon = $"You've been given a {rarity} {weaponName}! Attack: {atk} Critical Hit Chance: {critChance}% Range: {range}";
        }
        else
        {
            rewardedWeapon = $"You've been given a {rarity} {weaponName} of {elemental}! Attack: {atk} Critical Hit Chance: {critChance}% Range: {range}";
        }

        return rewardedWeapon;

    }
}

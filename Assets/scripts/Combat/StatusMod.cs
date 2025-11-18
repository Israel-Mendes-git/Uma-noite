using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;

//enum do stats que vai mudar
public enum StatusModType
{
    ATTACK_MOD,
    DEFFENSE_MOD
}

public class StatusMod : MonoBehaviour
{
    public StatusModType type;
    public float amount;

    public Stats Apply(Stats stats)
    {
        //clona os stats 
        Stats modedStats = stats.Clone();

        switch (this.type)
        {
            //caso o enum seja do tipo ataque 
            case StatusModType.ATTACK_MOD:
                //modifica o ataque
                modedStats.Attack += this.amount; // Usar a propriedade Attack
                break;

            //caso o enum seja do tipo defesa
            case StatusModType.DEFFENSE_MOD:
                //modifica a defesa
                modedStats.Defense += this.amount; // Usar a propriedade Defense
                break;
        }

        return modedStats;
    }

    public void ApplyMod(Stats stats)
    {
        // atualiza o valor do ataque e da defesa após modificação
        float attackValue = stats.Attack;
        float defenseValue = stats.Defense; 
        
    }
}


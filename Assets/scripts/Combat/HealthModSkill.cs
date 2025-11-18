using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;


// Enum para definir os tipos de modifica��o de sa�de
public enum HealthModType
{
    STAT_BASED, // Baseado em estat�sticas
    FIXED,      // Fixo
    PERCENTAGE  // Percentual
}

// Classe que representa uma habilidade que modifica a sa�de
public class HealthModSkill : Skill // Corrigido para herdar de Skill com letra mai�scula
{
    [Header("Health Mod")]
    public float amount; // Quantidade a ser modificada

    public HealthModType modType; // Tipo de modifica��o de sa�de

    [Range(0f, 1f)]
    public float critChance = 0;

    // M�todo que executa a habilidade
    protected override void OnRun()
    {
        float amount = this.GetModification(); // Obt�m a quantidade de modifica��o

        float dice = Random.Range(0f, 1f);

        if (dice <= this.critChance)
        {
            amount *= 2f;
            this.messages.Enqueue("Acerto crítico!");
            SoundEffectManager.Play("Miss");
        }

        this.receiver.ModifyHealth(amount); // Modifica a sa�de do receptor
        
    }

    // M�todo que calcula a modifica��o com base no tipo
    // M�todo que calcula a modifica��o com base no tipo
    public float GetModification()
    {
        switch (this.modType)
        {
            case HealthModType.STAT_BASED:
                Stats emitterStats = this.emitter.GetCurrentStats(); // Estat�sticas do emissor
                Stats receiverStats = this.receiver.GetCurrentStats(); // Estat�sticas do receptor

                // C�lculo de dano bruto
                float rawDamage = (((2 * emitterStats.Level) / 5) + 2) * this.amount * (emitterStats.Attack / receiverStats.Defense);

                return (rawDamage / 50) + 2; // Retorna a modifica��o de sa�de

            case HealthModType.FIXED:
                return this.amount; // Retorna a quantidade fixa

            case HealthModType.PERCENTAGE:
                Stats rStats = this.receiver.GetCurrentStats(); // Estat�sticas do receptor

                return rStats.MaxHealth * this.amount; // Use MaxHealth com "M" mai�sculo
        }

        throw new System.InvalidOperationException("HealthModSkill::GetModification.Unreachable!"); // Exce��o caso o c�digo n�o entre em nenhum caso
    }
}

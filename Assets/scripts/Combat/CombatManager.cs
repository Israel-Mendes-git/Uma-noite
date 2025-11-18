using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Combat;

public enum CombatStatus
{
    WAITING_FOR_FIGHTER,
    FIGHTER_ACTION,
    CHECK_ACTION_MESSAGES,
    CHECK_FOR_VICTORY,
    NEXT_TURN
}

public class CombatManager : MonoBehaviour
{
    //=====VARI�VEIS DO COMBAT MANAGER=====//
    public Fighter[] fighters; // Array de lutadores envolvidos na batalha
    private int fighterIndex; // �ndice do lutador atual que est� fazendo o turno

    private bool isCombatActive; // Indica se a batalha est� ativa

    private CombatStatus combatStatus;
    private HealthModSkill healthMod;
    private Skill currentFighterSkill;
    public StatusPanel statusPanel;
    public Stats stats;
    private Animator anim;


    public string cena;

    void Start()
    {
        anim = GetComponent<Animator>();
        statusPanel = FindObjectOfType<StatusPanel>();
        healthMod = GetComponent<HealthModSkill>();

        // Cria uma nova instância de Stats para o CombatManager, com valores iniciais
        stats = new Stats(1, 100, 10, 5, 3); // Exemplo de valores iniciais: level 1, maxHealth 100, attack 10, defense 5, spirit 3

        foreach (var fighter in this.fighters)
        {
            if (fighter == null)
            {
                Debug.LogError("Um dos objetos Fighter no array fighters está null.");
            }
            else
            {
                fighter.combatManager = this; // Atribui o CombatManager
                fighter.InitializeStatsPanel(fighter.CurrentHealth, fighter.MaxHealth, fighter.Level); // Inicializa o painel de status
            }
        }

        this.combatStatus = CombatStatus.NEXT_TURN;
        this.fighterIndex = -1; // Inicializa o índice do lutador para o primeiro
        this.isCombatActive = true; // Define que o combate está ativo
        StartCoroutine(this.CombatLoop()); // Inicia o loop de combate
    }



    // Coroutine que controla a sequ�ncia de turnos dos lutadores
    IEnumerator CombatLoop()
    {
        while (this.isCombatActive) // Enquanto o combate estiver ativo
        {
            switch (this.combatStatus)
            {
                case CombatStatus.WAITING_FOR_FIGHTER:
                    yield return null;
                    break;

                case CombatStatus.FIGHTER_ACTION:
                    LogPanel.Write($"{this.fighters[this.fighterIndex].idName} usou {currentFighterSkill.skillName}.");
                    

                    yield return null;

                    currentFighterSkill.Run();
                    HealthModSkill healthSkill = currentFighterSkill as HealthModSkill;
                    if (healthSkill != null && healthSkill.modType == HealthModType.STAT_BASED)
                    {
                        Fighter currentFighter = this.fighters[this.fighterIndex];
                        Animator fighterAnim = currentFighter.GetComponent<Animator>();

                        if (fighterAnim != null)
                        {
                            Debug.Log($"Animator encontrado em {currentFighter.idName}, ativando animação!");
                            fighterAnim.SetTrigger("Attack");
                            SoundEffectManager.Play("Slash");
                        }
                        else
                        {
                            Debug.LogError($"O lutador {currentFighter.idName} não tem um Animator!");
                        }
                    }




                    yield return new WaitForSeconds(currentFighterSkill.animationDuration);
                    this.combatStatus = CombatStatus.CHECK_ACTION_MESSAGES;

                    break;

                case CombatStatus.CHECK_ACTION_MESSAGES:
                    string nextMessage = this.currentFighterSkill.GetNextMessage();

                    if(nextMessage != null)
                    {
                        LogPanel.Write(nextMessage);
                        yield return new WaitForSeconds(2f);
                    }
                    else
                    {
                        this.currentFighterSkill = null;
                        this.combatStatus = CombatStatus.CHECK_FOR_VICTORY;
                        yield return null;
                    }
                    break;


                case CombatStatus.CHECK_FOR_VICTORY:
                    foreach (var fgtr in this.fighters)
                    {
                        if (!fgtr.IsAlive) 
                        {
                            this.isCombatActive = false;

                            LogPanel.Write("Vitória!");

                            Debug.Log($"Fighter: {fgtr.idName}, Current Health: {fgtr.CurrentHealth}, Max Health: {fgtr.MaxHealth}");

                            // Carrega a pr�xima cena
                            SceneManager.LoadScene(cena);

                        }
                        else
                        {
                            this.combatStatus = CombatStatus.NEXT_TURN;
                        }
                    }
                    yield return null;
                    break;

                case CombatStatus.NEXT_TURN:
                    yield return new WaitForSeconds(1f);
                    // Atualiza o �ndice para o pr�ximo lutador, voltando ao in�cio se chegar ao final
                    this.fighterIndex = (this.fighterIndex + 1) % this.fighters.Length;
                    Debug.Log(fighterIndex);

                    // Obt�m o lutador atual baseado no �ndice
                    var currentTurn = this.fighters[this.fighterIndex];
                    Debug.Log(currentTurn);

                    // Informa quem � o lutador da vez
                    LogPanel.Write($"{currentTurn.idName} é o seu turno.");
                    currentTurn.InitTurn(); // Chama o m�todo para iniciar o turno do lutador 

                    this.combatStatus = CombatStatus.WAITING_FOR_FIGHTER;

                    break;
            }
            
        }
    }

    public Fighter GetOpposingFighter()
    {
        if(this.fighterIndex == 0)
        {
            return this.fighters[1];
        } 
        else
        {
            return this.fighters[0];
        }
    }

    public void OnFighterSkill(Skill skill)
    {
        this.currentFighterSkill = skill;
        this.combatStatus = CombatStatus.FIGHTER_ACTION;
    }
}

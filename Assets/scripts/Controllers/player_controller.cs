using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//requer o componente do tipo player 
[RequireComponent(typeof(player))]

public class player_controller : MonoBehaviour
{
//===VARIÁVEIS DO PLAYER CONTROLLER===//
    //varíaveis de componentes
    public player player;
    private Rigidbody2D rig;

    //variáveis de movimentação e velocidade
    private float inicialSpeed;
    public float runSpeed;
    public float speed = 5.5f;
    private Vector2 direction;
    private Vector3 moviment;
    [SerializeField]
    public Direction direcaoMovimento;
    private bool playingFootsteps = false;
    public float footstepSpeed = 0.5f;

    //variáveis de posições e layers
    public LayerMask solidObjectsLayer;
    public LayerMask interactablesLayer;

    private float horizontal;
    private bool isPaused;

    [Header("Painel e Menu")]
    public GameObject pausePanel;
    public string cena;
    public GameObject options;



    void Awake()
    {
        
        //recebendo o componente da física 
        rig = GetComponent<Rigidbody2D>();
        //buscando o objeto do Dialogue System
    }

    void Start()
    {
        //recebendo o componente do player
        player = GetComponent<player>();

        //armazena que a direção inicial do movimento é a direita
        this.direcaoMovimento = Direction.Direita;

        //armazena a posição do transform como posicial inicial 

        //a velociadade inicial é a velocidade do script entity
        inicialSpeed = speed;

        Time.timeScale = 1.0f;



    }

    void Update()
    {

        //armazena as direcoes horizontal e vertical
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;            
        
        //chama a função de corrida do player
        PlayerRun();
        if (DialogueManager.Instance != null && DialogueManager.Instance.isDialogueActive)
        {
            horizontal = 0f;
            return;
        }


        horizontal = Input.GetAxisRaw("Horizontal");    

        // se a direção de x for positiva 
        if(direction.x > 0)
        {
            //o player anda para a direita 
            this.direcaoMovimento = Direction.Direita;
        }
        //se a direção de x for negativa 
        else if (direction. x < 0) {
               
            //o player anda para a esquerda
            this.direcaoMovimento = Direction.Esquerda;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
            PauseScreen();
        }

        PlayerPrefs.SetFloat("x", transform.position.x);
        PlayerPrefs.SetFloat("y", transform.position.y);
        PlayerPrefs.SetFloat("z", transform.position.z);

    }

    
    void FixedUpdate()
    {

        rig.MovePosition(rig.position + direction * speed * Time.fixedDeltaTime);
        if (!isPaused)
        {
            if (direction.magnitude > 0.1f && !playingFootsteps)
            {
                StartFootsteps();
            }
            else if (direction.magnitude <= 0.1f && playingFootsteps)
            {
                StopFootsteps();
            }
        }
    }
    void PlayerRun()   
    {
            //se a tecla control esquerdo for pressionado     
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                //a velocidade recebe a velocidade do player correndo
                speed = runSpeed;
            }
            //se parar de pressionar a tecla 
            if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                //a velocidade do player recebe a velocidade inicial
                speed = inicialSpeed;
            }
    }

    void PauseScreen()
    {
        if (isPaused)
        {
            SoundEffectManager.Play("unPause");
            isPaused = false;
            pausePanel.SetActive(false);
            Time.timeScale = 1.0f;
            
        }
        else
        {
            SoundEffectManager.Play("Pause");
            isPaused = true;
            pausePanel.SetActive(true);
            Time.timeScale = 0f;
            

        }

    }

    public void BackToMenu()
    {
        isPaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(cena);
    }

    public void BackToGame()
    {
        PauseScreen();   
    }
    public void OptionsOn()
    {
        options.SetActive(true);
    }
    public void OptionsOff()
    {
        options.SetActive(false);
    }
    void StartFootsteps()
    {
        playingFootsteps = true;
        InvokeRepeating(nameof(PlayFootstep), 0f, footstepSpeed);
        Debug.Log("Start Footsteps");
    }

    void StopFootsteps()
    {
        playingFootsteps = false;
        CancelInvoke(nameof(PlayFootstep));
        Debug.Log("Footsteps parou");
    }

    void PlayFootstep()
    {
        SoundEffectManager.Play("Run");
        Debug.Log("o efeito aconteceu");
    }

}

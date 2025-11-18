using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string cena;
    public Vector2 novaPosicao;
    public Transform player;

    private bool playerDentro = false;

    void Update()
    {
        if (playerDentro && Input.GetKeyDown(KeyCode.E))
        {
            // Salva a posição alvo antes de mudar de cena
            if (TransitionData.Instance != null)
            {
                TransitionData.Instance.playerTargetPosition = novaPosicao;
            }

            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.LoadScene(cena);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Busca o jogador na nova cena e o posiciona
        GameObject jogador = GameObject.FindGameObjectWithTag("Player");
        if (jogador != null && TransitionData.Instance != null)
        {
            jogador.transform.position = TransitionData.Instance.playerTargetPosition;
        }

        // Remove o listener para evitar múltiplas chamadas
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerDentro = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerDentro = false;
        }
    }
}

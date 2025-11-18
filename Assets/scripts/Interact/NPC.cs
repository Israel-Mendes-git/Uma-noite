using UnityEngine;

public class NPC : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Se o jogador entra na área de colisão
        if (other.CompareTag("Player"))
        {
            // Exibe o botão de interação na posição do NPC
            UIManager.Instance.ShowInteractButton(this.transform);
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Se o jogador sai da área de colisão
        if (other.CompareTag("Player"))
        {
            // Esconde o botão de interação
            UIManager.Instance.HideInteractButton();
        }
    }

}

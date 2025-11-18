using UnityEngine;

//script de teste
public class NPC3 : MonoBehaviour
{

    //qunado jogo começar, o botão de interação aparece na posição de quem possui o componente
    public void Start()
    {
        
        Debug.Log("era pra funcionar");
        UIManager.Instance.ShowInteractButton(this.transform);
        
    }
    

}

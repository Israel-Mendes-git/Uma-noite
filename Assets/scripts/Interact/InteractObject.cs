using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractObject : MonoBehaviour
{
    public GameObject interactBtn;
    public string cena;

    // Start is called before the first frame update
    void Awake()
    {
        interactBtn.SetActive(false);

    }

    private bool isPlayerInRange = false;

    //cria e armazena uma tag alvo como sendo Player
    public string TagTarget = "Player";

    //cria uma lista de colisores chamada de detectedObjs
    public List<Collider2D> detectedObjs = new List<Collider2D>();

    public void OnTriggerEnter2D(Collider2D collision)
    {
        //se o game object com a mesma tag que a tag alvo entrar na área de colisão 
        if (collision.gameObject.tag == TagTarget)
        {
            //adiciona o collision a lista de objetos detectado 
            detectedObjs.Add(collision);
            interactBtn.SetActive(true);
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //se o game object com a mesma tag que a tag alvo sair da área de colisão 
        if (collision.gameObject.tag == TagTarget)
        {
            //remove o collision da lista 
            detectedObjs.Remove(collision);
            interactBtn.SetActive(false);
            isPlayerInRange = false;

        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//script para detectar se o player (tag alvo) possui a quantidade mínima de itens para poder passar de fase
//ideia descartada
public class Detecion : MonoBehaviour
{
    public string TagTarget = "Player";
    public List<Collider2D> detectedObjs = new List<Collider2D>();
    public Item item;

    public void Start()
    {
        item = GetComponent<Item>();
    }
    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(TagTarget))
        {
            detectedObjs.Add(collision);
            Debug.Log($"Trigger Enter: {collision.name}");
            if(item.count >= 4)
            {
                SceneManager.LoadScene(DialogueManager.Instance.cena);
            }
        }
        else
        {
            Debug.Log($"Trigger Enter (não alvo): {collision.name}");
        }
    }
}

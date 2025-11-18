using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ideia descartada
public class InteractObject2 : MonoBehaviour
{
    public GameObject interactBtn;
    public GameObject dialogue;

    
    void Awake()
    {   //caixa de diálogo e botçaoo de interação começam desativados
        dialogue.SetActive(false);
        interactBtn.SetActive(false);
    }

    void Update()
    {
        //se a tecla E for pressionada funciona
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("funcionou");
        }
    }
}

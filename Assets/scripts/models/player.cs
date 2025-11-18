using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player : MonoBehaviour
{
    
    //define a tecla para abrir o inventário
    [Header("Player inventory")]
    public KeyCode inventoryKey = KeyCode.I;

    //define o objeto que vai ser ativado, no caso o inventário
    [Header("Player UI Panels")]
    public GameObject inventory;

    private void Update()
    {
        
        //ativa o inventário caso pressione a tecla 
        if (Input.GetKeyUp(inventoryKey))
        {
            inventory.SetActive(!inventory.activeSelf);
        }


    }

    
    
}
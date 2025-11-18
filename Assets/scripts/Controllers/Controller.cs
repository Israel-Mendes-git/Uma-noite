using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

//script para tentar atualizar os componentes do personagem que foi escolhido
//ideia descartada
public class Controller : MonoBehaviour
{
    public player_controller player_controller;
    public GameObject[] playerPrefabs;
    int characterIndex;
    public CinemachineVirtualCamera VCam;

    void Awake()
    {
        player_controller = GetComponent<player_controller>();
        characterIndex = PlayerPrefs.GetInt("SelectedCharacter", 0);
        GameObject player = Instantiate(playerPrefabs[characterIndex], transform.position, Quaternion.identity);
        VCam.m_Follow = player.transform;

    }
    

    
}

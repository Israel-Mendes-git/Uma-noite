using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject options;
    public GameObject controls;

    //carrega uma cena específica
    public void LoadScene(string cena)
    {
        //carrega a cena 
        SoundEffectManager.Play("NewScene");
        SceneManager.LoadScene(cena);        
    }
    public void OptionsOn()
    {
        SoundEffectManager.Play("Pause");
        options.SetActive(true);
    }
    public void OptionsOff()
    {
        SoundEffectManager.Play("unPause");
        options.SetActive(false);
    }

    public void ControlsOn()
    {
        SoundEffectManager.Play("Pause");
        controls.SetActive(true);
    }
    public void ControlsOff()
    {
        SoundEffectManager.Play("unPause");
        controls.SetActive(false);
    }

    //função para sair do jogo
    public void QuitGame()
    {
        //sai da aplicação 
        Application.Quit();
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamTrigger : MonoBehaviour
{
    public GameObject vcam2;


    private void OnTriggerEnter2D (Collider2D other)
    {
        
        switch (other.gameObject.tag)
        {
            case "CamTrigger":
                vcam2.SetActive(true);
                
                transform.localScale = new Vector3(0.5f, 0.5f, 1f);
                break;
        }
    }

    private void OnTriggerExit2D (Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "CamTrigger":
                vcam2.SetActive(false);
                
                transform.localScale = new Vector3(0.08731378f, 0.09994097f, 1f);
                break;
        }
    }
}

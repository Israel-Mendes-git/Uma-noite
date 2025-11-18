using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*script para o objeto permanecer quando passar de cena
 * isso caso ele exista em outra cena e seu index em ambas for igual*/
public class DontDestroy : MonoBehaviour
{
    private static GameObject[] persistentObjects = new GameObject[7];
    public int objectIndex;

    void Awake()
    {
        if (persistentObjects[objectIndex] == null)
        {
            persistentObjects[objectIndex] = gameObject;
            DontDestroyOnLoad(gameObject);
        }
        else if (persistentObjects[objectIndex] != gameObject)
        {
            Destroy(gameObject);
        }
        
    }


    
}

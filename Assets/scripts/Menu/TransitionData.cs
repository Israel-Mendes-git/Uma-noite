using UnityEngine;

public class TransitionData : MonoBehaviour
{
    public static TransitionData Instance;

    public Vector2 playerTargetPosition;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

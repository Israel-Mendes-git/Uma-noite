using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject interactBtnPrefab; // Prefab do botão
    public GameObject interactBtn; // Instância do botão

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("UIManager inicializado.");
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Garante que o prefab seja carregado dos Resources se não estiver atribuído no Inspector
        if (interactBtnPrefab == null)
        {
            interactBtnPrefab = Resources.Load<GameObject>("Prefabs/InteractButtonUI");

            if (interactBtnPrefab == null)
            {
                Debug.LogError("Não foi possível carregar o prefab do botão de interação de Resources/Prefabs/InteractButtonUI");
            }
        }
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        if (interactBtn == null && interactBtnPrefab != null)
        {
            InitializeInteractBtn(interactBtnPrefab);
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (interactBtn == null && interactBtnPrefab != null)
        {
            InitializeInteractBtn(interactBtnPrefab);
        }
    }

    public void InitializeInteractBtn(GameObject prefab, Transform parent = null)
    {
        if (prefab == null)
        {
            Debug.LogError("Prefab do botão de interação não está atribuído!");
            return;
        }

        if (interactBtn == null)
        {
            if (parent == null)
            {
                GameObject canvasObj = GameObject.Find("Canvas");
                if (canvasObj == null)
                {
                    Debug.LogError("Canvas não encontrado na cena!");
                    return;
                }
                parent = canvasObj.transform;
            }

            interactBtn = Instantiate(prefab, parent, false);
            interactBtn.SetActive(false);
        }
    }

    public void ShowInteractButton(Transform npcTransform)
    {
        if (interactBtn == null)
        {
            Debug.LogWarning("Botão de interação não inicializado.");
            return;
        }

        interactBtn.SetActive(true);
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(npcTransform.position + Vector3.up * 2);
        interactBtn.transform.position = screenPosition;
    }

    public void HideInteractButton()
    {
        if (interactBtn != null)
        {
            interactBtn.SetActive(false);
        }
    }
}

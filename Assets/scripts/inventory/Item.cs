using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour
{
    [SerializeField] public string itemName;
    [SerializeField] public int quantity;
    [SerializeField] public Sprite sprite;
    [TextArea][SerializeField] public string itemDescription;

    public int itemId;
    public int amountInStack;
    public InventoryManager inventoryManager;
    public GameObject interactBtn;
    public int count;

    private bool isPlayerInRange = false;

    private void Start()
    {
        // Busca o InventoryManager normalmente
        GameObject canvas = GameObject.Find("Canvas");
        if (canvas != null)
        {
            inventoryManager = canvas.GetComponent<InventoryManager>();
        }

        // Busca o botão que está dentro do Player
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            interactBtn = player.transform.Find("InteractButtonUI")?.gameObject;
            if (interactBtn == null)
            {
                Debug.LogWarning("Botão de interação não encontrado como filho do Player.");
            }
        }
    }


    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            CollectItem();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && interactBtn != null)
        {
            isPlayerInRange = true;
            interactBtn.SetActive(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && interactBtn != null)
        {
            isPlayerInRange = false;
            interactBtn.SetActive(false);
        }
    }

    private void CollectItem()
    {
        if (inventoryManager == null)
        {
            Debug.LogWarning("InventoryManager não foi encontrado.");
            return;
        }

        int leftOverItems = inventoryManager.AddItem(itemName, quantity, sprite, itemDescription);

        count++;

        if (leftOverItems <= 0)
        {
            if (interactBtn != null)
            {
                interactBtn.SetActive(false);
            }
            Destroy(gameObject);
        }
        else
        {
            quantity = leftOverItems;
        }
    }
}

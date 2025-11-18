using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueCharacter
{
    public string name;
    public Sprite icon;
}

[System.Serializable]
public class DialogueLine
{
    public DialogueCharacter character;
    [TextArea(3, 10)]
    public string line;
}

[System.Serializable]
public class Dialogue
{
    public List<DialogueLine> dialogueLines = new List<DialogueLine>();
}

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public bool startOnSceneLoad = false;
    public bool canRepeat = true;

    private bool isPlayerInTrigger = false;
    private bool isDialogueActive = false;

    private void Awake()
    {
        if (UIManager.Instance != null)
        {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/InteractButtonUI");
            UIManager.Instance.InitializeInteractBtn(prefab, GameObject.Find("Canvas").transform);
        }
    }

    private void Start()
    {
        if (startOnSceneLoad)
        {
            TriggerDialogue();
        }
    }

    private void Update()
    {
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.E) && !isDialogueActive && !startOnSceneLoad)
        {
            TriggerDialogue();
        }
    }

    public void TriggerDialogue()
    {
        if (isDialogueActive) return;

        if (DialogueManager.Instance != null)
        {
            DialogueManager.Instance.StartDialogue(dialogue, this);
            isDialogueActive = true;

            if (!canRepeat)
            {
                GetComponent<Collider2D>().enabled = false;
            }
        }
        else
        {
            Debug.LogError("DialogueManager.Instance está nulo!");
        }
    }

    public void OnDialogueEnded()
    {
        isDialogueActive = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !startOnSceneLoad)
        {
            isPlayerInTrigger = true;
            UIManager.Instance.ShowInteractButton(this.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !startOnSceneLoad)
        {
            isPlayerInTrigger = false;
            UIManager.Instance.HideInteractButton();
        }
    }
}

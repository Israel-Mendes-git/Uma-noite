using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public Image characterIcon;
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI dialogueArea;
    public string cena;
    public bool End;

    private Queue<DialogueLine> lines;
    private DialogueTrigger currentTrigger = null;

    public bool isDialogueActive = false;
    public GameObject dialogueBox;
    public float typingSpeed = 0.2f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("DialogueManager inicializado corretamente.");
        }
        else
        {
            Destroy(gameObject);
            Debug.LogWarning("Já existe uma instância do DialogueManager.");
            return;
        }

        lines = new Queue<DialogueLine>();

        if (dialogueBox == null)
        {
            dialogueBox = GameObject.Find("DialogueBox");
        }

        if (dialogueBox == null)
        {
            Debug.LogError("DialogueBox não encontrado na cena.");
        }

        dialogueBox.SetActive(false);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        cena = SceneManager.GetActiveScene().name;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        cena = scene.name;
        dialogueBox.SetActive(false);
        Debug.Log("Cena carregada: " + cena);
    }

    public void StartDialogue(Dialogue dialogue, DialogueTrigger trigger = null)
    {
        Debug.Log("Exibindo o diálogo...");
        isDialogueActive = true;
        currentTrigger = trigger;

        dialogueArea.text = "";
        characterName.text = "";
        characterIcon.sprite = null;

        lines.Clear();
        dialogueBox.SetActive(true);

        if (dialogue.dialogueLines.Count == 0)
        {
            Debug.LogError("Não há linhas de diálogo.");
            EndDialogue();
            return;
        }

        foreach (DialogueLine dialogueLine in dialogue.dialogueLines)
        {
            lines.Enqueue(dialogueLine);
        }

        DisplayNextDialogueLine();

        if (UIManager.Instance != null && UIManager.Instance.interactBtn != null)
        {
            UIManager.Instance.interactBtn.SetActive(false);
        }
    }

    public void DisplayNextDialogueLine()
    {
        if (lines.Count == 0)
        {
            EndDialogue();
            return;
        }

        DialogueLine currentLine = lines.Dequeue();

        characterIcon.sprite = currentLine.character.icon;
        characterName.text = currentLine.character.name;

        StopAllCoroutines();
        StartCoroutine(TypeSentence(currentLine));

        Button nextBtn = GameObject.Find("NextBtn")?.GetComponent<Button>();
        if (nextBtn != null)
        {
            nextBtn.interactable = true;
        }
    }

    IEnumerator TypeSentence(DialogueLine dialogueLine)
    {
        dialogueArea.text = "";
        foreach (char letter in dialogueLine.line.ToCharArray())
        {
            dialogueArea.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void EndDialogue()
    {
        Debug.Log("Fim do diálogo.");
        End = true;
        isDialogueActive = false;
        dialogueArea.text = "";
        dialogueBox.SetActive(false);

        if (currentTrigger != null)
        {
            currentTrigger.OnDialogueEnded();
            currentTrigger = null;
        }
    }

    public void SceneSwap(string novaCena)
    {
        EndDialogue();
        if (isDialogueActive)
        {
            Debug.LogWarning("Tentativa de trocar de cena durante o diálogo!");
            return;
        }
    }

    public void Scene()
    {
        cena = "biblioteca";
        dialogueBox.SetActive(false);
        SceneManager.LoadScene(cena);
    }
}

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : Singleton<DialogManager>
{
    [SerializeField] GameObject dialogPanel;
    [SerializeField] Image npcIcon;
    [SerializeField] TextMeshProUGUI npcName;
    [SerializeField] TextMeshProUGUI npcDialog;
    private bool dialogStarted;
    private PlayerActions playerActions;

    private Queue<int> queue = new Queue<int>();
    private Queue<string> dialogQueue = new Queue<string>();
    public static event Action<InteractionType> OnExtraInteraction;

    public NPCInteraction NPCSelected { get; set; }

    public override void Awake()
    {
        base.Awake();
        playerActions = new PlayerActions();
        queue.Enqueue(1);
        queue.Enqueue(2);
        queue.Enqueue(3);
        int firstItem = queue.Dequeue();
        int nextItem = queue.Peek();
        int count = queue.Count;
    }
    private void Start()
    {
        playerActions.Dialog.Interact.performed += ctx => HandleDialog();
        playerActions.Dialog.Continue.performed += ctx => HandleDialog();
    }
    /*private void ShowDialog()
    {
        if (NPCSelected == null)
            return;
        if (dialogStarted)
            return;

        dialogPanel.SetActive(true);
        LoadDialogFromNPC();

        npcIcon.sprite = NPCSelected.DialogToShow.Icon;
        npcName.text = NPCSelected.DialogToShow.Name;
        npcDialog.text = NPCSelected.DialogToShow.Greeting;
        dialogStarted = true;
    }
    private void ContinueDialog()
    {
        if (NPCSelected == null)
        {
            dialogQueue.Clear();
            return;
        }

        if (dialogQueue.Count <= 0)
        {
            CloseDialogPanel();
            return;
        }

        npcDialog.text = dialogQueue.Dequeue();
    }*/
    private void HandleDialog()
    {
        if (NPCSelected == null)
            return;

        if (!dialogStarted)
        {
            dialogPanel.SetActive(true);
            LoadDialogFromNPC();

            npcIcon.sprite = NPCSelected.DialogToShow.Icon;
            npcName.text = NPCSelected.DialogToShow.Name;
            npcDialog.text = NPCSelected.DialogToShow.Greeting;
            dialogStarted = true;
        }
        else
        {
            if (dialogQueue.Count <= 0)
            {
                CloseDialogPanel();
                if(NPCSelected.DialogToShow.HasInteraction)
                    OnExtraInteraction?.Invoke(NPCSelected.DialogToShow.Type);
                return;
            }
            else
                npcDialog.text = dialogQueue.Dequeue();
        }
    }
    private void OnEnable()
    {
        playerActions.Enable();
    }
    private void OnDisable()
    {
        playerActions.Disable();
    }
    private void LoadDialogFromNPC()
    {
        if (NPCSelected.DialogToShow.Chat.Length <= 0)
            return;

        foreach (string line in NPCSelected.DialogToShow.Chat)
            dialogQueue.Enqueue(line);
    }
    public void CloseDialogPanel()
    {
        dialogPanel.SetActive(false);
        dialogStarted = false;
        dialogQueue.Clear();
    }

}
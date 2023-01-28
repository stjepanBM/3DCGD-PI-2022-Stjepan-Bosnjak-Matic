using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;


    private void Start()
    {
        StartCoroutine(TriggerDialogueCoroutine());
    }

    IEnumerator TriggerDialogueCoroutine()
    {
        yield return new WaitForSeconds(2);
        TriggerDialogue();
    }


    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

}

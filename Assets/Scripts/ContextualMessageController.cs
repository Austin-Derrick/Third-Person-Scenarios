 using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class ContextualMessageController : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private TMP_Text messageText;
    private float fadeOutDuration = 1;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        messageText = GetComponent<TMP_Text>();

        canvasGroup.alpha = 0;

    }

    private IEnumerator ShowMessage(string message, float duration)
    {
        canvasGroup.alpha = 1;

        messageText.text = message;

        yield return new WaitForSeconds(duration);
        // Start fading out
        float elapsedTime = 0;
        float fadeStartTime = Time.time;
        while (elapsedTime < fadeOutDuration)
        {
            elapsedTime = Time.time - fadeStartTime;
            canvasGroup.alpha = 1 - elapsedTime / fadeOutDuration;
            yield return null;
        }
        canvasGroup.alpha = 0;
    }

    private void OnContextualMessageTriggered(string message, float messageDuration)
    {
        StopAllCoroutines();
        StartCoroutine(ShowMessage(message, messageDuration));
    }

    private void OnEnable()
    {
        ContextualMessageTrigger.ContextualMessageTriggered += OnContextualMessageTriggered;
    }

    private void OnDisable()
    {
        ContextualMessageTrigger.ContextualMessageTriggered -= OnContextualMessageTriggered;
    }
}

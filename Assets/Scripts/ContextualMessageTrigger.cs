using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextualMessageTrigger : MonoBehaviour
{
    /* public delegate void ContextualMessageTriggeredAction
    public static event ContextualMessageTriggeredAction ContextualMessageTriggered;
    */

    [SerializeField] private string message = "Default Message";
    [SerializeField] private float messageDuration = 2.0f;

    public static event Action<string, float> ContextualMessageTriggered;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (ContextualMessageTriggered != null)
            {
                ContextualMessageTriggered.Invoke(message, messageDuration);
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class BindingButton : MonoBehaviour
{
    public InputActionReference actionReference;

    public int inputBindingIndex;

    TMP_Text buttonText;

    private InputActionRebindingExtensions.RebindingOperation rebindingOperation;

    private Button button;

    private void OnValidate()
    {
        inputBindingIndex = Mathf.Clamp(inputBindingIndex, 0, actionReference.action.bindings.Count - 1);
    }

    private void Update()
    {
        buttonText.text = actionReference.action.bindings[inputBindingIndex].ToDisplayString();
    }

    void Start()
    {
        button = GetComponentInChildren<Button>();

        buttonText = GetComponentInChildren<TMP_Text>();

        button.onClick.AddListener(StartRebind);
    }

    private void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }

    public void StartRebind()
    {
        actionReference.action.Disable();

        rebindingOperation = actionReference.action.PerformInteractiveRebinding()
                    .WithCancelingThrough("<Keyboard>/escape")
                    .WithTargetBinding(inputBindingIndex)
                    .Start()
                    .OnComplete((x) =>
                    {
                        buttonText.text = actionReference.action.bindings[inputBindingIndex].ToDisplayString();                        
                        PlayerPrefs.SetString("Bindings", actionReference.asset.SaveBindingOverridesAsJson());
                        PlayerPrefs.Save();
                        actionReference.action.Enable();
                        x.Dispose();
                    })
                    .OnCancel((x) =>
                    {
                        FindObjectOfType<EventSystem>().SetSelectedGameObject(null);
                        x.Dispose();
                        actionReference.action.Enable();
                    });

        if (actionReference.action.bindings[inputBindingIndex].isPartOfComposite)
        {
            rebindingOperation.WithExpectedControlType("Keyboard");
        }
    }
}

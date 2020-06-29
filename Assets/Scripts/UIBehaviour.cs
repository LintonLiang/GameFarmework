using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// 1，向UIManager注册Gameobject 2，挂载空间的事件，放在继承UIBase 这样的脚本上 不用查找
/// </summary>
public class UIBehaviour : MonoBehaviour {

    private void Awake()
    {
        UIManager.Instance.RegistGameObject(name, gameObject);
    }

    public void AddInputListener(UnityAction<string> action)
    {
        if (action !=null)
        {
            InputField btn = transform.GetComponent<InputField>();
            btn.onValueChanged.AddListener(action);
        }
    }
    public void RemoveInputListener(UnityAction<string> action)
    {
        if (action != null)
        {
            InputField btn = transform.GetComponent<InputField>();
            btn.onValueChanged.RemoveListener(action);
        }
    }
    public void AddButtonListener(UnityAction action)
    {
        if (action != null)
        {
            Button btn = transform.GetComponent<Button>();
            btn.onClick.AddListener(action);
        }
    }
    public void RemoveButtonListener(UnityAction action)
    {
        if (action != null)
        {
            Button btn = transform.GetComponent<Button>();
            btn.onClick.RemoveListener(action);
        }
    }
    public void AddToggleListener(UnityAction<bool> action)
    {
        if (action != null)
        {
            Toggle btn = transform.GetComponent<Toggle>();
            btn.onValueChanged.AddListener(action);
        }
    }
    public void RemoveTogglenListener(UnityAction<bool> action)
    {
        if (action != null)
        {
            Toggle btn = transform.GetComponent<Toggle>();
            btn.onValueChanged.RemoveListener(action);
        }
    }
    public void AddSliderListener(UnityAction<float> action)
    {
        if (action != null)
        {
            Slider btn = transform.GetComponent<Slider>();
            btn.onValueChanged.AddListener(action);
        }
    }
    public void RemoveSliderListener(UnityAction<float> action)
    {
        if (action != null)
        {
            Slider btn = transform.GetComponent<Slider>();
            btn.onValueChanged.RemoveListener(action);
        }
    }
}

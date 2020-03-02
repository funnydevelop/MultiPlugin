using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
class ComponentTools: MonoBehaviour
{
    protected T findComponent<T>(Transform parent, string path) where T : Component
    {
        if (parent == null)
            return null;

        Transform transform = parent.Find(path);
        if (transform == null)
            return null;

        T component = transform.GetComponent<T>();

        return component;
    }

    protected T addComponent<T>(Transform parent) where T : Component
    {
        return addComponent<T>(parent, "");
    }

    protected T addComponent<T>(GameObject obj) where T : Component
    {
        return addComponent<T>(obj.transform, "");
    }

    protected T addComponent<T>(string path) where T : Component
    {
        return addComponent<T>(this.transform, path);
    }

    protected T addComponent<T>(Transform parent, string path) where T : Component
    {
        if (parent == null)
            return null;

        Transform transform = parent.Find(path);
        if (transform == null)
            return null;

        T component = transform.GetComponent<T>();
        if (component == null)
            component = transform.gameObject.AddComponent<T>();

        return component;
    }


    protected T findComponent<T>(string path) where T : Component
    {
        return findComponent<T>(this.transform, path);
    }

    protected Button findButton(Transform parent, string path, UnityAction action)
    {
        Button button = findComponent<Button>(parent, path);
        if (button != null && action != null)
        {
            button.onClick.AddListener(action);
        }
        return button;
    }


    protected Button findButton(string path, UnityAction action)
    {
        return findButton(this.transform, path, action);
    }
    protected virtual void Start()
    {

    }
    protected virtual void Update()
    {

    }
}


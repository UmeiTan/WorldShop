using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class RequestView
{
    [SerializeField] private IconBase _iconBase;
    [SerializeField] private List<Image> _elements;

    public void UpdateRequestView(List<int> request)
    {
        int pos = 0;

        foreach (var element in request)
        {
            _elements[pos].sprite = _iconBase.Sprites[element];
            _elements[pos++].gameObject.SetActive(true);
        }
        for (; pos < _elements.Count; pos++)
        {
            _elements[pos].gameObject.SetActive(false);
        }
    }
}
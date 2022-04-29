using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ResultView
{
    [SerializeField] private IconBase _icons;
    [SerializeField] private List<Image> _plusElements;
    [SerializeField] private List<Image> _minusElements;

    public void UpdateResultView(List<int> plus, List<int> minus)
    {
        int plusPos = 0;
        int minusPos = 0;

        foreach (var plusElement in plus)
        {
            _plusElements[plusPos].sprite = _icons.Sprites[plusElement];
            _plusElements[plusPos++].gameObject.SetActive(true);
        }
        for (; plusPos < _plusElements.Count; plusPos++)
        {
            _plusElements[plusPos].gameObject.SetActive(false);
        }
        foreach (var minusElements in minus)
        {
            _minusElements[minusPos].sprite = _icons.Sprites[minusElements];
            _minusElements[minusPos++].gameObject.SetActive(true);
        }
        for (; minusPos < _plusElements.Count; minusPos++)
        {
            _minusElements[minusPos].gameObject.SetActive(false);
        }
    }

}
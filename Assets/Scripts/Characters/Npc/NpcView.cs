using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class NpcView
{
    [SerializeField] private Image _image;

    public void View(Npc npc)
    {
        _image.sprite = npc.NpcSprite;
    }

}
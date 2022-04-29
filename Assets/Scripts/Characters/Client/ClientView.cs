using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]
public class ClientView
{
    [SerializeField] private Image _image;

    public void View(Client client)
    {
        _image.sprite = client.ClientSprite;
    }

}
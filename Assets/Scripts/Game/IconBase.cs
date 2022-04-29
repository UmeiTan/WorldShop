using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class IconBase : ScriptableObject
{
    [SerializeField] private List<Sprite> _sprites;
    [SerializeField] private List<Sprite> _sign;

    public List<Sprite> Sprites => _sprites;
    public List<Sprite> Sign => _sign;
}
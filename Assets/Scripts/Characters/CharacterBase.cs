using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public partial class CharacterBase : ScriptableObject
{

    [SerializeField] private List<Client> _clients;
    [SerializeField] private List<Npc> _npc;

    public List<Client> Clients => _clients;
    public List<Npc> Npc => _npc;

}
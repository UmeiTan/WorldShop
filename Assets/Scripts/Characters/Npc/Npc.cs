using UnityEngine;

public abstract class Npc
{
    [SerializeField] private Sprite _npcSprite;

    public Sprite NpcSprite => _npcSprite;
}

public class NpcLandlord : Npc
{

}

public class NpTrader : Npc
{

}

public class NpcSecretTrader : Npc
{

}
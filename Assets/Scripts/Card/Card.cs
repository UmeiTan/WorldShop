using System;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public delegate bool CardUsed(CardData cardData);
    public delegate CardData CardRemoved(Card card);

    public static event CardUsed OnCardUsed;//запускает обработчик результата
    public static event CardRemoved OnCardRemoved;//запрашивает новую карту
    public static event Action OnCardUseEnded;//сообщает что можно выводить результат

    [SerializeField] private bool _activeCard = false;
    [SerializeField] private CardView _cardView;

    private CardData _cardData; 
    private Animator _animator;
    private Button _button;
    private bool _cardPending = false;

    readonly int _hashUsedCard = Animator.StringToHash("UsedCard");
    readonly int _hashFail = Animator.StringToHash("Fail");
    readonly int _hashDiscard = Animator.StringToHash("Discard");
    readonly int _hashNextCard = Animator.StringToHash("NextCard");


    private void Start()
    {
        if (_activeCard)
        {
            _animator = GetComponent<Animator>();
            _button = GetComponent<Button>();
            //NextCard();
        }
    }
    public void SetInactiveCard(CardData cardData)
    {
        if (!_activeCard)
        {
            _cardData = cardData;
            _cardView.UpdateCardView(_cardData);
        }
        else
        {
            Debug.LogError("Попытка изменить значение активной карты");
        }
    }
    public void NextCard()
    {
        _animator.ResetTrigger(_hashUsedCard);
        _cardData = OnCardRemoved?.Invoke(this);
        _cardView.UpdateCardView(_cardData);
        OnCardUseEnded?.Invoke();
        _cardPending = false;
    }
    public void UseCard()
    {
        if (!_cardPending && OnCardUsed != null)
        {
            _cardPending = true;
            if (OnCardUsed.Invoke(_cardData))
            {
                _animator.SetTrigger(_hashUsedCard);
            }
            else
            {
                _animator.SetTrigger(_hashFail);
            }
        }
        else Debug.LogWarning("Обработчик результата не доступен");
    }
    public void Activate(bool anim)
    {
        if(anim) _animator.SetTrigger(_hashNextCard);
        _button.enabled = true;
    }
    public void Deactivate(bool anim)
    {
        if (anim) _animator.SetTrigger(_hashDiscard);
        _button.enabled = false;
    }

}
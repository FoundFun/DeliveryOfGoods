using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalletPresenter : MonoBehaviour
{
    [SerializeField] private int _money;

    public int Money { get; private set; }

    public event Action<int> MoneyChanged;

    public void Init()
    {
        Money = _money;
        MoneyChanged?.Invoke(_money);
    }

    public void AddMoney(int money)
    {
        if (money > 0)
            throw new InvalidOperationException(nameof(money));

        _money += money;
        Money = _money;
        MoneyChanged?.Invoke(_money);
    }

    public void RemoveMoney(int money)
    {
        if (money < 0)
            throw new InvalidOperationException(nameof(money));

        _money -= money;
        Money = _money;
        MoneyChanged?.Invoke(_money);
    }
}

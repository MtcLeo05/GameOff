using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyHolder : MonoBehaviour, IDataPersistence
{

    [SerializeField] private float money;
    
    public void loadData(GameData data)
    {
        money = data.playerData.money;
    }

    public void saveData(ref GameData data)
    {
        data.playerData.money = money;
    }

    public void addMoney(float amount)
    {
        money += amount;
    }

    public bool removeMoney(float amount)
    {
        if (!(money - amount >= 0)) return false;
        
        money -= amount;
        return true;
    }

    public float getMoney()
    {
        return money;
    }
}

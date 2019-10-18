using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSpeed : Bonus
{
    [SerializeField] float _speedModifier =1.5f, _duration =10f;
    internal override void ExecuteBonus()
    {
        FindObjectOfType<SnakeManager>()?.ChangeSpeed(_speedModifier, _duration);
        base.ExecuteBonus();
    }

}

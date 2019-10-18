using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusReverseMovement : Bonus
{
    internal override void ExecuteBonus()
    {
        FindObjectOfType<SnakeManager>()?.ReverseHead();
        base.ExecuteBonus();
    }
}

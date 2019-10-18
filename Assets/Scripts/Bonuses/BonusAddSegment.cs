using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusAddSegment : Bonus
{
    internal override void ExecuteBonus()
    {
        FindObjectOfType<SnakeManager>()?.AddSegment();
        base.ExecuteBonus();
    }
}

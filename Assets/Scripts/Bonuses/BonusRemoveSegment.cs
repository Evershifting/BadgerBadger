using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusRemoveSegment : Bonus
{
    internal override void ExecuteBonus()
    {
        FindObjectOfType<SnakeManager>()?.RemoveSegment();
        base.ExecuteBonus();
    }
}

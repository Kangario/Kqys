using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mech : MonoBehaviour
{
    [SerializeField] private LegData[] legs;

    [SerializeField] private float stepLenght = 0.75f;

    private void Update()
    {
        for (var index = 0; index < legs.Length; index++) 
        { 
            ref var leg = ref legs[index];
            if (!CanMove(index)) continue;
            if (!leg.Leg.IsMoving &&
                !(Vector3.Distance(leg.Leg.CurrentPosition, leg.Raycast.Position) > stepLenght)) continue;
            leg.Leg.MoveTo(leg.Raycast.Position);
        }
    }

    public bool CanMove(int legIndex)
    {
        var legCount = legs.Length; 
        var n1 = legs[(legIndex + legCount - 1)% legCount];
        var n2 = legs[(legIndex + 1) % legCount];
        return !n1.Leg.IsMoving && !n2.Leg.IsMoving;
    }

    [Serializable]
    private struct LegData
    {
        public LegTarget Leg;
        public LegRaycast Raycast;
    }


}

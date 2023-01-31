using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ring data", menuName = "ItemData/RingData", order = 2)]
public class RingData : ItemData
{
    [SerializeField]
    private RingType ring_type_;
    public RingType ring_type { get => ring_type_; }
}

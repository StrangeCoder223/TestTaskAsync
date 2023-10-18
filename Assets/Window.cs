using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour
{
    [SerializeField] private List<Slot> _slots;

    public void ResetWindowContent()
    {
        _slots.ForEach(x => x.ResetAll());
    }
}

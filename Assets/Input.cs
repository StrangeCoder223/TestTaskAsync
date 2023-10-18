using System.Threading;
using UnityEngine;

public class Input : MonoBehaviour
{
    public CancellationTokenSource SpaceCancel { get; private set; } = new();

    private void Update()
    {
        if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
        {
            SpaceCancel.Cancel();
           //SpaceCancel.Dispose();
        }
    }
}
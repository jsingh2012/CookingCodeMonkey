using System;
using UnityEngine;

public class SelectedCounterVisiual : MonoBehaviour
{
    [SerializeField] private ClearCounter clearCounter;
    [SerializeField] private GameObject selectedCounter;
    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += onPlayerSelectedCounterChanges;
    }

    private void onPlayerSelectedCounterChanges(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if (e.selectedCounter == clearCounter)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Hide()
    {
        selectedCounter.SetActive(false);
    }

    private void Show()
    {
        selectedCounter.SetActive(true);
    }
}

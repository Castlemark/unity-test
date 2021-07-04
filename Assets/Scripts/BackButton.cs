using System;
using UnityEngine;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
  public event Action<int> GameSelected;

  void Awake()
  {
    this.GetComponent<Button>().onClick.AddListener(() => { GameSelected?.Invoke(0); });
  }
}

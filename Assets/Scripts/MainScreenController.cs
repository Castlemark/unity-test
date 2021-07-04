using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class MainScreenController : MonoBehaviour
{
  [SerializeField] private TextMeshProUGUI TMPTimestamp;
  [SerializeField] private Button BtnJuego1;
  [SerializeField] private Button BtnJuego2;
  [SerializeField] private Button BtnJuego3;

  public event Action<int> GameSelected;

  void Awake()
  {
    BtnJuego1.onClick.AddListener(() => { GameSelected?.Invoke(1); });
    BtnJuego2.onClick.AddListener(() => { GameSelected?.Invoke(2); });
    BtnJuego3.onClick.AddListener(() => { GameSelected?.Invoke(3);});
  }
}

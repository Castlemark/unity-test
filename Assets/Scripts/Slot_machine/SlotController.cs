using UnityEngine;
using DigitalRuby.Tween;

public class SlotController : MonoBehaviour
{
  private const int MAX_HEIGTH = 1400;
  [SerializeField] private RectTransform RTDrawings;

  public string Result = "";

  void Start()
  {
    RTDrawings.anchoredPosition = new Vector2(0.0f, Random.Range(0, 7) * 200.0f);
  }

  public void SpinSlot(int id)
  {
    var auxVector = Vector2.zero;
    System.Action<ITween<float>> whileSpinSlot = (t) =>
    {
      auxVector.y = t.CurrentValue % 1400;
      RTDrawings.anchoredPosition = auxVector;
    };

    System.Action<ITween<float>> spinSlotCompleted = (t) =>
    {
      switch (t.CurrentValue % 1400)
      {
        case 0:
          Result = "Diamond";
          break;
        case 200:
          Result = "Crown";
          break;
        case 400:
          Result = "Melon";
          break;
        case 600:
          Result = "Bar";
          break;
        case 800:
          Result = "Seven";
          break;
        case 1000:
          Result = "Cherry";
          break;
        case 1200:
          Result = "Lemon";
          break;
        case 1400:
          Result = "Diamond";
          break;
        default:
          Debug.LogError("Error!");
          break;
      }
    };

    Result = "";

    TweenFactory.Tween(
      $"SpinSlot{id}",
      RTDrawings.anchoredPosition.y,
      Random.Range(14, 22) * 200.0f,
      3.0f,
      TweenScaleFunctions.CubicEaseOut,
      whileSpinSlot,
      spinSlotCompleted);
  }
}

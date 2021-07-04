using UnityEngine;
using DigitalRuby.Tween;

public class ScreenMover : MonoBehaviour
{
  [SerializeField] private MainScreenController mainScreenController;
  [SerializeField] private GameObject GOGameScreen1;
  [SerializeField] private GameObject GOGameScreen2;
  [SerializeField] private GameObject GOGameScreen3;

  private RectTransform RTScreenContainer;

  void Awake()
  {
    RTScreenContainer = this.GetComponent<RectTransform>();

    mainScreenController.GameSelected += (gameScreenIdx) => { onGameScreenSelected(gameScreenIdx); };
    GOGameScreen1.GetComponentInChildren<BackButton>().GameSelected += (gameScreenIdx) => { onGameScreenSelected(gameScreenIdx); };
    GOGameScreen2.GetComponentInChildren<BackButton>().GameSelected += (gameScreenIdx) => { onGameScreenSelected(gameScreenIdx); };
    GOGameScreen3.GetComponentInChildren<BackButton>().GameSelected += (gameScreenIdx) => { onGameScreenSelected(gameScreenIdx); };
  }

  private void onGameScreenSelected(int gameScreenIdx)
  {
    System.Action<ITween<Vector2>> moveToScreen = (t) =>
      {
        var anchorMax = RTScreenContainer.anchorMax;
        var anchorMin = RTScreenContainer.anchorMin;
        anchorMax.x = t.CurrentValue.y;
        anchorMin.x = t.CurrentValue.x;

        RTScreenContainer.anchorMax = anchorMax;
        RTScreenContainer.anchorMin = anchorMin;
      };

    System.Action<ITween<Vector2>> moveToScreenCompleted = (t) =>
    {
      if (t.CurrentValue == Vector2.up)
      {
        GOGameScreen1.SetActive(false);
        GOGameScreen2.SetActive(false);
        GOGameScreen3.SetActive(false);
      }
      else
      {
        mainScreenController.gameObject.SetActive(false);
      }
    };

    Vector2 curAnchor = new Vector2(RTScreenContainer.anchorMin.x, RTScreenContainer.anchorMax.x);
    Vector2 targetAnchor = Vector2.up;
    switch (gameScreenIdx)
    {
      case 0:
        mainScreenController.gameObject.SetActive(true);
        break;
      case 1:
        GOGameScreen1.SetActive(true);
        targetAnchor = Vector2.left;
        break;
      case 2:
        GOGameScreen2.SetActive(true);
        targetAnchor = Vector2.left;
        break;
      case 3:
        GOGameScreen3.SetActive(true);
        targetAnchor = Vector2.left;
        break;
    }

    TweenFactory.Tween(
      "MoveToScreen",
      curAnchor,
      targetAnchor,
      0.75f,
      TweenScaleFunctions.CubicEaseInOut,
      moveToScreen,
      moveToScreenCompleted
      );
  }
}

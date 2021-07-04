using UnityEngine;
using UnityEngine.SceneManagement;
using DigitalRuby.Tween;

public class SplashScreenController : MonoBehaviour
{
  [SerializeField] private RectTransform RTProgressBarFill;

  void Start()
  {
    // This will update the progressbar with the values determined by the Tween
    System.Action<ITween<Vector2>> fillBar = (t) =>
    {
      RTProgressBarFill.anchorMax = t.CurrentValue;
    };

    // This will run once the tween has completed
    System.Action<ITween<Vector2>> onFillBarCompleted = (t) =>
    {
      // When the tween has been completed, we load the main game scene
      SceneManager.LoadSceneAsync("MainScene");
    };

    //We use a tween to simulate the loading of the game
    TweenFactory.Tween(
      "FillProgressBar",
      new Vector2(0.0f, 1.0f),
      new Vector2(1.0f, 1.0f),
      5.0f,
      TweenScaleFunctions.Linear, 
      fillBar,
      onFillBarCompleted
    );
  }
}

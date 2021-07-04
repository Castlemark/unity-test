using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Networking;
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

  void Start()
  {
    StartCoroutine(getTimestamp());
  }

  // Send web request, then receive, parse and format response to show it in text
  private IEnumerator getTimestamp()
  {
    var request = UnityWebRequest.Get("http://worldtimeapi.org/api/ip");
    yield return request.SendWebRequest();

    if (request.isHttpError || request.isNetworkError)
    {
      Debug.LogError(request.error);
    }
    else
    {
      string textResponse = request.downloadHandler.text;
      var timestampResponse = JsonUtility.FromJson<TimestampResponse>(textResponse);
      TMPTimestamp.text = timestampResponse.datetime.Replace('T', ' ').Replace('-', '/').Split('.')[0];
    }

  }
}

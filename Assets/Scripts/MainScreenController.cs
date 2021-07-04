using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Networking;

public class MainScreenController : MonoBehaviour
{
  [SerializeField] private TextMeshProUGUI TMPTimestamp;
  [SerializeField] private Button BtnJuego1;
  [SerializeField] private Button BtnJuego2;
  [SerializeField] private Button BtnJuego3;

  void Awake()
  {
    BtnJuego1.onClick.AddListener(() => { });
    BtnJuego2.onClick.AddListener(() => { });
    BtnJuego3.onClick.AddListener(() => { });
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

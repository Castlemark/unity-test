using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class TimestampText : MonoBehaviour
{
  private TextMeshProUGUI TMPTimestamp;

  void Awake()
  {
    TMPTimestamp = this.gameObject.GetComponent<TextMeshProUGUI>();
  }

  void Start()
  {
    StartCoroutine(getTimestamp());
  }

  void OnEnable()
  {
    StartCoroutine(getTimestamp());
  }

  // Send web request, then receive, parse and format response to show it in text
  private IEnumerator getTimestamp()
  {
    var request = UnityWebRequest.Get("https://www.worldtimeapi.org/api/ip");
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

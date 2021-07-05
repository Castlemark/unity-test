using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlotMachine : MonoBehaviour
{
  [SerializeField] private SlotController Slot1Controller;
  [SerializeField] private SlotController Slot2Controller;
  [SerializeField] private SlotController Slot3Controller;
  [SerializeField] private Button SpinButton;
  [SerializeField] private TextMeshProUGUI TMPResult;

  [SerializeField] private AudioClip AudioWin;
  [SerializeField] private AudioClip AudioLose;

  void Awake()
  {
    SpinButton.onClick.AddListener(() =>
    {
      SpinButton.GetComponent<AudioSource>().Play();
      StartCoroutine(playSlotMachine());
    });
  }

  void OnEnable()
  {
    TMPResult.gameObject.SetActive(false);
  }

  private IEnumerator playSlotMachine()
  {
    TMPResult.gameObject.SetActive(false);
    SpinButton.interactable = false;

    Slot1Controller.SpinSlot(1);
    yield return new WaitForSeconds(0.5f);
    Slot2Controller.SpinSlot(2);
    yield return new WaitForSeconds(0.5f);
    Slot3Controller.SpinSlot(3);
    yield return new WaitForSeconds(3.1f);

    if (Slot1Controller.Result == Slot2Controller.Result && Slot2Controller.Result == Slot3Controller.Result)
    {
      TMPResult.text = "Has Ganado!";
      this.GetComponent<AudioSource>().PlayOneShot(AudioWin);
    }
    else
    {
      TMPResult.text = "Has Perdido!";
      this.GetComponent<AudioSource>().PlayOneShot(AudioLose);
    }

    TMPResult.gameObject.SetActive(true);
    SpinButton.interactable = true;
  }
}

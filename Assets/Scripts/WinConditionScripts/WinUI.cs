using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinUI : MonoBehaviour
{
    [SerializeField] private TMP_Text progressText;
    [SerializeField] private Image holdFillImage;

    public void SetProgress(int completed, int total)
    {
        if (progressText != null)
            progressText.text = "Locuri activate: " + completed + " / " + total;
    }

    public void SetHoldProgress(float t01)
    {
        if (holdFillImage == null) return;

        t01 = Mathf.Clamp01(t01);
        holdFillImage.fillAmount = t01;
    }

    public void ShowHoldBar(bool show)
    {
        if (holdFillImage != null)
            holdFillImage.transform.parent.gameObject.SetActive(show);
    }

}

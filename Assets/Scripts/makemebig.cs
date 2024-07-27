using AirFishLab.ScrollingList;
using UnityEngine;
using UnityEngine.UI;

public class makemebig : MonoBehaviour
{
    public void OnFocusingBoxChanged(ListBox prevFocusingBox, ListBox curFocusingBox)
    {
        curFocusingBox.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(160f, 160f);
        curFocusingBox.gameObject.GetComponent<Button>().onClick.Invoke();
        if (prevFocusingBox != null)
        {
            prevFocusingBox.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(90.8f, 93.9f);
        }
    }
}

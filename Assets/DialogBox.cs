using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogBox : MonoBehaviour
{

    private Vector3 off;
    private Vector3 on;
    private RectTransform _rectTransform;
    private TMP_Text _text;

    private void Awake()
    {
        off = new Vector3(0, 10000, 0);
        on = new Vector3(0, 0, 0);
        _rectTransform = GetComponent<RectTransform>();
        _rectTransform.anchoredPosition = off;
        _text = gameObject.GetComponentInChildren<TMP_Text>();

    }

    public void showDialog()
    {
        _rectTransform.anchoredPosition = on;
    }

    public void hideDialog()
    {
        _rectTransform.anchoredPosition = off;
    }

    public void setText(string s)
    {
        _text.text = s;
    }
}

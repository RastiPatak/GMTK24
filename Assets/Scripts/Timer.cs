using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private TextMeshProUGUI _textField;
    private float timeToDisplay = 0f;

    // Start is called before the first frame update
    void Start()
    {
        _textField = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        timeToDisplay += Time.deltaTime;

        TimeSpan timeSpan = TimeSpan.FromSeconds(timeToDisplay);
        _textField.text = timeSpan.ToString(@"mm\:ss\:ff");
    }
}

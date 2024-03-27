using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;

public class ContextMenuItem : MonoBehaviour
{
    [SerializeField] private Text _title;
    [SerializeField] private Button _buildButton;

    public string Text
    {
        get => _title.text;
        set => _title.text = value;
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.AssemblyQualifiedNameParser;
using UnityEngine;
using UnityEngine.UIElements;

public class TopMenu : MonoBehaviour
{
    private Label wave;
    private Label credits;
    private Label gatehealth;
    private Button play;
    private VisualElement root;
    // Start is called before the first frame update
    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        wave = root.Q<Label>("Wave");
        credits = root.Q<Label>("Credits");
        gatehealth = root.Q<Label>("GateHealth");
        play = root.Q<Button>("ArrowRight");
        if (play != null)
        {
            play.clicked += OnPlayButtonClicked;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetGateHealthLabel(string text)
    {
        gatehealth.text = text;
    }
    public void SetCreditsLabel(string text)
    {
        credits.text = text;
    }
    public void SetWaveLabel(string text)
    {
        wave.text = text;
    }
    private void OnPlayButtonClicked()
    {
        
    }
    private void OnDestroy()
    {
        if (play != null)
        {
            play.clicked -= OnPlayButtonClicked;
        }
    }
}

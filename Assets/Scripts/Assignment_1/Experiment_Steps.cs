using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Experiment_Steps : MonoBehaviour
{
    public static Experiment_Steps expSteps;
    public TMP_Text _stepsText;

    private void Awake()
    {
        expSteps = this;
    }
    private void Start()
    {
        StartCoroutine(TextDisplay());
    }

    private IEnumerator TextDisplay()
    {
        _stepsText.text = "Welcome to our Chemistry Lab!";
        yield return new WaitForSeconds(2);
        _stepsText.text = "Let's do an experiment!";
        yield return new WaitForSeconds(2);
        _stepsText.text = "Select Flask A!";
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selecting_Conical_Flask : MonoBehaviour
{
    public static Selecting_Conical_Flask instance;
    [SerializeField] private GameObject flaskPositionPoint;
    [SerializeField] private float _speed;
    public Animator _flaskAnimator;
    public GameObject _shakeButton;
    public Renderer _flaskRenderer;
    private bool _isMoving = false;
    private void Awake()
    {
        instance = this;
        _shakeButton.SetActive(false);
    }
    private IEnumerator MoveFlask()
    {
        _isMoving = true;
        while (_isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, flaskPositionPoint.transform.position, _speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, flaskPositionPoint.transform.position) < 0.001f)
            {
                _isMoving = false;
                yield return new WaitForSeconds(1);
                Experiment_Steps.expSteps._stepsText.text = "Select the filled Testube";
            }
            yield return new WaitForSeconds(0.001f);
        }
    }
    private void OnMouseDown()
    {
        if (_isMoving) return; 
        StartCoroutine("MoveFlask");
    }
}
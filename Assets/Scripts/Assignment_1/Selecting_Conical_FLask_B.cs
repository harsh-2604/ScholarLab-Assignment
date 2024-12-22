using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selecting_Conical_FLask_B : MonoBehaviour
{
    #region Public Components
    public static Selecting_Conical_FLask_B Instance;
    public Animator _BFlaskAnimator;
    #endregion

    #region SerializedField Components
    [SerializeField] private GameObject flaskA;
    [SerializeField] private GameObject flaskB;
    [SerializeField] private Transform flaskATargetPosition;
    [SerializeField] private Transform flaskBTargetPosition;
    [SerializeField] private float speed = 1f;
    [SerializeField] private Animator _testTubeAnimator;
    [SerializeField] private Renderer _testTubeMaterial;
    [SerializeField] private Renderer _bFlaskRenderer;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private Animator _characterAnimator;
    #endregion

    #region Private Components
    private bool isMoving = false;
    #endregion
    private void Awake()
    {
        Instance = this;
        _BFlaskAnimator.enabled = false;
        
    }
    private void OnMouseDown()
    {
        if (!isMoving)
        {
            StartCoroutine(MoveFlask());
            Selecting_FIlled_TestTube.instance._isTestTube = true;
        }
    }

    private IEnumerator MoveFlask()
    {
        isMoving = true;
        yield return StartCoroutine(MoveObject(flaskA, flaskATargetPosition.position));
        yield return StartCoroutine(MoveObject(flaskB, flaskBTargetPosition.position));
        isMoving = false;
        yield return new WaitForSeconds(1.5f);
        Experiment_Steps.expSteps._stepsText.text = "Select the half filled TestTube";
        
    }

    private IEnumerator MoveObject(GameObject flask, Vector3 targetPosition)
    {
        while (Vector3.Distance(flask.transform.position, targetPosition) > 0.01f)
        {
            flask.transform.position = Vector3.MoveTowards(flask.transform.position, targetPosition, speed * Time.deltaTime);
            yield return null;
        }
        flask.transform.position = targetPosition;
    }

    public IEnumerator TestTubeAnimation()
    {
        yield return new WaitForSeconds(1);
        _testTubeAnimator.SetBool("isPositionChanged", true);
        yield return new WaitForSeconds(1.5f);
        _testTubeMaterial.material.SetFloat("_FillAmount", 0.8f);
        foreach (Material mat in _bFlaskRenderer.materials)
        {
            mat.SetFloat("_FillAmount", 0.23f);
        }
        yield return new WaitForSeconds(2);
        Selecting_Conical_Flask.instance._shakeButton.SetActive(true);
        Experiment_Steps.expSteps._stepsText.text = "Click On Shake!";
    }

    public IEnumerator ShakeAnimation()
    {
        yield return new WaitForSeconds(1);
        _BFlaskAnimator.SetBool("isShaking", true);
        yield return new WaitForSeconds(1.75f);
        yield return new WaitForSeconds(1);
        _BFlaskAnimator.SetBool("isShaking", false);
        _characterAnimator.SetBool("isIrritated", true);
        yield return new WaitForSeconds(3.3f);
        Experiment_Steps.expSteps._stepsText.text = "Eww that's a pretty bad smell!";
        _audioSource.PlayOneShot(_audioClip);
        yield return new WaitForSeconds(3.3f);
        _characterAnimator.SetBool("isIrritated", false);

    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selecting_FIlled_TestTube : MonoBehaviour
{
    #region Public Components
    public static Selecting_FIlled_TestTube instance;
    public bool _isTestTube = false;
    #endregion

    #region SerializedField Components
    [SerializeField] private Animator _animator;
    [SerializeField] private Renderer _testTubeMaterial;
    [SerializeField] private GameObject _pourButton;
    [SerializeField] private Animator _characterAnimator;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClip;
    #endregion

    private void Awake()
    {
        instance = this;
        _pourButton.SetActive(false);
        Selecting_Conical_Flask.instance._flaskAnimator.enabled = false;
    }
    private void OnMouseDown()
    {
        _pourButton.SetActive(true);
        Experiment_Steps.expSteps._stepsText.text = "Click on Pour!";
    }

    private IEnumerator AnimationPlay()
    {
        yield return new WaitForSeconds(1);
        _animator.SetBool("isPositionChanged", true);
        yield return new WaitForSeconds(1.5f);
        _testTubeMaterial.material.SetFloat("_FillAmount", 0.55f);
        foreach (Material mat in Selecting_Conical_Flask.instance._flaskRenderer.materials)
        {
            mat.SetFloat("_FillAmount", 0.23f);
        }
        yield return new WaitForSeconds(2);
        Selecting_Conical_Flask.instance._flaskAnimator.enabled = true;
        Selecting_Conical_Flask.instance._shakeButton.SetActive(true);

        Experiment_Steps.expSteps._stepsText.text = "Click on Shake!";
        _animator.SetBool("isPositionChanged", false);
    }

    private IEnumerator ShakeAnimation()
    {
        yield return new WaitForSeconds(1);
        Selecting_Conical_Flask.instance._flaskAnimator.SetBool("isShaking", true);
        yield return new WaitForSeconds(1.75f);
        foreach (Material mat in Selecting_Conical_Flask.instance._flaskRenderer.materials)
        {
            mat.SetColor("_Tint", new Color32(128, 0, 128, 255));
        }
        yield return new WaitForSeconds(1);
        Selecting_Conical_Flask.instance._flaskAnimator.SetBool("isShaking", false);
        _characterAnimator.SetBool("isExcited", true);
        yield return new WaitForSeconds(3.3f);
        _audioSource.PlayOneShot(_audioClip);
        Experiment_Steps.expSteps._stepsText.text = "Congratulations the color of the liquid changed!";
        yield return new WaitForSeconds(3.3f);
        _characterAnimator.SetBool("isExcited", false);
        Experiment_Steps.expSteps._stepsText.text = "Now Select Flask B!";
        Selecting_Conical_Flask.instance._flaskAnimator.enabled = false;
        
    }
    public void PlayAnimation()
    {
        if(!_isTestTube)
        {
            _pourButton.SetActive(false);
            StartCoroutine("AnimationPlay");
        }
        else
        {
            Selecting_Conical_FLask_B.Instance._BFlaskAnimator.enabled = true;
            _pourButton.SetActive(false);
            StartCoroutine(Selecting_Conical_FLask_B.Instance.TestTubeAnimation());
        }
    }

    public void Shake()
    {
        if(!_isTestTube)
        {
            Selecting_Conical_Flask.instance._shakeButton.SetActive(false);
            StartCoroutine("ShakeAnimation");
        }
        else
        {
            Selecting_Conical_Flask.instance._shakeButton.SetActive(false);
            StartCoroutine(Selecting_Conical_FLask_B.Instance.ShakeAnimation());
        }
    }
}
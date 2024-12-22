using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    #region Public Components
    public Animal_Cards _aimalCards;
    public GameObject _descriptionPanel;
    public TMP_Text _descriptionText;
    #endregion

    #region Private Components
    private Vector3 _startPosition;
    private Transform _bucket;
    #endregion

    private void Awake()
    {
        _descriptionPanel.SetActive(false);
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        _startPosition = transform.position;
        _bucket = transform.parent;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }


    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        GameObject target = eventData.pointerEnter;
        if(target != null && target.CompareTag("Bucket"))
        {
            FindObjectOfType<Quiz_Manager>().CheckAnimal(_aimalCards, target);
            Destroy(gameObject);
        }
        else
        {
            transform.position = _startPosition;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _descriptionPanel.SetActive(true);
        _descriptionText.text = _aimalCards._description;           
    }
}

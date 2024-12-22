using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using TMPro;
using UnityEngine;


public class Quiz_Manager : MonoBehaviour
{
    public List<Animal_Cards> _cards;
    public GameObject _redBucket, _blueBucket;
    public GameObject _finishScreenPanel;
    public TMP_Text _displayQuizText;
    public TMP_Text _redBucketText;
    public TMP_Text _blueBucketText;
    public TMP_Text _ScoreText;
    public TMP_Text _winLoseText;
    public TMP_Text _incorrectAnimalsText;
    public TMP_Text _CategoryText;
    public enum _quizCategory { Flying, Insect, Eating, Lives, Reproduction }

    private List<string> _incorrectAnimals = new List<string>();
    private int _score = 0;
    private int _correctCards = 0;
    private int _remaininngCards;
    private _quizCategory currentCategory;

    private void Awake()
    {
        _finishScreenPanel.SetActive(false);
    }
    public void Start()
    {
        _remaininngCards = _cards.Count;
        StartQuiz();
        _finishScreenPanel.SetActive(false);
    }

    public void StartQuiz()
    {
        currentCategory = (_quizCategory)Random.Range(0, 5);
        CategoryUI(); ;
    }

    private void CategoryUI()
    {
        switch(currentCategory)
        {
            case _quizCategory.Flying:
                _displayQuizText.text = "Sort animals into Flying or Non-Flying!";
                _redBucketText.text = "Flying";
                _blueBucketText.text = "Non-Flying";
                break;
            case _quizCategory.Insect:
                _displayQuizText.text = "Sort animals into Insect or Non-Insect!";
                _redBucketText.text = "Insect";
                _blueBucketText.text = "Non-Insect ";
                break;
            case _quizCategory.Eating:
                _displayQuizText.text = "Sort animals into Omnivorous or Herbivorous!";              
                _redBucketText.text = "Omnivorous";
                _blueBucketText.text = "Herbivorous";
                break;
            case _quizCategory.Lives:
                _displayQuizText.text = "Sort animals into Lives in Group or Solo!";
                _redBucketText.text = "Lives In Group";
                _blueBucketText.text = "Solo";
                break;
            case _quizCategory.Reproduction:
                _displayQuizText.text = "Sort animals into Lay Eggs or Give Birth!";
                _redBucketText.text = "Lay Eggs";
                _blueBucketText.text = "Give Birth";
                break;
        }
    }

    public void CheckAnimal(Animal_Cards _animal_Cards, GameObject _selectedBucket)
    {
        bool isCorrect = false;
        switch(currentCategory)
        {
            case _quizCategory.Flying:
                isCorrect = (_selectedBucket) == _redBucket && _animal_Cards.movementType == Animal_Cards._movementType.Flying || (_selectedBucket) == _blueBucket && _animal_Cards.movementType == Animal_Cards._movementType.NonFlying;
                break;
            case _quizCategory.Insect:
                isCorrect = (_selectedBucket) == _redBucket && _animal_Cards.animalType == Animal_Cards._animalType.Insect || (_selectedBucket) == _blueBucket && _animal_Cards.animalType == Animal_Cards._animalType.NonInsect;
                break;
            case _quizCategory.Eating:
                isCorrect = (_selectedBucket) == _redBucket && _animal_Cards.eatingType == Animal_Cards._eatingType.Omnivorous || (_selectedBucket) == _blueBucket && _animal_Cards.eatingType == Animal_Cards._eatingType.Herbivorous;
                break;
            case _quizCategory.Lives:
                isCorrect = (_selectedBucket) == _redBucket && _animal_Cards.livingType == Animal_Cards._livingType.LivesInGroup || (_selectedBucket) == _blueBucket && _animal_Cards.livingType == Animal_Cards._livingType.Solo;
                break;
            case _quizCategory.Reproduction:
                isCorrect = (_selectedBucket) == _redBucket && _animal_Cards.birthType == Animal_Cards._birthType.LaysEggs || (_selectedBucket) == _blueBucket && _animal_Cards.birthType == Animal_Cards._birthType.GiveBirth;
                break;
        }

        if (isCorrect)
        {
            _score++;
            _correctCards++;
        }
        else
        {
            _incorrectAnimals.Add(_animal_Cards._animalName);
        }
        ScoreUI();
        _remaininngCards--;
        if (_remaininngCards <= 0) EndGame();
    }

    private void EndGame()
    {
        _finishScreenPanel.SetActive(true);
        _CategoryText.text = "Category: " + " " + currentCategory.ToString();
        if(_incorrectAnimals.Count > 0)
        {
            _incorrectAnimalsText.text = "Incorrect Animals:\n" + string.Join("\n", _incorrectAnimals);
        }
        else
        {
            _incorrectAnimalsText.text = "All animals are correct!";
        }
        if(_score >= 12)
        {
            _winLoseText.text = "Great Job!";
        }
        else
        {
            _winLoseText.text = "Better Luck Next Time!";
        }
    }

    private void ScoreUI()
    {
        _ScoreText.text = "Score: " + _score + "\n " + "Correct Cards: " + _correctCards; ;
    }
}
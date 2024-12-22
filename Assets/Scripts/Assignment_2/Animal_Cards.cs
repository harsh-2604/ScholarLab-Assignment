using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Animal Cards")]
public class Animal_Cards : ScriptableObject
{
    public string _animalName;
    public Sprite _animalSprite;
    public _movementType movementType;
    public _animalType animalType;
    public _eatingType eatingType;
    public _livingType livingType;
    public _birthType birthType;
    public string _description;
    public enum _movementType
    {
        Flying,
        NonFlying
    };
    public enum _animalType
    {
        Insect,
        NonInsect
    };
    public enum _eatingType
    {
        Herbivorous,
        Omnivorous
    };
    public enum _livingType
    {
        LivesInGroup,
        Solo
    }
    public enum _birthType
    {
        LaysEggs,
        GiveBirth
    };
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private Vector2 startingPosition;
    private Vector2 lengthOfSprite;
    public Vector2 amountOfParallax;
    public GameObject mainCamera;

    private void Start()
    {
        mainCamera = GameObject.Find("Main Camera");
        startingPosition = transform.position;
        lengthOfSprite = GetComponent<SpriteRenderer>().bounds.size;
    }

    private void Update()
    {
        Vector3 cameraPosition = mainCamera.transform.position;
        Vector2 temp = new Vector2(cameraPosition.x * (1 - amountOfParallax.x), cameraPosition.y * (1 - amountOfParallax.y));
        Vector2 distance = new Vector2(cameraPosition.x * amountOfParallax.x, cameraPosition.y * amountOfParallax.y);

        Vector3 newPosition = new Vector3(startingPosition.x + distance.x, startingPosition.y + distance.y, transform.position.z);

        transform.position = newPosition;

        if (temp.x > startingPosition.x + (lengthOfSprite.x / 2))
        {
            startingPosition.x += lengthOfSprite.x;
        }
        else if (temp.x < startingPosition.x - (lengthOfSprite.x / 2))
        {
            startingPosition.x -= lengthOfSprite.x;
        }

        // For Endless background in y position

        //if (temp.y > _startingPos.y + (_lengthOfSprite.y / 2))
        //{
        //    _startingPos.y += _lengthOfSprite.y;
        //}
        //else if (temp.y < _startingPos.y - (_lengthOfSprite.y / 2))
        //{
        //    _startingPos.y -= _lengthOfSprite.y;
        //}
    }
}

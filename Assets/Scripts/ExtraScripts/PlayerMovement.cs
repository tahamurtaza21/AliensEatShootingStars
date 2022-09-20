using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject pizza;
    Vector3 offset;

    SpringJoint2D cheeseString;

    Vector2 screenBounds;

    void Start()
    {
        initBound();
        cheeseString = gameObject.GetComponent<SpringJoint2D>();
    }

    void Update()
    {
        CheckPizza();
    }

    void CheckPizza()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        SelectPizza(mousePosition);
        DragPizzaAround(mousePosition);
        UnSelectPizza();
    }

    private void UnSelectPizza()
    {
        if (Input.GetMouseButtonUp(0) && pizza) //Remove Hand from Pizza
        {
            pizza = null;
        }
    }

    private void DragPizzaAround(Vector3 mousePosition)
    {
        if (pizza) //Pizza Selected
        {
            Vector2 newPos = new Vector2();

            //newPos.x = Mathf.Clamp(mousePosition.x + offset.x, minBounds.x, maxBounds.x);
            //newPos.y = Mathf.Clamp(mousePosition.y + offset.y, minBounds.y, maxBounds.y);

            newPos.x = Mathf.Clamp(mousePosition.x + offset.x, -(screenBounds.x), screenBounds.x);
            newPos.y = Mathf.Clamp(mousePosition.y + offset.y, -(screenBounds.y), screenBounds.y);

            //Debug.Log(mousePosition + offset);
            //Debug.Log(screenBounds);

            pizza.transform.position = newPos;
            //cheeseString.anchor = newPos;

        }
    }

    private void SelectPizza(Vector3 mousePosition)
    {
        if (Input.GetMouseButtonDown(0))
        {
            Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);

            if (targetObject)
            {
                pizza = targetObject.transform.gameObject;
                offset = pizza.transform.position - mousePosition;
            }
        }
    }

    void initBound()
    {
        Camera mainCamera = Camera.main;
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
    }
}
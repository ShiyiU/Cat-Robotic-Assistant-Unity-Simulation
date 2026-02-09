using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private GameObject foodButton;
    [SerializeField] private GameObject food; 
    [SerializeField] private GameObject housePlants;
    [SerializeField] private GameObject dishware; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleFood()
    {
        food.SetActive(!food.activeSelf);
    }

    public void ToggleDishware()
    {
        dishware.SetActive(!dishware.activeSelf);
        foodButton.SetActive(!foodButton.activeSelf);
    }

    public void TogglePlants()
    {
        housePlants.SetActive(!housePlants.activeSelf);
    }
}

using UnityEngine;
using UnityEngine.UI; 

public class RoomGUIManager : MonoBehaviour
{
    [SerializeField] private Image[] buttons; 

    void Awake()
    {
    }

    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ToggleButton(int id)
    {
        if (buttons[id].color == Color.white)
        {
            buttons[id].color = Color.green;
        } else
        {
            buttons[id].color = Color.white; 
        }
    }
}

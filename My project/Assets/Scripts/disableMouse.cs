using UnityEngine;

public class disableMouse : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.visible = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnExit()
    {
        Application.Quit();
    }
}

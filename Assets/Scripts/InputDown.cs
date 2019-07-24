using UnityEngine;

public class InputDown
{
    string inputName;
    bool isTriggered = false;

    public InputDown(string name)
    {
        this.inputName = name;
    }

    public float GetInputDown()
    {
        float val = Input.GetAxisRaw(inputName);
        if(!isTriggered && val != 0f)
        {
            isTriggered = true;
            return val;
        }
        return 0f;
    }

    public void Update()
    {
        if (isTriggered && Input.GetAxisRaw(inputName) == 0f)
        {
            isTriggered = false;
        }
    }
}

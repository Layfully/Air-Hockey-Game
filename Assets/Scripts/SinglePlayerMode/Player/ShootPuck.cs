using UnityEngine;
public class ShootPuck : ShootPuckCore
{
    void Start()
    {
        Pointer = GameObject.FindGameObjectWithTag(PointerTag); // pointer tag "PlayerPointer";
    }

    void Update()
    {
        base.Update();
        if (Input.GetButton(InputName)) 
        {
            holdTime += Time.deltaTime / 5;
            holdTime = Mathf.Clamp(holdTime, 0, 2);

        }

        if (Input.GetButtonUp(InputName))
        {
            Fired = true;
            Shoot();
            holdTime = 0.7f;
        }
    }
}
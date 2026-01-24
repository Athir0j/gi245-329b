using UnityEngine;

public class CharAnimation : MonoBehaviour
{
    private Character character;

    private void ChooseAnimation(Character c)
    {
        c.Anim.SetBool("IsIdle", false);
        c.Anim.SetBool("IsWalk", false);

        switch (c.State)
        {
            case CharState.Idle:
                c.Anim.SetBool("IsIdle", true);
                break;
            case CharState.Idle:
                c.Anim.SetBool("IsWalk", true);
                break;

            

        }

    }

    void Start()
    {
        character =  GetComponent<Character>();
    }

    void Update() 
    {
        ChooseAnimation(character);
    }
}

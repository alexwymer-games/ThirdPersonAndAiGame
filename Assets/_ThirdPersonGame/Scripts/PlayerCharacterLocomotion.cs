using UnityEngine;

public class PlayerCharacterLocomotion : MonoBehaviour
{
    
    //Movement Input and Variables 
    [SerializeField] Vector2 inputVector;

    //Components 
    private Animator playerAnimator;


    #region LIFECYCLE

    private void Awake()
    {
        //Get attached Components
        playerAnimator = GetComponent<Animator>();
    }

    #endregion



    public void MovePlayer(Vector2 inputVec)
    {
        //Get Input
        inputVector = inputVec;

        //Set Animator Values
        playerAnimator.SetFloat("InputX", inputVector.x);
        playerAnimator.SetFloat("InputY", inputVector.y);
    }
}

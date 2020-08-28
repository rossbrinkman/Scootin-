using UnityEngine;

public class TrickEnded : StateMachineBehaviour
{
    private PlayerStateManager stateManager;
    private GameObject player;
    private float timeLength;
    private bool done = false;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        stateManager = player.GetComponent<PlayerStateManager>();
    }

    //OnStateEnter is called before OnStateEnter is called on any state inside this state machine
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    float endLength = 0;
    //    endLength += Time.deltaTime;
    //    Debug.Log("Begun");
    //    if (stateInfo.length >= stateInfo.length)
    //    {
    //        stateManager.doingTrick = false;
    //        Debug.Log("Begun2");
    //    }
    //    
    //}

    // OnStateUpdate is called before OnStateUpdate is called on any state inside this state machine
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //if (!done)
        //    timeLength = 0;
        //if (stateManager.doingTrick)
        //{
        //    timeLength += Time.deltaTime;
        //}
        //if (stateManager.currentState != PlayerStateManager.PlayerState.Pushing && !stateManager.doingTrick)
        //{
        //    stateManager.ActiveState(PlayerStateManager.PlayerState.Airborne);
        //}
    }

    //OnStateExit is called before OnStateExit is called on any state inside this state machine
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    //float length = animator.GetCurrentAnimatorStateInfo(0).length;
    //    //Debug.Log(stateManager.length + " : " + length);
    //    if (stateManager.currentState != PlayerStateManager.PlayerState.Pushing && !stateManager.doingTrick)
    //    {
    //        stateManager.ActiveState(PlayerStateManager.PlayerState.Airborne);
    //    }
    //}

    // OnStateMove is called before OnStateMove is called on any state inside this state machine
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateIK is called before OnStateIK is called on any state inside this state machine
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMachineEnter is called when entering a state machine via its Entry Node
    //override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    //{
    //    
    //}

    // OnStateMachineExit is called when exiting a state machine via its Exit Node
    //override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    //{
    //    
    //}
}

using UnityEngine;

public class FlyView : NetworkBehaviourView
{
	public Fly fly;
	public override MonoBehaviour localBehaviour { get { return fly; } }
}

using UnityEngine;

public class Done_DestroyByTime : MonoBehaviour
{
	public float lifetime;

	void Start ()
	{
		Destroy (gameObject, lifetime);
		GameManager.getInstance().deleteGameObject(gameObject);
	}
}

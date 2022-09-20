using UnityEngine;

/// <summary>
/// Drag a Rigidbody2D by selecting one of its colliders by pressing the mouse down.
/// When the collider is selected, add a TargetJoint2D.
/// While the mouse is moving, continually set the target to the mouse position.
/// When the mouse is released, the TargetJoint2D is deleted.`
/// </summary>
public class DragTarget : MonoBehaviour
{
	public LayerMask m_DragLayers;

	[Range (0.0f, 100.0f)]
	public float m_Damping = 1.0f;

	[Range (0.0f, 100.0f)]
	public float m_Frequency = 5.0f;

	[Range(0.0f, 5000.0f)]
	public float m_maxForce = 5.0f;

	public bool m_DrawDragLine = true;
	public Color m_Color = Color.cyan;

	private TargetJoint2D m_TargetJoint;

	Vector3 screenBounds;
	Vector3 offset;
	Vector2 updatedPosition;

	[SerializeField] GameObject TopWeight;
	[SerializeField] GameObject BottomWeight;

	private Rigidbody2D body;
	
	void Start()
    {
		screenBounds = initBound();
    }

	void Update ()
	{
		Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		FindCollider(worldPos);
		Drag(worldPos);
	}

	void FindCollider(Vector3 worldPosition)
    {
		// Calculate the world position for the mouse.

		if (Input.GetMouseButtonDown(0))
		{
			// Fetch the first collider.
			// NOTE: We could do this for multiple colliders.
			var collider = Physics2D.OverlapPoint(worldPosition, m_DragLayers);
			if (!collider)
				return;

			// Fetch the collider body.
			body = collider.attachedRigidbody;
			if (!body)
				return;

			offset = body.transform.position - worldPosition;

			// Add a target joint to the Rigidbody2D GameObject.
			//if(body.tag == "Player")

			body.bodyType = RigidbodyType2D.Dynamic;
			m_TargetJoint = body.gameObject.AddComponent<TargetJoint2D>();
			m_TargetJoint.dampingRatio = m_Damping;
			m_TargetJoint.frequency = m_Frequency;
			m_TargetJoint.maxForce = m_maxForce;
			m_TargetJoint.autoConfigureTarget = false;

			// Attach the anchor to the other weight
			m_TargetJoint.anchor = m_TargetJoint.transform.InverseTransformPoint(worldPosition);

			/* if (collider.transform.gameObject.name == "Top Weight")
			{
				m_TargetJoint.anchor = m_TargetJoint.transform.InverseTransformPoint(BottomWeight.transform.position);
			}
			else if (collider.transform.gameObject.name == "Bottom Weight")
			{
				m_TargetJoint.anchor = m_TargetJoint.transform.InverseTransformPoint(TopWeight.transform.position);
			} */
		}
		else if (Input.GetMouseButtonUp(0))
		{
			Destroy(m_TargetJoint);
			m_TargetJoint = null;
			body.bodyType = RigidbodyType2D.Static; // so that Pizza stays in place
			return;
		}
	}

	void Drag(Vector3 position)
    {
		if (m_TargetJoint)
		{
			//So it doesn't go out of screen
			
			updatedPosition.x = Mathf.Clamp(position.x + offset.x, -(screenBounds.x), screenBounds.x);
			updatedPosition.y = Mathf.Clamp(position.y + offset.y, -(screenBounds.y), screenBounds.y);

			m_TargetJoint.target = updatedPosition;

			// Draw the line between the target and the joint anchor.
			if (m_DrawDragLine)
				Debug.DrawLine(m_TargetJoint.transform.TransformPoint(m_TargetJoint.anchor), position, m_Color);
		}
	}
	Vector3 initBound()
	{
		Camera mainCamera = Camera.main;
		Vector3 screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));
		Debug.Log(screenBounds);
		
		return screenBounds;
	}
}

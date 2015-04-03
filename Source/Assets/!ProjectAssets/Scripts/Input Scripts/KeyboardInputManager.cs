using UnityEngine;
using System.Collections;

public class KeyboardInputManager : MonoBehaviour 
{
    private CharacterController _cc;
    private Vector2 v_move;
    private Vector2 v_face;


    public CharacterController CC
    {
        get
        {
            return _cc;
        }
        set
        {
            _cc = value;
        }
    }
	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    void FixedUpdate()
    {
        // Generate a plane that intersects the transform's position with an upwards normal.
        Plane playerPlane = new Plane(Vector3.up, transform.position);

        // Generate a ray from the cursor position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Determine the point where the cursor ray intersects the plane.
        // This will be the point that the object must look towards to be looking at the mouse.
        // Raycasting to a Plane object only gives us a distance, so we'll have to take the distance,
        //   then find the point along that ray that meets that distance.  This will be the point
        //   to look at.
        float hitdist = 0.0f;
        Vector3 newRotation = new Vector3(0, 0, 0);
        // If the ray is parallel to the plane, Raycast will return false.
        if (playerPlane.Raycast(ray, out hitdist))
        {
            // Get the point along the ray that hits the calculated distance.
            Vector3 targetPoint = ray.GetPoint(hitdist);

            // Determine the target rotation.  This is the rotation if the transform looks at the target point.
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
            newRotation = targetRotation.eulerAngles;
            // Smoothly rotate towards the target point.
            //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        Vector3 v_move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        _cc.MoveAndFace(v_move, newRotation);

        if (Input.GetAxis("LeftClick") > 0f)
            _cc.Cast('R');
        if (Input.GetAxis("RightClick") > 0f)
            _cc.Cast('L');
        if (Input.GetAxis("AbilityA") > 0f)
            _cc.Cast('A');
        if (Input.GetAxis("AbilityB") > 0f)
            _cc.Cast('B');
        if (Input.GetAxis("AbilityX") > 0f)
            _cc.Cast('X');
        if (Input.GetAxis("AbilityY") > 0f)
            _cc.Cast('Y');
    }
}

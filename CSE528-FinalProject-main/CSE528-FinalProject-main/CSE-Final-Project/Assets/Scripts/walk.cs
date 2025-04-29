using UnityEngine;
using System.Collections;
public enum Dir
{
    left,
    right
};
public class Walk : MonoBehaviour
{
    public Sprite idle;      // Play this sprite if object is not walking.
    public Sprite[] sprites;
    public float framesPerSecond;
	public Dir dir;
    public float Speed = 1.0f; // always walk in x axis direction (positive or negative)

    private SpriteRenderer spriteRenderer;
    private int index = 0;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
		dir = Dir.right;
        //transform.rotation = Quaternion.Euler(0, 180, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal") > 0) // Walk to right (i.e. +X axis)
        {
            if (dir == Dir.left)
            {
                // flip method 1
                //Vector3 scale = transform.localScale;
                //if (scale.x < 0) 
                //{
                //	scale.x *= -1;
                //	transform.localScale = scale;
                //}
                Speed = -Speed;

                // flip method 2
                transform.localRotation = Quaternion.Euler(0, 0, 0);

                dir = Dir.right;
            }
            index = (int)(Time.timeSinceLevelLoad * framesPerSecond);
            index = index % sprites.Length;

            spriteRenderer.sprite = sprites[index];
            float runDist = Speed * Time.deltaTime;
            transform.Translate(runDist, 0, 0, Space.World);

        }
        else if (Input.GetAxis("Horizontal") < 0) // Walk to left (i.e. -X axis)
        {
            if (dir == Dir.right)
            {
                // flip method 1
                //Vector3 scale = transform.localScale;
                //if (scale.x > 0) 
                //{
                //	scale.x *= -1;
                //	transform.localScale = scale;
                //}
                Speed = -Speed;

                // flip method 2
                transform.localRotation = Quaternion.Euler(0, 180.0f, 0);
                dir = Dir.left;
            }

            index = (int)(Time.timeSinceLevelLoad * framesPerSecond);
            index = index % sprites.Length;
            spriteRenderer.sprite = sprites[index];

            float runDist = Speed * Time.deltaTime;
            transform.Translate(runDist, 0, 0, Space.World);
            //Debug.Log(transform.position.z);
        }
        else
        { 
            if (idle == null)
                spriteRenderer.sprite = sprites[index];
            else
                spriteRenderer.sprite = idle;
        }

    } // end of Update method ()
}

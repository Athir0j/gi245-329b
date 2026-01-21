using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera cam;

    [Header("Move")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform corner1;
    [SerializeField] private Transform corner2;


    [SerializeField] private float xInput;
    [SerializeField] private float zInput;

    

    public static CameraController instance;

    private Vector3 Clamp(Vector3 lowerLeft, Vector3 topRight)
    {
        Vector3 pos = new Vector3(Mathf.Clamp(transform.position.x, lowerLeft.x, topRight.x),
                                        transform.position.y,Mathf.Clamp(transform.position.z,lowerLeft.z, topRight.z));
        return pos;
    }

    [Header("Zoom")]
    [SerializeField] private float zoomModifier;
    private void Zoom()
    {
        zoomModifier = Input.GetAxis("Mouse ScrollWheel");
        if (Input.GetKey(KeyCode.Z))
            zoomModifier = -0.2f;
        if (Input.GetKey(KeyCode.X))
            zoomModifier = 0.2f;

        cam.orthographicSize += zoomModifier;
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, 4,10);
    }

    
    private void MoveByMouse()
    {
        //ผมไม่แน่จะนำว่าทำไงไปซ้ายไปไงแต่จาก check debug ตำแหน่งเมาส์ เป็นเลยลองให้เป็น 0 ไปเลย แบบเดี๋ยวรอดูของ อาจารย์ วันเสาร์นี้ หวังว่าผมจะไปทันT-T 
        Debug.Log(Input.mousePosition);
        //right
        if (Input.mousePosition.x >= Screen.width)
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime, Space.World);
        //left
        if (Input.mousePosition.x <= 0)
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime, Space.World); 
        //up
        if (Input.mousePosition.y >= Screen.height)
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, Space.World);
        //down
        if (Input.mousePosition.y <= 0)
            transform.Translate(Vector3.back * moveSpeed * Time.deltaTime, Space.World);
        
    }
    

    void Awake()
    {
        instance = this;
        cam = Camera.main;
    }

    private void MoveByKB()
    {
        xInput = Input.GetAxis("Horizontal");
        zInput = Input.GetAxis("Vertical");

        Vector3 dir = (transform.forward * zInput) + (transform.right * xInput);
        transform.position += dir * moveSpeed * Time.deltaTime;
        transform.position = Clamp(corner1.position, corner2.position);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveSpeed = 50;
    }



    // Update is called once per frame
    void Update()
    {
        MoveByKB();
        Zoom();
        MoveByMouse();
    }

}

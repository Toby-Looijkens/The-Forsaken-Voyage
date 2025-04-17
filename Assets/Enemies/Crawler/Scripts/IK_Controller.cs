using Unity.VisualScripting;
using UnityEngine;

public class IK_Controller : MonoBehaviour
{
    [SerializeField] Node nodePrefab;

    [Header("Arcing raycast settings")]
    [SerializeField] int amountOfArcsPerRow = 10;
    [SerializeField] int rows = 1;
    [SerializeField] float spacingBetweenRows = 1.5f;
    [SerializeField] float arcAngle = 270;
    [SerializeField] float arcRadius = 1f;
    [SerializeField] int arcResolution = 6;
    [SerializeField] bool turnOnDebugRays = false;
    [SerializeField] LayerMask layer;

    [Header("IK Controllers")]
    [SerializeField] Transform IK_Left_Front;
    [SerializeField] Transform IK_Left_Back;
    [SerializeField] Transform IK_Right_Front;
    [SerializeField] Transform IK_Right_Back;
    [SerializeField] float stepSize = 1f;

    private Node Left_FrontNode;
    private Node Left_BackNode;
    private Node Right_FrontNode;
    private Node Right_BackNode;

    private void Awake()
    {
        Left_FrontNode = Instantiate(nodePrefab, IK_Left_Front.transform.position, Quaternion.identity);
        Left_BackNode = Instantiate(nodePrefab, IK_Left_Back.transform.position, Quaternion.identity);
        Right_FrontNode = Instantiate(nodePrefab, IK_Right_Front.transform.position, Quaternion.identity);
        Right_BackNode = Instantiate(nodePrefab, IK_Right_Back.transform.position, Quaternion.identity);
    }

    void Update()
    {
        IK_Left_Front.position = Vector3.MoveTowards(IK_Left_Front.position, Left_FrontNode.transform.position, 5);
        IK_Left_Back.position = Vector3.MoveTowards(IK_Left_Back.position, Left_BackNode.transform.position, 5);
        IK_Right_Front.position = Vector3.MoveTowards(IK_Right_Front.position, Right_FrontNode.transform.position, 5);
        IK_Right_Back.position = Vector3.MoveTowards(IK_Right_Back.position, Right_BackNode.transform.position, 5);
        AlignToSurface(IK_Left_Front, 315, Left_FrontNode);
        AlignToSurface(IK_Left_Back, 225, Left_BackNode);
        AlignToSurface(IK_Right_Front, 45, Right_FrontNode);
        AlignToSurface(IK_Right_Back, 135, Right_BackNode);
    }

    private void AlignToSurface(Transform IK, float angle, Node node)
    {
        if (RaycastExtensions.ArcCast(transform.position, transform.rotation * Quaternion.Euler(0, angle, 0), arcAngle, arcRadius, arcResolution, layer, out RaycastHit _hit, turnOnDebugRays))
        {
            if ((_hit.point - node.transform.position).magnitude > stepSize) 
            {
                node.transform.position = _hit.point;
            }
        }
    }
}

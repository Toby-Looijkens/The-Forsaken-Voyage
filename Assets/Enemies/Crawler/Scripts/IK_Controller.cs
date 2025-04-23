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

    private Node IK_Left_FrontNode;
    private Node IK_Left_BackNode;
    private Node IK_Right_FrontNode;
    private Node IK_Right_BackNode;

    private void Awake()
    {
        Left_FrontNode = Instantiate(nodePrefab, IK_Left_Front.transform.position, Quaternion.identity);
        Left_BackNode = Instantiate(nodePrefab, IK_Left_Back.transform.position, Quaternion.identity);
        Right_FrontNode = Instantiate(nodePrefab, IK_Right_Front.transform.position, Quaternion.identity);
        Right_BackNode = Instantiate(nodePrefab, IK_Right_Back.transform.position, Quaternion.identity);
        IK_Left_FrontNode = Instantiate(nodePrefab, IK_Left_Front.transform.position, Quaternion.identity);
        IK_Left_BackNode = Instantiate(nodePrefab, IK_Left_Back.transform.position, Quaternion.identity);
        IK_Right_FrontNode = Instantiate(nodePrefab, IK_Right_Front.transform.position, Quaternion.identity);
        IK_Right_BackNode = Instantiate(nodePrefab, IK_Right_Back.transform.position, Quaternion.identity);
        IK_Left_Front.position = Left_FrontNode.transform.position + transform.forward * 0.5f;
        IK_Left_Back.position = Left_BackNode.transform.position - transform.forward * 0.5f;
        IK_Right_Front.position = Right_FrontNode.transform.position - transform.forward * 0.5f;
        IK_Right_Back.position = Right_BackNode.transform.position + transform.forward * 0.5f;
    }

    void Update()
    {
        if ((Left_FrontNode.transform.position - IK_Left_FrontNode.transform.position ).magnitude > stepSize)
        {
            IK_Left_FrontNode.transform.position = Left_FrontNode.transform.position;
        }

        if ((Left_BackNode.transform.position - IK_Left_BackNode.transform.position).magnitude > stepSize)
        {
            IK_Left_BackNode.transform.position = Left_BackNode.transform.position;
        }

        if ((Right_FrontNode.transform.position - IK_Right_FrontNode.transform.position).magnitude > stepSize)
        {
            IK_Right_FrontNode.transform.position = Right_FrontNode.transform.position;
        }

        if ((Right_BackNode.transform.position - IK_Right_BackNode.transform.position).magnitude > stepSize)
        {
            IK_Right_BackNode.transform.position = Right_BackNode.transform.position;
        }

        IK_Left_Front.position = IK_Left_FrontNode.transform.position;
        IK_Left_Back.position = IK_Left_BackNode.transform.position;
        IK_Right_Front.position = IK_Right_FrontNode.transform.position;
        IK_Right_Back.position = IK_Right_BackNode.transform.position;
        AlignToSurface(315, Left_FrontNode);
        AlignToSurface(225, Left_BackNode);
        AlignToSurface(45, Right_FrontNode);
        AlignToSurface(135, Right_BackNode);
    }

    private void AlignToSurface(float angle, Node node)
    {
        if (RaycastExtensions.ArcCast(transform.position, transform.rotation * Quaternion.Euler(0, angle, 0), arcAngle, arcRadius, arcResolution, layer, out RaycastHit _hit, turnOnDebugRays))
        {
            if ((_hit.point - node.transform.position).magnitude > stepSize) 
            {
                node.transform.position = _hit.point;
            }
        }
    }

    private void OnDestroy()
    {
        Destroy(Left_FrontNode);
        Destroy(Left_BackNode);
        Destroy(Right_FrontNode);
        Destroy(Right_BackNode);
        Destroy(IK_Left_FrontNode);
        Destroy(IK_Left_BackNode);
        Destroy(IK_Right_FrontNode);
        Destroy(IK_Right_BackNode);
    }
}

/*
 * BAHMANSweepManager V1.1
 * 
 * detect sweeps on the screen and fires proper event
 * to use this class just listen to the following events:
 * OnSweep : outloauds the direction of a complete and complying  with the rules sweep.
 * OnStartDragging : the drag is started and the coordination on screen outloads.
 * OnDragging : dragging is currently occuring and the position on screen updates.
 * OnEndDragging : dragging is just ended and the end location outloads.
 * OnCompleteDragOccured : fires when a complete drag occures bu it not check the rules. start and end and the duration outlauds.
 */

using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// the direction in which sweep occured
/// </summary>
public enum SweepDirections { Left, Right, Up, Down };

public class BAHMANSweepManager : MonoBehaviour
{
    [SerializeField] float _MaximumXDrag = 200f, _MaximumYDrag = 100f,_DragDeadTime = .8f;
    /// <summary>
    /// fires when sweep completed;
    /// </summary>
    public static event UnityAction<SweepDirections> OnSweep;
    /// <summary>
    /// fires when dragging started
    /// </summary>
    public static event UnityAction<Vector3> OnStartDragging;
    /// <summary>
    /// fires when dragging continues
    /// </summary>
    public static event UnityAction<Vector3> OnDragging;
    /// <summary>
    /// fires when dragging ended
    /// </summary>
    public static event UnityAction<Vector3> OnEndDragging;
    /// <summary>
    /// fires when a complete drag occured
    /// </summary>
    public static event UnityAction<Vector3, Vector3, float> OnCompleteDragOccured;
    /// <summary>
    /// whether provide debug information or not
    /// </summary>
    [SerializeField] bool _provideDebugInformation = false;
    /// <summary>
    /// check if the dragging is started
    /// </summary>
    bool _isDragging = false;
    /// <summary>
    /// the duration of drag
    /// </summary>
    float _dragDuration = 0;
    /// <summary>
    /// the start position of the drag
    /// </summary>
    Vector3 _dragStartPosition = new Vector3(0, 0, 0);
    /// <summary>
    /// the end position of the drag
    /// </summary>
    Vector3 _dragEndPosition = new Vector3(0, 0, 0);
    /// <summary>
    /// this field is for display porposes . it shows the sweep direction in the editor
    /// </summary>
    [SerializeField] SweepDirections _Direction;
    /// <summary>
    /// interprete the drag which is occured and fires the OnCompleteDragOccured with 
    /// </summary>

    private void Start()
    {
        DontDestroyOnLoad(this);
    }
    void _dlog(string iMessage)
    {
        if(_provideDebugInformation)
        {
            Debug.Log(iMessage);
        }
    }
    void _describeAction()
    {
        if (_dragDuration > _DragDeadTime) return;
        OnCompleteDragOccured?.Invoke(_dragStartPosition, _dragEndPosition, _dragDuration);
        float xDelta = _dragStartPosition.x - _dragEndPosition.x;
        float yDelta = _dragStartPosition.y - _dragEndPosition.y;
        if ((Mathf.Abs(xDelta) > _MaximumXDrag)
            || (Mathf.Abs(yDelta) > _MaximumYDrag))
        {

            if (Mathf.Abs(xDelta) > Mathf.Abs(yDelta))
            {
                // move along the x axis
                if (xDelta > 0)
                {
                    OnSweep?.Invoke(SweepDirections.Left);
                    _Direction = SweepDirections.Left;
                    _dlog("Sweep Left");
                }
                else if (xDelta < 0)
                {
                    _dlog("Sweep Right");
                    OnSweep?.Invoke(SweepDirections.Right);
                    _Direction = SweepDirections.Right;
                }
                else
                {
                    _dlog("No Magnetiude");
                }
            }
            else
            {
                // move along the y axis
                if (yDelta > 0)
                {
                    OnSweep?.Invoke(SweepDirections.Down);
                    _Direction = SweepDirections.Down;
                    _dlog("Sweep Down");
                }
                else if (yDelta < 0)
                {
                    _dlog("Sweep Up");
                    OnSweep?.Invoke(SweepDirections.Up);
                    _Direction = SweepDirections.Up;
                }
                else
                {
                    _dlog("No Magnetiude");
                }
            }
        }
        else
        {
            _dlog("No Magnetiude");
        }
    }

    void Update()
    {

        if (Input.GetMouseButton(0))
        {
            if (!_isDragging)
            {
                _dragDuration = 0;
                _isDragging = true;
                _dragStartPosition = Input.mousePosition;
                OnStartDragging?.Invoke(_dragStartPosition);
            }
            else
            {
                _dragDuration += Time.deltaTime;
                OnDragging?.Invoke(Input.mousePosition);

            }
            _dlog("Dragging.");
        }
        else
        {
            if (_isDragging)
            {
                _isDragging = false;
                _dlog("Drag Duration= " + _dragDuration);
                _dragEndPosition = Input.mousePosition;
                _dlog("Drag Magnetitude= " + Vector3.Distance(_dragStartPosition, _dragEndPosition));
                OnEndDragging?.Invoke(_dragEndPosition);
                _describeAction();
            }
            else
            {

                _dlog("IDLE");
            }
        }

    }
}

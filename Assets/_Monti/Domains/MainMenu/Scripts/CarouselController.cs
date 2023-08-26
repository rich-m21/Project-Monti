using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Monti
{
    public enum CarouselMovementType
    {
        NONE = 0,
        DRAG_START = 1,
        DRAG = 2,
        DRAG_END = 3,
        TRANSITION = 4,
        REVERT = 5
    }
    
    public class CarouselController : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        [SerializeField] RectTransform _root = null;
        [SerializeField] RectTransform[] _slides;
        [SerializeField] RectTransform _outSideRight = null;
        [SerializeField] RectTransform _outSideLeft = null;
        [SerializeField] RectTransform _center = null;

        CarouselMovementType _carouselMovementType = CarouselMovementType.NONE;
        
        bool _dragging = false;
        float _dragThreshold = 50f;// Threshold for a strong swipe
        float _maxDistanceThreshold = 200f;
        bool _thresholdHit = false;

        int _currentIndexToSlideOut = 0;
        int _currentIndexToSlideIn = 1;

        int _cachedNextIndexOut = 0;
        int _cachedNextIndexIn = 0;

        float _dragPositionX = 0f;
        float _initialPositionX = 0f;
        
        float _transitionOutDelta = 0f;

        bool _hasTransitionedIn = false;
        bool _hasTransitionedOut = false;


        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            switch(_carouselMovementType)
            {

                case CarouselMovementType.NONE:
                    break;
                case CarouselMovementType.DRAG_START:
                    break;
                case CarouselMovementType.DRAG:
                    LerpToDrag(_dragPositionX);
                    break;
                case CarouselMovementType.DRAG_END:
                    if(_thresholdHit)
                    {
                        _carouselMovementType = CarouselMovementType.TRANSITION;
                    }
                    else
                    {
                        _carouselMovementType = CarouselMovementType.REVERT;
                    }
                    break;
                case CarouselMovementType.TRANSITION:
                    if(!_hasTransitionedIn)
                    {
                        LerpToTransitionInNextSlide();
                    }

                    if(!_hasTransitionedOut)
                    {
                        LerpToTransitionOutCurrentSlide(_transitionOutDelta);
                    }

                    if(_hasTransitionedIn && _hasTransitionedOut)
                    {
                        _carouselMovementType = CarouselMovementType.NONE;
                    }
                    break;
                case CarouselMovementType.REVERT:
                    if(!_hasTransitionedIn)
                    {
                        LerpToTransitionOutToCenter();
                    }
                    else
                    {
                        _carouselMovementType = CarouselMovementType.NONE;
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        void LerpToTransitionInNextSlide()
        {
            if(Math.Abs(_slides[_currentIndexToSlideIn].position.x - _center.position.x) < .01f)
            {
                _hasTransitionedIn = true;
                _currentIndexToSlideIn = _cachedNextIndexIn;
                _slides[_currentIndexToSlideIn].position = new Vector3(_center.position.x, _slides[_currentIndexToSlideIn].position.y, _slides[_currentIndexToSlideIn].position.z);
                return;
            }
            
            float newX = Mathf.Lerp(_slides[_currentIndexToSlideIn].position.x, _center.position.x, Time.deltaTime * 10f);
            Vector3 newPosition = new Vector3(newX, _slides[_currentIndexToSlideIn].position.y, _slides[_currentIndexToSlideIn].position.z);
            _slides[_currentIndexToSlideIn].position = newPosition;
        }
        
        void LerpToTransitionOutToCenter()
        {
            if(Math.Abs(_slides[_currentIndexToSlideOut].position.x - _center.position.x) < .01f)
            {
                _hasTransitionedIn = true;
                _slides[_currentIndexToSlideOut].position = new Vector3(_center.position.x, _slides[_currentIndexToSlideOut].position.y, _slides[_currentIndexToSlideOut].position.z);
                return;
            }
            
            float newX = Mathf.Lerp(_slides[_currentIndexToSlideOut].position.x, _center.position.x, Time.deltaTime * 10f);
            Vector3 newPosition = new Vector3(newX, _slides[_currentIndexToSlideOut].position.y, _slides[_currentIndexToSlideOut].position.z);
            _slides[_currentIndexToSlideOut].position = newPosition;
        }

        void LerpToTransitionOutCurrentSlide(float position)
        {
            if(Math.Abs(_slides[_currentIndexToSlideOut].position.x - position) < .01f)
            {
                _hasTransitionedOut = true;
                _currentIndexToSlideOut = _cachedNextIndexOut;
                return;
            }
            
            float newX = Mathf.Lerp(_slides[_currentIndexToSlideOut].position.x, position, Time.deltaTime * 10f);
            Vector3 newPosition = new Vector3(newX, _slides[_currentIndexToSlideOut].position.y, _slides[_currentIndexToSlideOut].position.z);
            _slides[_currentIndexToSlideOut].position = newPosition;
        }
        

        void LerpToDrag(float position)
        {
            float newX = Mathf.Lerp(_slides[_currentIndexToSlideOut].position.x, position, Time.deltaTime * 10f);
            Vector3 newPosition = new Vector3(newX, _slides[_currentIndexToSlideOut].position.y, _slides[_currentIndexToSlideOut].position.z);
            _slides[_currentIndexToSlideOut].position = newPosition;
        }


        public void OnBeginDrag(PointerEventData eventData)
        {
            _initialPositionX = eventData.position.x;
            _carouselMovementType = CarouselMovementType.DRAG_START;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _carouselMovementType = CarouselMovementType.DRAG_END;

            Debug.Log(eventData.delta);
            // Check for a strong swipe
            if(Mathf.Abs(eventData.delta.x) >= _dragThreshold ||  Mathf.Abs(_dragPositionX - _initialPositionX) > _maxDistanceThreshold)
            {
                // Check the direction of the swipe (positive for right, negative for left)
                if(eventData.delta.x > 0 ||  _dragPositionX > _initialPositionX)
                {
                    if(_currentIndexToSlideOut > 0)
                    {
                        _transitionOutDelta = _outSideRight.position.x;
                        _currentIndexToSlideIn = _currentIndexToSlideOut - 1;
                        _cachedNextIndexOut = _currentIndexToSlideOut - 1; // 1
                        _hasTransitionedOut = false;
                        _hasTransitionedIn = false;
                        _thresholdHit = true;
                        return;
                    }
                    else
                    {
                        _thresholdHit = false;
                    }
                }
                else if(eventData.delta.x < 0 || _dragPositionX < _initialPositionX)
                {
                    if(_currentIndexToSlideOut < _slides.Length - 1)
                    {
                        _transitionOutDelta = _outSideLeft.position.x;
                        _currentIndexToSlideIn = _currentIndexToSlideOut + 1;
                        _cachedNextIndexOut = _currentIndexToSlideOut + 1; // 0
                        _hasTransitionedOut = false;
                        _hasTransitionedIn = false;
                        _thresholdHit = true;
                        return;
                    }
                    else
                    {
                        _thresholdHit = false;
                    }
                }
            }
            else
            {
                _hasTransitionedIn = false;
            }
            
        }
        public void OnDrag(PointerEventData eventData)
        {
            _carouselMovementType = CarouselMovementType.DRAG;

            _dragPositionX = eventData.position.x;
            
        }
    }
}

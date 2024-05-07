using UnityEngine.Splines;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
namespace Arab
{
    public enum Car
    {
        _Car_1,
        _Car_2,
        _Car_3
    }

    public class CarController : MonoBehaviour
    {
        public SpriteRenderer playerProfile_SpriteRend;
        public TextMesh playerProfile_Name;
        public Car _car;
        public float _current_speed;
        public float normalSpeed = 30f; // lower value to increase speed
        public bool isGameStarted = false;

        private bool isCanMove = false;
        private SplineAnimate splineAnimate;
        public static event Action<CarController, float> OnFinished;

        public void SetSpeed(float newSpeed)
        {
            _current_speed = newSpeed;
        }

        private void Move()
        {
            if (!isCanMove) return;
            splineAnimate.Duration = _current_speed;
            splineAnimate.Play();
            isCanMove = false;
        }
        private void ResetMove()
        {
            splineAnimate.Duration = normalSpeed;
            splineAnimate.Restart(false);
            isCanMove = false;
        }
        public void PauseTheMove()
        {
            splineAnimate.Pause();
            splineAnimate.Duration = normalSpeed;
            isCanMove = false;
        }
        public void MoveAfterStop()
        {
            splineAnimate.Pause();
            splineAnimate.Duration = normalSpeed;
            isCanMove = true;
        }

        private void Awake()
        {
            //GET
            splineAnimate = GetComponent<SplineAnimate>();
            switch (_car)
            {
                case Car._Car_1:
                    splineAnimate.Container = GameObject.FindGameObjectWithTag("Spline1").GetComponent<SplineContainer>();

                    break;
                case Car._Car_2:
                    splineAnimate.Container = GameObject.FindGameObjectWithTag("Spline2").GetComponent<SplineContainer>();

                    break;
                case Car._Car_3:
                    splineAnimate.Container = GameObject.FindGameObjectWithTag("Spline3").GetComponent<SplineContainer>();
                    break;
                default:
                    break;
            }
        }

        private void Start()
        {
            //SETUP
            splineAnimate.PlayOnAwake = false;
            _current_speed = normalSpeed;
            splineAnimate.Loop = SplineAnimate.LoopMode.Once;
            splineAnimate.Easing = SplineAnimate.EasingMode.EaseIn;

        }
        private void Update()
        {
            if (isGameStarted)
            {
                isCanMove = true;
                Move();
            }
            else
            {
                isCanMove = false;
                ResetMove();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Finish"))
            {
                print("invoked");
                OnFinished?.Invoke(this, Time.time);

            }
        }

    }
}
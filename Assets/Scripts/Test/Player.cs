using System;
using UnityEngine;

namespace Test
{
    public class Player : MonoBehaviour
    {
        public InputViewMobile InputViewMobile;
        public AnimationController AnimationController;
        private IInput _input;
        // Start is called before the first frame update
        void Start()
        {
            _input = new InputControllerMobile(InputViewMobile);
            _input.Start();
            _input.AxisCodeInputReturn += CheckAxis;
            _input.ButtonCodeInputReturn += CheckButtons;
        }


        private void FixedUpdate()
        {
            _input.FixedUpdate();
        }

        void CheckAxis(AxesName nameAxis, Vector2 axis)
        {
            switch (nameAxis)
            {
                case AxesName.DirectionMove:
                    AnimationController.SetDirectionMove(axis);
                    break;
                case AxesName.CameraMovePressed:
                   
                    break;
                case AxesName.CameraMoveOnUp:
           
                    break;
            } 
        }

        void CheckButtons(ButtonsName nameButton, ButtonState state)
        {
            Debug.Log(nameButton);
            Debug.Log(state);
            switch (nameButton)
            {
                case ButtonsName.Atack:
                    AnimationController.Attack();
                    break;  
                case ButtonsName.Jump:
                    break;
                case ButtonsName.Aim:
                    AnimationController.SetIsAim(true);
                    break;
                case ButtonsName.Block:
                    break;
                case ButtonsName.Dodging:
                    AnimationController.Dodging();
                    break;
            }
        }
    }
}

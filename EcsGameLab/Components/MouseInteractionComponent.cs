using EcsGameLab.Statics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace EcsGameLab.Components
{
    public class MouseInteractionComponent : Component
    {
        private AnimationComponent _mouseIn;
        private AnimationComponent _mouseOut;
        private AnimationComponent _mouseLeftDown;
        private AnimationComponent _mouseRightDown;
        private AnimationComponent _mouseLeftUp;
        private AnimationComponent _mouseRightUp;
        private AnimationComponent _mouseMiddleDown;
        private AnimationComponent _mouseMiddleUp;
        private AnimationComponent _mouseScrollUp;
        private AnimationComponent _mouseScrollDown;


        private TransformComponent _ownerTransform;
        private bool prevMouseIn;

        // Store the previous MouseState to compare with the current state
        private MouseState _previousMouseState;

        public MouseInteractionComponent(bool expires = false) : base(expires)
        {
        }

        public override void Initialize()
        {
            _ownerTransform = Owner.GetComponent<TransformComponent>();
            foreach (AnimationComponent animationComponent in Owner.GetComponents<AnimationComponent>())
            {
                switch (animationComponent.Name)
                {
                    case AnimationNames.MouseIn:
                        _mouseIn = animationComponent;
                        break;
                    case AnimationNames.MouseOut:
                        _mouseOut = animationComponent;
                        break;
                    case AnimationNames.MouseLeftDown:
                        _mouseLeftDown = animationComponent;
                        break;
                    case AnimationNames.MouseRightDown:
                        _mouseRightDown = animationComponent;
                        break;
                    case AnimationNames.MouseLeftUp:
                        _mouseLeftUp = animationComponent;
                        break;
                    case AnimationNames.MouseRightUp:
                        _mouseRightUp = animationComponent;
                        break;
                    case AnimationNames.MouseMiddleDown:
                        _mouseMiddleDown = animationComponent;
                        break;
                    case AnimationNames.MouseMiddleUp:
                        _mouseMiddleUp = animationComponent;
                        break;
                    case AnimationNames.MouseScrollUp:
                        _mouseScrollUp = animationComponent;
                        break;
                    case AnimationNames.MouseScrollDown:
                        _mouseScrollDown = animationComponent;
                        break;
                }
            }
            _previousMouseState = Mouse.GetState();
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            MouseState currentMouseState = Mouse.GetState();
            Vector2 mousePosition = new Vector2(currentMouseState.X, currentMouseState.Y);
            bool currentMouseIn = _ownerTransform.Bounds.Contains(mousePosition);

            if (currentMouseIn)
            {
                if (!prevMouseIn)
                {
                    // Mouse just entered the object area
                    _mouseIn?.StartAnimation(gameTime);
                    _mouseOut?.Reset();
                }

                // Since the mouse is currently over the object, check for clicks and scroll events here

                // Left button down
                if (_previousMouseState.LeftButton == ButtonState.Released && currentMouseState.LeftButton == ButtonState.Pressed)
                {
                    _mouseLeftDown?.StartAnimation(gameTime);
                    _mouseLeftUp?.Reset();  
                }
                // Left button up
                if (_previousMouseState.LeftButton == ButtonState.Pressed && currentMouseState.LeftButton == ButtonState.Released)
                {
                    _mouseLeftUp?.StartAnimation(gameTime);
                    var onClick = Owner.GetComponent<OnClickComponent>();
                    onClick.Clicked = true;
                    _mouseLeftDown?.Reset();
                }

                // Right button down
                if (_previousMouseState.RightButton == ButtonState.Released && currentMouseState.RightButton == ButtonState.Pressed)
                {
                    _mouseRightDown?.StartAnimation(gameTime);
                    _mouseRightUp?.Reset();
                }
                // Right button up
                if (_previousMouseState.RightButton == ButtonState.Pressed && currentMouseState.RightButton == ButtonState.Released)
                {
                    _mouseRightUp?.StartAnimation(gameTime);
                    _mouseRightDown?.Reset();
                }

                // Middle button down
                if (_previousMouseState.MiddleButton == ButtonState.Released && currentMouseState.MiddleButton == ButtonState.Pressed)
                {
                    _mouseMiddleDown?.StartAnimation(gameTime);
                    _mouseMiddleUp?.Reset();
                }
                // Middle button up
                if (_previousMouseState.MiddleButton == ButtonState.Pressed && currentMouseState.MiddleButton == ButtonState.Released)
                {
                    _mouseMiddleUp?.StartAnimation(gameTime);
                    _mouseMiddleDown?.Reset();
                }

                // Scroll wheel up - this is a simplified example and might need to be adjusted based on your definition of "scroll up"
                if (currentMouseState.ScrollWheelValue > _previousMouseState.ScrollWheelValue)
                {
                    _mouseScrollUp?.StartAnimation(gameTime);
                    _mouseScrollDown?.Reset();
                }
                // Scroll wheel down
                if (currentMouseState.ScrollWheelValue < _previousMouseState.ScrollWheelValue)
                {
                    _mouseScrollDown?.StartAnimation(gameTime);
                    _mouseScrollUp?.Reset();
                }

            }
            else
            {
                if (prevMouseIn)
                {
                    // Mouse has left the object area
                    _mouseOut?.StartAnimation(gameTime);
                    _mouseIn?.Reset();
                }
            }

            // Update _previousMouseState and prevMouseIn at the end of the method
            _previousMouseState = currentMouseState;
            prevMouseIn = currentMouseIn;

            base.Update(gameTime);
        }
    }
}

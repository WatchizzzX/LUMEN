namespace Animators.Interfaces
{
    public interface IAnimator
    {
        public void Animate();
        
        public void Animate(bool value);

        public void Animate(bool value, float duration);
    }
}
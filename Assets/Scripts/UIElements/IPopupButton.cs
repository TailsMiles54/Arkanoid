namespace MiniIT.ARKANOID.UIElements
{
    public interface IPopupButton<BasePopupButtonSetting>
    {
        public void Setup(BasePopupButtonSetting settings);
        public void ButtonClick();
    }
    
    public class BasePopupButtonSetting
    {
        
    }
}
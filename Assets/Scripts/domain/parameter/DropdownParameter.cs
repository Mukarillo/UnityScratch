namespace domain.parameter
{
    public class DropdownParameter<T> : BaseParameter<T>
    {
        private DropdownParameterProvider<T> provider;
        private int currentSelectedIndex = 0;

        public DropdownParameter(DropdownParameterProvider<T> provider, int initialSelectedIndex = 0)
        {
            this.provider = provider;
            currentSelectedIndex = initialSelectedIndex;
        }

        public void SelectIndex(int index)
        {
            currentSelectedIndex = index;
        }

        public override T GetValue()
        {
            return GetOptions()[currentSelectedIndex];
        }
        
        public T[] GetOptions()
        {
            return provider.GetDropdownElements();
        }
    }
    
    public interface DropdownParameterProvider<out T>
    {
        T[] GetDropdownElements();
    }
}
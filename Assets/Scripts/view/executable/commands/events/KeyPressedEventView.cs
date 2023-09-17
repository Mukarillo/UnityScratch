using System.Linq;
using domain.commands.executables;
using domain.commands.executables.events.keypressed;
using UnityEngine;
using view.parameter;

namespace view.executable.commands.events
{
    public class KeyPressedEventView : ExecutableView
    {
        private ExecutableCommand command;
        public override ExecutableCommand Command => command;

        private ValidKeysDropdownParameter validKeysParameter;
        
        [SerializeField] private DropdownView dropdownView;

        public override void Setup(RectTransform root, ExecutableContext context)
        {
            base.Setup(root, context);
            var provider = new ValidKeysDropdownParameterProvider();
            validKeysParameter = new ValidKeysDropdownParameter(provider);
            
            dropdownView.SetupDropdown(validKeysParameter.GetOptions().Select(x => x.ToString()).ToArray());
            dropdownView.OnSelectOption.AddListener(i => validKeysParameter.SelectIndex(i));
            
            command = new KeyPressedEventCommand(context, validKeysParameter);
        }
    }
}
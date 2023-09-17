using System;
using System.Linq;
using domain.commands.executables;
using domain.commands.executables.events.message;
using domain.parameter;
using UnityEngine;
using view.parameter;

namespace view.executable.commands.events
{
    public class BroadcastMessageEventView : ExecutableView
    {
        private ExecutableCommand command;
        public override ExecutableCommand Command => command;

        private MessagesDropdownParameter parameter;

        [SerializeField] private DropdownView dropdown;

        public override void Setup(RectTransform root, ExecutableContext context)
        {
            base.Setup(root, context);

            DropdownParameterProvider<Message> provider = new MessagesDropdownParameterProvider();
            parameter = new MessagesDropdownParameter(provider);
            
            dropdown.SetupDropdown(parameter.GetOptions().Select(x => x.Value).ToArray());
            dropdown.OnSelectOption.AddListener(i => parameter.SelectIndex(i));
            
            command = new BroadcastMessageEventCommand(context, parameter);
            
            MessagesManager.Instance.OnMessagesUpdated += OnMessagesUpdated;
        }

        private void OnMessagesUpdated(object sender, EventArgs e)
        {
            dropdown.SetupDropdown(parameter.GetOptions().Select(x => x.Value).ToArray());
        }
    }
}
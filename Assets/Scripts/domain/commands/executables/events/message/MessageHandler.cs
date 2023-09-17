namespace domain.commands.executables.events.message
{
    public delegate void MessageEventHandler(MessageEventArgs args);
    
    public static class MessageHandler
    {
        public static event MessageEventHandler EvtHandler = delegate { };
        
        public static void RaiseEvent(Message message)
        {
            EvtHandler.Invoke(new MessageEventArgs(message));
        }
    }
}
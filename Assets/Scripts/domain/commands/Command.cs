namespace domain.commands
{
    public abstract class Command
    {
        public Context Context { get; }
        public abstract Connection Connection { get; } 

        protected Command(Context context)
        {
            Context = context;

            Initialize();
        }

        ~Command()
        {
            Destroy();
        }
        
        protected virtual void Initialize() { }
        public virtual void Destroy() { }
    }
}
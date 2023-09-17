using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace domain.commands.executables
{
    public class ExecutableContext : Context
    {
        public List<ExecutableCommand> RootCommands { get; private set; } = new List<ExecutableCommand>();
        public bool IsClone { get; }
        public bool IsDisposed { get; private set; }
        public CancellationTokenSource CancellationToken { get; private set; }

        public ExecutableContext(GameObject gameObject, bool isClone = false) : base(gameObject)
        {
            IsClone = isClone;
        }

        public void AddRootCommand(ExecutableCommand command)
        {
            RootCommands.Add(command);   
        }
        
        public void SetRootCommands(List<ExecutableCommand> rootCommands)
        {
            RootCommands = rootCommands;
        }

        public void Dispose()
        {
            Stop();
            IsDisposed = true;
            GameObject.Destroy(GameObject);
        }

        public void Execute()
        {
            CancellationToken = new CancellationTokenSource();
            foreach (var command in RootCommands)
            {
                command.Execute().AttachExternalCancellation(CancellationToken.Token);
            }
        }

        public void Stop()
        {
            CancellationToken.Cancel();
            CancellationToken.Dispose();
        }

        public List<ExecutableCommand> CloneRootCommands(ExecutableContext newContext)
        {
            var result = new List<ExecutableCommand>(RootCommands.Count);
            foreach (var command in RootCommands)
            {
                var clone = command.Clone(newContext);
                result.Add(clone);
            }

            return result;
        }

        ~ExecutableContext()
        {
            Dispose();
        }
    }
}
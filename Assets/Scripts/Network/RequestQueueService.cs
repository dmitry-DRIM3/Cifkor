using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using System.Threading;
using System;

public class RequestQueueService
{
    private readonly Queue<IRequestCommand> _queue = new();
    private bool _isExecuting = false;
    private CancellationTokenSource _currentTokenSource;

    public void Enqueue(IRequestCommand command)
    {
        _queue.Enqueue(command);
        TryExecuteNext();
    }

    public void Remove(Predicate<IRequestCommand> match)
    {
        var items = new Queue<IRequestCommand>();
        while (_queue.Count > 0)
        {
            var item = _queue.Dequeue();
            if (!match(item))
                items.Enqueue(item);
        }

        while (items.Count > 0)
            _queue.Enqueue(items.Dequeue());
    }

    public void CancelCurrent()
    {
        _currentTokenSource?.Cancel();
    }

    private async void TryExecuteNext()
    {
        if (_isExecuting || _queue.Count == 0)
            return;

        _isExecuting = true;
        var command = _queue.Dequeue();
        _currentTokenSource = new CancellationTokenSource();

        try
        {
            await command.ExecuteAsync().AttachExternalCancellation(_currentTokenSource.Token);
        }
        catch (System.OperationCanceledException)
        {
            command.Cancel();
        }

        _isExecuting = false;
        TryExecuteNext();
    }
}

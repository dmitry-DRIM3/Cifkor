using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;
using System;
using System.Threading;

public class WeatherPresenter : BasePresenter<WeatherView>
{
    private readonly RequestQueueService queue;
    private CancellationTokenSource loopToken;

    public WeatherPresenter(WeatherView view, RequestQueueService queue) : base(view)
    {
        this.queue = queue;
    }

    public override void Initialize()
    {
        view.Show();
        StartLoop();
    }

    public override void Dispose()
    {
        loopToken?.Cancel();
        queue.CancelCurrent();
        view.Hide();
    }

    private void StartLoop()
    {
        loopToken = new CancellationTokenSource();
        RunLoop(loopToken.Token).Forget();
    }

    private async UniTaskVoid RunLoop(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            var cmd = new WeatherRequestCommand(OnWeatherDataReceived);
            queue.Enqueue(cmd);
            await UniTask.Delay(5000, cancellationToken: token);
        }
    }

    private void OnWeatherDataReceived(string json)
    {
        
        view.SetWeather("Сегодня", 61);
    }
}

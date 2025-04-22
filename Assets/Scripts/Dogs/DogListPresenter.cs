using Cysharp.Threading.Tasks;
using System;
using Zenject;
using UnityEngine;
using System.Collections.Generic;

public class DogListPresenter : BasePresenter<DogListView>
{
    private readonly RequestQueueService queue;
    private readonly DogInfoPopup popup;
    private string? currentDogId;

    public DogListPresenter(DogListView view, RequestQueueService queue, DogInfoPopup popup) : base(view)
    {
        this.queue = queue;
        this.popup = popup;
    }

    public override void Initialize()
    {
        view.Show();
        view.ShowLoader(true);
        view.OnDogClicked += OnDogClicked;

        queue.Enqueue(new DogListRequestCommand(OnListReceived));
    }

    public override void Dispose()
    {
        view.OnDogClicked -= OnDogClicked;
        queue.CancelCurrent();
        view.ShowLoader(false);
        view.Hide();
        popup.Hide();
    }

    private void OnListReceived(List<(string id, string name)> dogs)
    {
        view.ShowLoader(false);
        view.SetDogList(dogs);
    }

    private void OnDogClicked(string dogId)
    {
        if (currentDogId == dogId) return;

        queue.CancelCurrent();
        view.ShowLoader(true);
        popup.Hide();
        currentDogId = dogId;

        queue.Enqueue(new DogInfoRequestCommand(dogId, ShowDogInfo));
    }

    private void ShowDogInfo(string name, string description)
    {
        view.ShowLoader(false);
        popup.Show(name, description);
    }
}

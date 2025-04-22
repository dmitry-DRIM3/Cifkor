using Cysharp.Threading.Tasks;
using Unity.VisualScripting;

public interface IRequestCommand
{
    UniTask ExecuteAsync();
    void Cancel();
}